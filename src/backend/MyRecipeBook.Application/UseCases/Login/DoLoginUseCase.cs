
using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.Application.UseCases.Login
{
    public interface IDoLoginUseCase : IUseCase<RequestLoginJson, ResponseRegisteredUserJson> { }

    public sealed class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public DoLoginUseCase(IUserRepository userRepository, 
            PasswordEncripter passwordEncripter,
            IAccessTokenGenerator accessTokenGenerator
            )
        {
            _userRepository = userRepository;
            _passwordEncripter = passwordEncripter;
            _accessTokenGenerator = accessTokenGenerator;
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
                Tokens = new ResponseTokenJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
                }
            };
        }
    }
}
