using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exception;

namespace MyRecipeBook.Application.UseCases.User.Update
{
    public interface IUpdateUserUseCase : IUseCaseWithRequest<RequestUpdateUserJson> { }

    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UpdateUserUseCase(ILoggedUser loggedUser, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task Execute(RequestUpdateUserJson request, CancellationToken cancellation = default)
        {
            var loggedUser = await _loggedUser.User();
            await Validate(request, loggedUser.Email);
            var user = await _userRepository.GetById(loggedUser.Id);
            user.Name = request.Name;
            user.Email = request.Email;
            _userRepository.Update(user);
            await _unitOfWork.Commit();
        }

        private async Task Validate(RequestUpdateUserJson request, string currentEmail)
        {
            var validator = new UpdateUserValidator();
            var result = validator.Validate(request);

            if (currentEmail.Equals(request.Email))
            {
                var userExist = await _userRepository.ExistActiveUserWithEmail(request.Email);
                if(userExist) 
                    result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessagesException.EMAIL_EMPTY));
            }

            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
