using Microsoft.EntityFrameworkCore;
using RecipeFinderPlusAPI.Data;
using RecipeFinderPlusAPI.Services.Recipe;
using System;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IRecipeService, RecipeService>();

    string connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

    // builder.Services.AddCors(options =>
    // {
    //     options.AddPolicy("AllowAnyOrigin", builder =>
    //     {
    //         builder
    //         .AllowAnyOrigin()
    //         .AllowAnyMethod()
    //         .AllowAnyHeader();
    //     });
    // });
    builder.Services.AddHealthChecks();
}


// Add services to the container.

var app = builder.Build();

// Apply database migrations
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.MigrateAsync();

app.MapHealthChecks("/health");
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseStaticFiles();

// app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();
