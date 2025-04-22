using CommonTestUtilities.Entities;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Application.UseCases.Login;
using MyRecipeBook.Comunication.Requests;

namespace UseCases.Test.User.Register
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var password) = UserBuilder.Build();
            var useCase = CreateUseCase(user);
            var result = await useCase.Execute(new RequestLoginJson
            {
                Email = user.Email,
                Password = password

            });

            result.ShouldNotBeNull();
            result.Name.ShouldNotBeNullOrWhiteSpace();
            result.Name.ShouldBe(user.Name);
        }



        private static DoLoginUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user = null)
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var userReadOnlyRespositoryBuilder = new UserRepositoryBuilder();
            var accessToKenGenerator = JwtTokenGeneratorBuilder.Build();

            if (user is not null)
                userReadOnlyRespositoryBuilder.GetByEmailAndPassword(user);


            return new DoLoginUseCase(userReadOnlyRespositoryBuilder.Build(),  passwordEncripter, accessToKenGenerator);
        }
    }
}
