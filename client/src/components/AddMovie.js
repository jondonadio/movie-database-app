import React, { useState } from 'react';
import axios from 'axios';

function AddMovie() {
  const [title, setTitle] = useState('');
  const [genre, setGenre] = useState('');
  const [releaseDate, setReleaseDate] = useState('');
  const [rating, setRating] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    const newMovie = { title, genre, releaseDate, rating };

    try {
      await axios.post('https://jonmoviedatabaseapp.azurewebsites.net/api/movies', newMovie);
      alert('Movie added successfully');
    } catch (error) {
      console.error('There was an error adding the movie!', error);
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
      <button type="submit" className="btn btn-primary">Add Movie</button>
    </form>
  );
}

export default AddMovie;
