import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:5001/api', // Update to your backend API base URL
    headers: {
        'Content-Type': 'application/json',
    },
});

const getDecorSuggestions = async (eventName, theme) => {
    try {
        const response = await api.post('/AI/DecorSuggestions', {
            eventName: eventName,
            theme: theme
        });

        // Display decor suggestions
        console.log(response.data);
    } catch (error) {
        console.error("Error getting decor suggestions:", error);
    }
};


export default api;
