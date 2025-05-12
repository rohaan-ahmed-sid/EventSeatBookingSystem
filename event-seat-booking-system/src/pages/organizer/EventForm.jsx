import { useState } from 'react';
import api from '../../services/api';

const EventForm = () => {
  const [themeInput, setThemeInput] = useState('');
  const [suggestions, setSuggestions] = useState('');

  const getDecorSuggestions = () => {
    api.post('/events/decor-suggestions', { theme: themeInput })
      .then(res => setSuggestions(res.data.suggestions))
      .catch(err => {
        console.error('AI decor error', err);
        alert('Failed to get decor suggestions.');
      });
  };

  return (
    <div>
      <h1 className="text-2xl font-bold mb-4">Event Decor AI Assistant</h1>

      <input
        type="text"
        value={themeInput}
        onChange={e => setThemeInput(e.target.value)}
        placeholder="Enter theme or event type..."
        className="border p-2 rounded w-full mb-2"
      />

      <button
        onClick={getDecorSuggestions}
        className="bg-purple-600 text-white px-4 py-2 rounded hover:bg-purple-700"
      >
        Generate Decor Ideas
      </button>

      {suggestions && (
        <div className="mt-4 p-4 bg-violet-50 border border-violet-300 rounded">
          <h3 className="font-semibold">AI Suggestions:</h3>
          <p>{suggestions}</p>
        </div>
      )}
    </div>
  );
};

export default EventForm;
