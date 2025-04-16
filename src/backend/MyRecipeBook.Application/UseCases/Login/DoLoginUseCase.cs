
namespace MyRecipeBook.Application.UseCases.Login
{
    public interface IDoLoginUseCase : IUseCase<RequestLoginJson, ResponseRegisteredUserJson> { }

    public sealed class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordEncripter _passwordEncripter;

        public DoLoginUseCase(IUserRepository userRepository, PasswordEncripter passwordEncripter)
        {
            _userRepository = userRepository;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request, CancellationToken cancellation = default)
        {
            var encriptedPassword = _passwordEncripter.Encrypt(request.Password);
            var user = await _userRepository
                .GetByEmailAndPassword(request.Email, encriptedPassword);
                
            if(user is null)
                throw new InvalidLoginException();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
            };
        }
    }
}
