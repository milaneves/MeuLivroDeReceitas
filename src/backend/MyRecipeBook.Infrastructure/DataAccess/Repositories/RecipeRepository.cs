using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Recipes;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly MyRecipeBookDbContext _dbContext;

        public RecipeRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(Recipe recipe) => await _dbContext.AddAsync(recipe);
    }
}
