import { useEffect, useState } from 'react';
import api from '../services/api';
import { QRCodeCanvas } from 'qrcode.react';


const Dashboard = () => {
    const [bookings, setBookings] = useState([]);
    const [loading, setLoading] = useState(true);
    const userId = "user123"; // replace this with actual logged-in user

    useEffect(() => {
        api.get(`/users/${userId}/bookings`)
            .then(res => setBookings(res.data))
            .catch(err => console.error('Error fetching bookings', err))
            .finally(() => setLoading(false));
    }, []);

    if (loading) return <p className="text-center">Loading your bookings...</p>;

    return (
        <div>
            <h1 className="text-2xl font-bold mb-4">My Bookings</h1>
            {bookings.length === 0 ? (
                <p className="text-gray-600">You haven't booked any events yet.</p>
            ) : (
                <div className="space-y-4">
                    {bookings.map((b, i) => (
                        <div key={i} className="border p-4 rounded shadow-sm bg-white">
                            <h2 className="text-xl font-semibold">{b.event.title}</h2>
                            <p className="text-gray-600">{b.event.date} @ {b.event.location}</p>
                            <p className="mt-1">Seats: {b.seats.join(', ')}</p>
                            <p className="text-sm text-gray-500">Booked on {new Date(b.bookingDate).toLocaleString()}</p>
                            <div className="mt-4 text-center">
                                <QRCodeCanvas
                                    value={JSON.stringify({
                                        bookingId: b.bookingId,
                                        event: b.event.title,
                                        seats: b.seats
                                    })}
                                    size={128}
                                    bgColor={"#ffffff"}
                                    fgColor={"#000000"}
                                    level={"H"}
                                    includeMargin={true}
                                />
                                <p className="text-xs text-gray-500 mt-1">Show this QR at the venue</p>
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default Dashboard;
