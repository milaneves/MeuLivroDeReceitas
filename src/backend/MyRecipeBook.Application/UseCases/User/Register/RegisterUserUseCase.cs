

namespace MyRecipeBook.Application.UseCases.User.Register;

public interface IRegisterUserUseCase : IUseCaseWithRequest<RequestRegisterUserJson> { }

public sealed class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterUserUseCase(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Execute(RequestRegisterUserJson request, CancellationToken cancellationToken)
    {
       
        var passwordCriptography = new PasswordEncripter();

        Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = passwordCriptography.Encrypt(request.Password);

        await _userRepository.Add(user, cancellationToken);
        //return new ResponseRegisteredUserJson
        //{
        //    Name = request.Name
        //};
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
