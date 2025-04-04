namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserRepository
    {
        public Task Add(Entities.User user, CancellationToken cancellationToken);
        public Task<bool> ExistActiveUserWithEmail(string email);
    }
}
