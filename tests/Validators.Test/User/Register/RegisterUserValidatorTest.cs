namespace Validators.Test.User.Register
{
    public class RegisterUserValidatorTest
    {
        [Fact]
        public void WhenRegisterUserIsSuccess()
        {
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            var result = validator.Validate(request);
            result.ShouldNotBeNull();
        }

        [Fact]
        public void Error_Name_Empty()
        {   
            //Arrange
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;
            
            //Act
            var result = validator.Validate(request);
            
            //Assert
            result.IsValid.ShouldBeFalse();
            result.Errors
                .ShouldContain(x => x.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
        }

        [Fact]
        public void Error_Email_Empty()
        {
            //Arrange
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = string.Empty;

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.ShouldBeFalse();
            result.Errors
                .ShouldContain(x => x.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
        }

        [Fact]
        public void Error_Email_Invalid()
        {
            //Arrange
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Email = "mila.com";

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.ShouldBeFalse();
            result.Errors
                .ShouldContain(x => x.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        public void Error_Password_Invalid(int passwordLength)
        {
            //Arrange
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.ShouldBeFalse();
            result.Errors
                .ShouldContain(x => x.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_INVALID));
        }
    }
}
