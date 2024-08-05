import React, { useState, useEffect } from 'react';
import axios from 'axios'; // Import Axios for making HTTP requests
import { Link } from 'react-router-dom'; // Import Link for navigation

function MoviesList() {
  const [movies, setMovies] = useState([]); // State to store the list of movies

  useEffect(() => {
    const fetchMovies = async () => {
      try {
        const response = await axios.get('https://localhost:7168/api/movies'); // backend api endpoint
        setMovies(response.data); // Update state with fetched movies
      } catch (error) {
        console.error('There was an error fetching the movies!', error);
      }
    };

    fetchMovies();
  }, []);  // Empty dependency array means this effect runs once on component mount

  const handleDelete = async (id) => {
    try {
      await axios.delete(`https://localhost:7168/api/movies/${id}`);
      setMovies(movies.filter(movie => movie.id !== id));  // Remove deleted movie from the state
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
            <tr key={movie.id}>
              <td>{movie.title}</td>
              <td>{movie.genre}</td>
              <td>{movie.releaseDate}</td>
              <td>{movie.rating}</td>
              <td>
                <Link className="btn btn-primary btn-sm mr-2" to={`/edit/${movie.id}`}>Edit</Link>
                <button className="btn btn-danger btn-sm" onClick={() => handleDelete(movie.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default MoviesList;
