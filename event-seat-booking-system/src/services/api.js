import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:5001/api', // Update to your backend API base URL
    headers: {
        'Content-Type': 'application/json',
    },
});

export default api;
