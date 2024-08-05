using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; // Import EF Core namespace for database context
using MovieDatabaseApp.Models; // Import the namespace for your data models

namespace MovieDatabaseApp.Data
{
    // The DbContext class for interacting with the SQL Server database
    public class MovieContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor to initialize the context with options
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        // DbSet for the Movie entity, represents the Movies table in the database
        public DbSet<Movie> Movies { get; set; }
    }
}
