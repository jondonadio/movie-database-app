using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieDatabaseApp.Models;

namespace MovieDatabaseApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Creates a new instance of MovieContext with the service provider's options
            using var context = new MovieContext(serviceProvider.GetRequiredService<DbContextOptions<MovieContext>>());
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            // Clear the Movies table
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Movies]");

            if (context.Movies.Any())
            {
                logger.LogInformation("Database already seeded.");
                return;   // DB has been seeded
            }

            try
            {
                var jsonString = File.ReadAllText("movies.json");
                // Deserializes the JSON data into a list of Movie objects
                var movies = JsonSerializer.Deserialize<List<Movie>>(jsonString);
                // Adds the movies to the context
                context.Movies.AddRange(movies);
                // Saves the changes to the database
                context.SaveChanges();
                logger.LogInformation("Database seeded successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
