namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);
        public Task<Entities.User?> GetByEmailAndPassword(string email, string password);
        public Task Add(Entities.User user, CancellationToken cancellationToken);
    }
}
