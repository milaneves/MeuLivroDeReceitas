using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories
{
    public class UserRepositoryBuilder
    {
        private readonly Mock<IUserRepository> _userRepository;

        public UserRepositoryBuilder() => _userRepository = new Mock<IUserRepository>();

        public void ExistActiveUserWithEmail(string email)
        {
            _userRepository
                .Setup(repository => repository.ExistActiveUserWithEmail(email))
                .ReturnsAsync(true);
        }

        public IUserRepository Build()
        {
           return _userRepository.Object;
        }
    }
}
