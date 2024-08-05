import axios from 'axios';

// Create an Axios instance with the base URL of your API
const api = axios.create({
  baseURL: 'https://localhost:7168/api/', // Adjust if necessary
});

export default api;
