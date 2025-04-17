using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exception;

namespace MyRecipeBook.Application.UseCases.User.Register;

public interface IRegisterUserUseCase : IUseCase<RequestRegisterUserJson, ResponseRegisteredUserJson> { }

public sealed class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly PasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;


    public RegisterUserUseCase(
        IMapper mapper, 
        IUnitOfWork unitOfWork,
        IUserRepository userRepository, 
        PasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request, CancellationToken cancellationToken = default)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userRepository.Add(user, cancellationToken);

        await _unitOfWork.Commit();
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokenJson
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var emailExist = await _userRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
