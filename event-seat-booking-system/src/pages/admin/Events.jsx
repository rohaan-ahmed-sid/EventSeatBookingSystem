import { useEffect, useState } from 'react';
import api from '../../services/api';

const AdminEvents = () => {
    const [events, setEvents] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        setLoading(true);
        api.getAdminEvents()
            .then(data => {
                setEvents(data);
                setLoading(false);
            })
            .catch(err => {
                console.error('Failed to load events', err);
                setError('Failed to load events');
                setLoading(false);
            });
    }, []);

    if (loading) return <div className="text-center p-8">Loading events...</div>;
    if (error) return <div className="text-center p-8 text-red-500">{error}</div>;

    return (
        <div className="p-6">
            <h2 className="text-xl font-bold mb-4">All Events</h2>
            <table className="w-full table-auto border">
                <thead className="bg-gray-200">
                    <tr>
                        <th className="px-4 py-2 border">ID</th>
                        <th className="px-4 py-2 border">Name</th>
                        <th className="px-4 py-2 border">Date</th>
                        <th className="px-4 py-2 border">Venue</th>
                        <th className="px-4 py-2 border">Available Seats</th>
                        <th className="px-4 py-2 border">Price</th>
                    </tr>
                </thead>
                <tbody>
                    {events.length > 0 ? (
                        events.map(event => (
                            <tr key={event.EventId} className="text-center">
                                <td className="border px-4 py-2">{event.EventId}</td>
                                <td className="border px-4 py-2">{event.Name}</td>
                                <td className="border px-4 py-2">{new Date(event.EventDate).toLocaleDateString()}</td>
                                <td className="border px-4 py-2">{event.Venue}</td>
                                <td className="border px-4 py-2">{event.AvailableSeats} / {event.TotalSeats}</td>
                                <td className="border px-4 py-2">${event.TicketPrice}</td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="6" className="border px-4 py-2 text-center">No events found</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};

export default AdminEvents;