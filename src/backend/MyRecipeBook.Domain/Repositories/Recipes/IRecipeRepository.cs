namespace MyRecipeBook.Domain.Repositories.Recipes
{
    public interface IRecipeRepository
    {
        Task Add(Recipe recipe);
    }
}
