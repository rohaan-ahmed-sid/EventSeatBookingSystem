import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import api from '../services/api';
import { useNavigate } from 'react-router-dom';
import toast from 'react-hot-toast';
import { QRCodeCanvas } from 'qrcode.react';

const EventDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [event, setEvent] = useState(null);
    const [seats, setSeats] = useState([]); // available seats
    const [selected, setSelected] = useState([]); // user selected
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        api.get(`/events/${id}`)
            .then(res => {
                setEvent(res.data);
                // Mock seat data for now
                const totalSeats = 30;
                const mockSeats = Array.from({ length: totalSeats }, (_, i) => ({
                    number: i + 1,
                    isBooked: false,
                }));
                setSeats(mockSeats);
            })
            .catch(err => console.error('Error fetching event', err))
            .finally(() => setLoading(false));
    }, [id]);

    const toggleSeat = (seatNum) => {
        setSelected(prev =>
            prev.includes(seatNum)
                ? prev.filter(n => n !== seatNum)
                : [...prev, seatNum]
        );
    };

    const bookSeats = () => {
        if (selected.length === 0) return alert('Select at least one seat.');
        const payload = {
            eventId: id,
            seats: selected,
            userId: "user123" // replace with actual logged-in user id
        };
        api.post('/bookings', payload)
            .then(() => {
                alert('Booking Confirmed!');
                navigate('/dashboard'); // redirect after booking
            })
            .catch(err => {
                console.error('Booking error', err);
                alert('Booking failed.');
            });
    };

    const [recommendedSeats, setRecommendedSeats] = useState([]);
    const [showAI, setShowAI] = useState(false);

    // Fetch AI recommendations when user requests
    const selectBestSeats = async (eventId, seatPreferences) => {
        try {
            const response = await api.post('/SeatSelection/SelectSeats', {
                eventId: eventId,
                ...seatPreferences
            });

            // Process response (best seats)
            console.log(response.data);
        } catch (error) {
            console.error("Error selecting seats:", error);
        }
    };


    if (loading) return <p>Loading...</p>;
    if (!event) return <p className="text-red-500">Event not found.</p>;

    return (
        <div>
            <h1 className="text-2xl font-bold mb-4">{event.title}</h1>

            <div className="my-4">
                <h2 className="text-lg font-semibold mb-2">Select Your Seats</h2>
                <div className="grid grid-cols-6 gap-2">
                    {seats.map(seat => (
                        <div
                            key={seat.number}
                            onClick={() => !seat.isBooked && toggleSeat(seat.number)}
                            className={`
                border rounded p-2 text-center cursor-pointer
                ${seat.isBooked ? 'bg-gray-300 cursor-not-allowed' : ''}
                ${selected.includes(seat.number) ? 'bg-green-500 text-white' : 'bg-white'}
              `}
                        >
                            {seat.number}
                        </div>
                    ))}
                    <button
                        onClick={fetchAIRecommendations}
                        className="mt-4 bg-indigo-600 text-white px-3 py-2 rounded hover:bg-indigo-700"
                    >
                        Get AI Recommended Seats
                    </button>

                    {showAI && recommendedSeats.length > 0 && (
                        <div className="mt-4 p-4 bg-green-50 border border-green-300 rounded">
                            <h3 className="text-lg font-semibold mb-2">Recommended Seats by AI</h3>
                            <p>{recommendedSeats.join(', ')}</p>
                        </div>
                    )}
                </div>
            </div>

            <button
                onClick={bookSeats}
                className="mt-4 bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
            >
                Confirm Booking
            </button>
            toast.success('Booking confirmed!');
            toast.error('Something went wrong.');
        </div>
    );
};
