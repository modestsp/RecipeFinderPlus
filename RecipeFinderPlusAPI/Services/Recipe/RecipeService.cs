
using Microsoft.EntityFrameworkCore;
using RecipeFinderPlusAPI.Data;

namespace RecipeFinderPlusAPI.Services.Recipe;

public class RecipeService : IRecipeService
{
    private readonly AppDbContext _context;

    public RecipeService(AppDbContext context)
    {
        _context = context ??
            throw new ArgumentNullException(nameof(context));
    }

    public async Task<Models.Recipe> GetRecipeByIdAsync(int id)
    {
        return await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<ICollection<Models.Recipe>> GetTrendingRecipesAsync()
    {
        return await _context.Recipes.OrderByDescending(r => r.Likes).Take(10).ToListAsync();
    }

    public void AddRecipe(Models.Recipe recipeToAdd)
    {
        if (recipeToAdd == null)
        {
            throw new ArgumentNullException(nameof(recipeToAdd));
        }

        _context.Recipes.Add(recipeToAdd);
    }

    public async Task<Models.Recipe> UpdateRecipeAsync(Models.Recipe recipe)
    {
        var existingRecipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipe.Id);
        existingRecipe.Likes++;
        return existingRecipe;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}