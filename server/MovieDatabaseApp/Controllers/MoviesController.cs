using Microsoft.AspNetCore.Mvc; // Import the ASP.NET Core MVC namespace for controller functionality
using MovieDatabaseApp.Models; // Import the namespace for your data models
using Microsoft.EntityFrameworkCore; // Import the EF Core namespace for database operations
using MovieDatabaseApp.Data; // Import the namespace for your database context

namespace MovieDatabaseApp.Controllers
{
    // Controller responsible for handling HTTP requests related to movies.
    [Route("api/[controller]")] // Sets the base route for all actions in this controller
    [ApiController] // Marks this class as an API controller
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context; // Dependency injection of the database context

        // Constructor to inject the MovieContext dependency.
        public MoviesController(MovieContext context)
        {
            _context = context; // Assigns the injected context to a private field
        }

        // GET api/Moviess
        [HttpGet] // Maps this method to HTTP GET
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            // Retrieves all movies from the database asynchronously
            return await _context.Movies.ToListAsync();
        }

        // GET api/Movies/5
        [HttpGet("{id}")] // Maps this method to HTTP GET with a route parameter
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            // Finds a movie by its ID asynchronously
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound(); // Returns a 404 Not Found response
            }

            return movie;
        }

        // POST api/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);  // Adds the new movie to the context
            await _context.SaveChangesAsync();

            // Returns a 201 Created response
            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // PUT api/Movies/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest(); // Returns a 400 Bad Request response
            }

            _context.Entry(movie).State = EntityState.Modified; // Marks the entity as modified

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent(); // Returns a 204 No Content response indicating success
        }

        // DELETE api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie); // Removes the movie from the context
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
