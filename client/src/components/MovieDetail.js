import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../services/api';

const MovieDetail = () => {
  const { id } = useParams();
  const [movie, setMovie] = useState(null);

  useEffect(() => {
    api.get(`movies/${id}`)
      .then(response => setMovie(response.data))
      .catch(error => console.error('Error fetching movie:', error));
  }, [id]);

  return (
    <div>
      {movie ? (
        <div>
          <h1>{movie.title}</h1>
          <p>Genre: {movie.genre}</p>
          <p>Release Date: {new Date(movie.releaseDate).toLocaleDateString()}</p>
          <p>Rating: {movie.rating}</p>
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
};

export default MovieDetail;
