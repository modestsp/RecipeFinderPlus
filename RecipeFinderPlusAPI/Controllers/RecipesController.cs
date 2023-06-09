using Microsoft.AspNetCore.Mvc;
using RecipeFinderPlusAPI.Models;
using RecipeFinderPlusAPI.Services.Recipe;

namespace RecipeFinderPlusAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingRecipes()
        {
            var recipes = await _recipeService.GetTrendingRecipesAsync();
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertRecipe(CreateRecipeRequest request)
        {
            Console.WriteLine(request.Id.GetType());
            // Todo: Search the recipe in db
            var existingRecipe = await _recipeService.GetRecipeByIdAsync(request.Id);
            // Todo: If not found, add the recipe to db
            if (existingRecipe == null)
            {
                _recipeService.AddRecipe(new Recipe
                {
                    Id = request.Id,
                    Title = request.Title,
                    Image = request.Image,
                    Likes = 1
                });
                await _recipeService.SaveChangesAsync();
                return Ok();
            }
            // Todo: if found, update the number of likes
            await _recipeService.UpdateRecipeAsync(existingRecipe);
            await _recipeService.SaveChangesAsync();
            return Ok();
        }
    }

}