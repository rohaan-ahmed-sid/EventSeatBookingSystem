import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import api from '../services/api';

const Home = () => {
    const [events, setEvents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        console.log('Fetching events...');
        api.getEvents()
            .then(data => {
                console.log('Events data received:', data);
                setEvents(data);
                setLoading(false);
            })
            .catch(err => {
                console.error('Error fetching events:', err);
                setError('Failed to load events: ' + err.message);
                setLoading(false);
            });
    }, []);

    if (loading) return <div className="text-center p-8">Loading events...</div>;
    if (error) return <div className="text-center p-8 text-red-500">{error}</div>;

    return (
        <div className="container mx-auto p-4">
            <h1 className="text-3xl font-bold mb-6">Upcoming Events</h1>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                {events.length > 0 ? (
                    events.map(event => (
                        <div key={event.EventId} className="bg-white rounded-lg shadow-md overflow-hidden">
                            {event.ImageUrl && (
                                <img
                                    src={event.ImageUrl}
                                    alt={event.Name}
                                    className="w-full h-48 object-cover"
                                />
                            )}
                            <div className="p-4">
                                <h2 className="text-xl font-semibold mb-2">{event.Name}</h2>
                                <p className="text-gray-600 mb-2">{new Date(event.EventDate).toLocaleDateString()}</p>
                                <p className="text-gray-600 mb-2">{event.Venue}</p>
                                <p className="mb-4">{event.Description.substring(0, 100)}...</p>
                                <div className="flex justify-between items-center">
                                    <span className="font-bold">${event.TicketPrice}</span>
                                    <Link
                                        to={`/event/${event.EventId}`}
                                        className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                                    >
                                        View Details
                                    </Link>
                                </div>
                            </div>
                        </div>
                    ))
                ) : (
                    <p className="col-span-3 text-center text-gray-500">No events available at the moment.</p>
                )}
            </div>
        </div>
    );
};

export default Home;