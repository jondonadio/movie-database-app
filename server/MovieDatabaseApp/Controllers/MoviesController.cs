using Microsoft.AspNetCore.Mvc;
using MovieDatabaseApp.Models;
using Microsoft.EntityFrameworkCore;
using MovieDatabaseApp.Data;
using MovieDatabaseApp.Interfaces;

namespace MovieDatabaseApp.Controllers
{
    // Controller responsible for handling HTTP requests related to movies.
    [Route("api/[controller]")] // Sets the base route for all actions in this controller
    [ApiController] // Marks class as an API controller
    public class MoviesController : ControllerBase
    {
        // Dependency injection of the repository
        private readonly IMovieRepository _context; 

        // Constructor to inject the MovieContext dependency.
        public MoviesController(IMovieRepository context)
        {
            // Assigns the injected context to a private field
            _context = context; 
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _context.GetAllMoviesAsync();
            return Ok(movies);
        }

        // Maps this method to HTTP GET with a route parameter
        [HttpGet("{movieId}")]        
        public async Task<ActionResult<Movie>> GetMovie(int MovieId)
        {
            var movie = await _context.GetMovieByIdAsync(MovieId);

            if (movie == null)
            {
                // Returns a 404 Not Found response
                return NotFound(); 
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {

            await _context.AddMovieAsync(movie);

            // Returns a 201 Created response
            return CreatedAtAction(nameof(GetMovie), new { movieId = movie.MovieId }, movie);
        }

        [HttpPut("{MovieId}")]
        public async Task<ActionResult> PutMovie(int MovieId, Movie movie)
        {
            if (MovieId != movie.MovieId)
            {
                // Returns a 400 Bad Request response
                return BadRequest(); 
            }

            await _context.UpdateMovieAsync(movie);
            // Returns a 204 No Content response indicating success   
            return NoContent();
        }

        [HttpPost("delete/{MovieId}")]
        public async Task<IActionResult> DeleteMovie(int MovieId)
        {
            var movie = await _context.GetMovieByIdAsync(MovieId);
            if (movie == null)
            {
                return NotFound();
            }

            await _context.DeleteMovieAsync(MovieId);
            return NoContent();
        }

    }
}
