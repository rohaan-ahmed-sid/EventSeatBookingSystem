import { useEffect, useState } from 'react';
import api from '../../services/api';

const AdminBookings = () => {
    const [bookings, setBookings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        setLoading(true);
        api.getAdminBookings()
            .then(data => {
                setBookings(data);
                setLoading(false);
            })
            .catch(err => {
                console.error('Failed to load bookings', err);
                setError('Failed to load bookings');
                setLoading(false);
            });
    }, []);

    if (loading) return <div className="text-center p-8">Loading bookings...</div>;
    if (error) return <div className="text-center p-8 text-red-500">{error}</div>;

    return (
        <div className="p-6">
            <h2 className="text-xl font-bold mb-4">All Bookings</h2>
            <table className="w-full table-auto border">
                <thead className="bg-gray-200">
                    <tr>
                        <th className="px-4 py-2 border">ID</th>
                        <th className="px-4 py-2 border">Event</th>
                        <th className="px-4 py-2 border">User</th>
                        <th className="px-4 py-2 border">Seat</th>
                        <th className="px-4 py-2 border">Date</th>
                        <th className="px-4 py-2 border">Status</th>
                        <th className="px-4 py-2 border">Amount</th>
                    </tr>
                </thead>
                <tbody>
                    {bookings.length > 0 ? (
                        bookings.map(booking => (
                            <tr key={booking.BookingId} className="text-center">
                                <td className="border px-4 py-2">{booking.BookingId}</td>
                                <td className="border px-4 py-2">{booking.EventName}</td>
                                <td className="border px-4 py-2">{booking.UserId}</td>
                                <td className="border px-4 py-2">{booking.SeatNumber}</td>
                                <td className="border px-4 py-2">{new Date(booking.BookingDate).toLocaleDateString()}</td>
                                <td className="border px-4 py-2">{booking.Status}</td>
                                <td className="border px-4 py-2">${booking.TotalAmount}</td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="7" className="border px-4 py-2 text-center">No bookings found</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};

export default AdminBookings;