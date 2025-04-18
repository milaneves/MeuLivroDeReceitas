using CommonTestUtilities.Entities;
using MyRecipeBook.Application.UseCases.Login;
using MyRecipeBook.Comunication.Requests;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionBase;

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

        //[Fact]
        //public async Task Error_Invalid_User()
        //{
        //    var request = RequestLoginJsonBuilder.Build();
        //    var useCase = CreateUseCase();

        //    Func<Task> act = async () => { await useCase.Execute(request); };
        //    var exception = await Should.ThrowAsync<InvalidLoginException>(() => useCase.Execute(request));
        //    exception.Message.ShouldBe(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID);
        //}


        private static DoLoginUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user = null)
        {
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var userReadOnlyRespositoryBuilder = new UserRepositoryBuilder();

            if (user is not null)
                userReadOnlyRespositoryBuilder.GetByEmailAndPassword(user);


            return new DoLoginUseCase(userReadOnlyRespositoryBuilder.Build(), passwordEncripter);
        }
    }
}
