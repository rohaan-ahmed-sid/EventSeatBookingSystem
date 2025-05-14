import axios from 'axios';

const api = axios.create({
    baseURL: '', // Leave blank to use the proxy in package.json
    headers: {
        'Content-Type': 'application/json',
    },
});

export const getDecorSuggestionImage = async (eventName, theme) => {
    try {
        const response = await api.post('/api/AI/DecorSuggestionsImage', {
            EventName: eventName,
            Theme: theme
        });
        return response.data.imageUrl;
    } catch (error) {
        console.error("Error getting decor suggestion image:", error);
        throw error;
    }
};

export default api;