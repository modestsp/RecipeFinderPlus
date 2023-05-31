using Microsoft.EntityFrameworkCore;
using RecipeFinderPlusAPI.Models;

namespace RecipeFinderPlusAPI.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Recipe>? Recipes { get; set; }
}