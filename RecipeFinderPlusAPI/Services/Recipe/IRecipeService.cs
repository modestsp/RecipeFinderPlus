using RecipeFinderPlusAPI.Models;
namespace RecipeFinderPlusAPI.Services.Recipe;

public interface IRecipeService
{
    Task<Models.Recipe> GetRecipeByIdAsync(int id);
    Task<ICollection<Models.Recipe>> GetTrendingRecipesAsync();
    void AddRecipe(Models.Recipe recipe);
    Task<Models.Recipe> UpdateRecipeAsync(Models.Recipe recipe);
    Task<bool> SaveChangesAsync();
}