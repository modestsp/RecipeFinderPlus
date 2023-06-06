using RecipeFinderPlusAPI.Models;
namespace RecipeFinderPlusAPI.Services.Recipe;

public interface IRecipeService
{
    Task<ICollection<Models.Recipe>> GetTrendingRecipesAsync();
    void AddRecipe(Models.Recipe recipe);
    Task<Models.Recipe> UpdateRecipeAsync(Models.Recipe recipe);
    Task<bool> SaveChangesAsync();
}