import React, { useState, useEffect } from 'react';
import axios from 'axios'; 
import { Link } from 'react-router-dom';

function MoviesList() {
  const [movies, setMovies] = useState([]);

  // provide empty array param so the hook only executes on component mount
  useEffect(() => {
    const fetchMovies = async () => {
      try {
        // Make axios GET request to backend api get all movies endpoint
        const response = await axios.get('https://jonmoviedatabaseapp.azurewebsites.net/api/movies'); 
        // Update component state with fetched movies
        setMovies(response.data); 
      } catch (error) {
        console.error('There was an error fetching the movies!', error);
      }
    };
    fetchMovies();
  }, []);

  const handleDelete = async (movieId) => {
    try {
      // Make axios POST request to backend api delete movie endpoint
      await axios.post(`https://jonmoviedatabaseapp.azurewebsites.net/api/movies/delete/${movieId}`);
      // Remove deleted movie from the state
      setMovies(movies.filter(movie => movie.movieId !== movieId)); 
    } catch (error) {
      console.error('There was an error deleting the movie!', error);
    }
  };

  return (
    <div>
      <h2>Movies List</h2>
      <table className="table">
        <thead>
          <tr>
            <th>Title</th>
            <th>Genre</th>
            <th>Release Date</th>
            <th>Rating</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {movies.map(movie => (
            <tr key={movie.movieId}>
              <td>{movie.title}</td>
              <td>{movie.genre}</td>
              <td>{movie.releaseDate}</td>
              <td>{movie.rating}</td>
              <td>
                <Link className="btn btn-primary btn-sm mr-2" to={`/edit/${movie.movieId}`}>Edit</Link>
                <button className="btn btn-danger btn-sm" onClick={() => handleDelete(movie.movieId)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default MoviesList;
