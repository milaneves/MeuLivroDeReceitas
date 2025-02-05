

using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Application.UseCases.User.Register;

public interface IRegisterUserUseCase : IUseCase<RequestRegisterUserJson, ResponseRegisteredUserJson> { }

public sealed class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly PasswordEncripter _passwordEncripter;

    public RegisterUserUseCase(
        IMapper mapper, 
        IUnitOfWork unitOfWork,
        IUserRepository userRepository, 
        PasswordEncripter passwordEncripter)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordEncripter = passwordEncripter;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request, CancellationToken cancellationToken)
    {
        Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);

        await _userRepository.Add(user, cancellationToken);

        await _unitOfWork.Commit();
        return new ResponseRegisteredUserJson
        {
            Name = request.Name
        };
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
