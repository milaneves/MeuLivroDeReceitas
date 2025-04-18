namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase();
            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
        }

        private RegisterUserUseCase CreateUseCase(string? email = null)
        {
            var mapper = MapperBuilder.Build();
            var passwordEncripter = PasswordEncripterBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var userRepository = new UserRepositoryBuilder().Build();
           
            if (string.IsNullOrEmpty(email) == false)
                userRepository.ExistActiveUserWithEmail(email);

            return new RegisterUserUseCase(mapper, unitOfWork, userRepository, passwordEncripter);
        }
    }
}
