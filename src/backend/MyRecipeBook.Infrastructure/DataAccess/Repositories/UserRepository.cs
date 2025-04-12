using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

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

        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
           return await _dbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email)
                    && user.Password.Equals(password));  
        }
    }
}
