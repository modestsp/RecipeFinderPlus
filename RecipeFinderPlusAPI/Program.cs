using Microsoft.EntityFrameworkCore;
using RecipeFinderPlusAPI.Data;
using RecipeFinderPlusAPI.Services.Recipe;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IRecipeService, RecipeService>();

    var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

    builder.Services.AddHealthChecks();
}


// Add services to the container.

var app = builder.Build();
app.MapHealthChecks("/health");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
