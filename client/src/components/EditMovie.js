import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';

function EditMovie() {
  const { movieId } = useParams();
  const [title, setTitle] = useState('');
  const [genre, setGenre] = useState('');
  const [releaseDate, setReleaseDate] = useState('');
  const [rating, setRating] = useState('');

  useEffect(() => {
    const fetchMovie = async () => {
      try {
        const response = await axios.get(`https://localhost:7168/api/movies/${movieId}`);
        const movie = response.data;
        setTitle(movie.title);
        setGenre(movie.genre);
        setReleaseDate(movie.releaseDate);
        setRating(movie.rating);
      } catch (error) {
        console.error('There was an error fetching the movie!', error);
      }
    };

    fetchMovie();
  }, [movieId]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const updatedMovie = { id, title, genre, releaseDate, rating };

    try {
      await axios.put(`https://localhost:7168/api/movies/${id}`, updatedMovie);
      alert('Movie updated successfully');
    } catch (error) {
      console.error('There was an error updating the movie!', error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="form-group">
        <label>Title</label>
        <input
          type="text"
          className="form-control"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          required
        />
      </div>
      <div className="form-group">
        <label>Genre</label>
        <input
          type="text"
          className="form-control"
          value={genre}
          onChange={(e) => setGenre(e.target.value)}
          required
        />
      </div>
      <div className="form-group">
        <label>Release Date</label>
        <input
          type="date"
          className="form-control"
          value={releaseDate}
          onChange={(e) => setReleaseDate(e.target.value)}
          required
        />
      </div>
      <div className="form-group">
        <label>Rating</label>
        <input
          type="number"
          className="form-control"
          value={rating}
          onChange={(e) => setRating(e.target.value)}
          required
        />
      </div>
      <button type="submit" className="btn btn-primary">Update Movie</button>
    </form>
  );
}

export default EditMovie;
