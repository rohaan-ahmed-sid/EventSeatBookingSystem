import { useEffect, useState } from 'react';
import api from '../services/api';
import { Link } from 'react-router-dom';

const Home = () => {
    const [events, setEvents] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        api.get('/events')
            .then(res => setEvents(res.data))
            .catch(err => console.error('Failed to fetch events', err))
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <p className="text-center">Loading events...</p>;

    return (
        <div>
            <h1 className="text-2xl font-bold mb-4">Available Events</h1>
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
                {events.map(event => (
                    <div key={event.id} className="border p-4 rounded shadow hover:shadow-md transition">
                        <h2 className="text-xl font-semibold">{event.title}</h2>
                        <p className="text-gray-600">{event.date} – {event.location}</p>
                        <Link to={`/event/${event.id}`} className="text-blue-500 hover:underline mt-2 inline-block">
                            View Details
                        </Link>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Home;
