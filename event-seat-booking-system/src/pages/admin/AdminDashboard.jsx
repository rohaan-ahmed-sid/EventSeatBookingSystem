import { Link } from 'react-router-dom';

const AdminDashboard = () => {
    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold mb-4">Admin Panel</h1>
            <div className="space-y-4">
                <Link to="/admin/users" className="block bg-gray-100 p-4 rounded hover:bg-gray-200">Manage Users</Link>
                <Link to="/admin/events" className="block bg-gray-100 p-4 rounded hover:bg-gray-200">Manage Events</Link>
                <Link to="/admin/bookings" className="block bg-gray-100 p-4 rounded hover:bg-gray-200">Manage Bookings</Link>
            </div>
        </div>
    );
};

export default AdminDashboard;
