import { Link } from 'react-router-dom';

const Navbar = () => {
    return (
        <nav className="bg-gray-800 text-white p-4">
            <div className="container mx-auto flex justify-between items-center">
                <Link to="/" className="text-xl font-bold">Event Seat Booking System</Link>
                <div className="space-x-4">
                    <Link to="/" className="hover:text-gray-300">Home</Link>
                    <Link to="/admin" className="hover:text-gray-300">Admin</Link>
                    <Link to="/admin/events" className="hover:text-gray-300">Events</Link>
                    <Link to="/admin/bookings" className="hover:text-gray-300">Bookings</Link>
                </div>
            </div>
        </nav>
    );
};

export default Navbar;