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
            => await _dbContext.Users.AddAsync(user, cancellationToken);

        public async Task<bool> ExistActiveUserWithEmail(string email, CancellationToken cancellationToken)
            => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && 
            user.Active, cancellationToken);
 
    }
}
