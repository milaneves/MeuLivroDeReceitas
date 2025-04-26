using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyRecipeBookDbContext _dbContext;

        public UserRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(User user, CancellationToken cancellationToken)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistActiveUserWithEmail(string email)
            => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && 
            user.Active);

        public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier)
            => await _dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) &&
            user.Active);

        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
           return await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email)
                    && user.Password.Equals(password));  
        }
    }
}
