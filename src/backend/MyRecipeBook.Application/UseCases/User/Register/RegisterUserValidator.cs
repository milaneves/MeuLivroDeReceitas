namespace MyRecipeBook.Application.UseCases.User.Register
{
    public  class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator() 
        {
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6);
        }
    }
}
