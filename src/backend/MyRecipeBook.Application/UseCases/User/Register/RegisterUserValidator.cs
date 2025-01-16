using MyRecipeBook.Exception;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public  class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator() 
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessageException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("");
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6);
        }
    }
}
