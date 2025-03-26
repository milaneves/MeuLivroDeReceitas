using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Infrastructure.DataAccess;

namespace MyRecipeBook.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyRecipeBookDbContext _dbContext;

        public UnitOfWork(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
