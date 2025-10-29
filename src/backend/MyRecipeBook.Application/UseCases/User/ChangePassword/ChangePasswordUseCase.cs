using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exception;

namespace MyRecipeBook.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase : IUseCaseWithRequest<RequestChangePasswordJson> { }
    
    public sealed class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        

        public ChangePasswordUseCase(
            ILoggedUser loggedUser,
            IPasswordEncripter passwordEncripter,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _passwordEncripter = passwordEncripter;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestChangePasswordJson request, CancellationToken cancellationToken = default)
        {
            var loggedUser = await _loggedUser.User();
            Validate(request, loggedUser);
            var user = await _userRepository.GetById(loggedUser.Id);
            user.Password = _passwordEncripter.Encrypt(request.NewPassword);
            _userRepository.Update(user);
            await _unitOfWork.Commit();

        }

        private void Validate(RequestChangePasswordJson reqeuest, Domain.Entities.User loggedUser)
        {
            var result = new ChangePasswordValidator().Validate(reqeuest);
            var currentPasswordEncripted = _passwordEncripter.Encrypt(reqeuest.Password);

            if(!currentPasswordEncripted.Equals(loggedUser.Password))
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD));

            if (!result.IsValid)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }

    }

}
