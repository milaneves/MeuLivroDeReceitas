namespace MyRecipeBook.Domain.Repositories.User
{
    public interface IUserRepository
    {
        Task<bool> ExistActiveUserWithEmail(string email);
        Task<Entities.User?> GetByEmailAndPassword(string email, string password);
        Task Add(Entities.User user, CancellationToken cancellationToken);
        Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
        Task<Entities.User> GetById(long id);
        void Update(Entities.User user);
    }
}
