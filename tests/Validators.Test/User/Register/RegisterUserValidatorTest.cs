using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exception;
using Shouldly;

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
                .ShouldContain(x => x.ErrorMessage.Equals(ResourceMessageException.NAME_EMPTY));
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
                .ShouldContain(x => x.ErrorMessage.Equals(ResourceMessageException.EMAIL_EMPTY));
        }
    }
}
