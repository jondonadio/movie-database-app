using System.Collections.Generic;
using System.Threading.Tasks;
using MovieDatabaseApp.Models;

namespace MovieDatabaseApp.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int movieId);
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int movieId);
    }
}
