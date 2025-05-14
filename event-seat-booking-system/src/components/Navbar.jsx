import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Toaster } from 'react-hot-toast';
import MainLayout from './layouts/MainLayout';
import Home from './pages/Home';
import AdminDashboard from './pages/admin/AdminDashboard';
import AdminEvents from './pages/admin/Events';
import AdminBookings from './pages/admin/Bookings';

function App() {
    return (
        <BrowserRouter>
            <Toaster position="top-right" />
            <Routes>
                <Route path="/" element={<MainLayout />}>
                    <Route index element={<Home />} />

                    {/* Admin Routes */}
                    <Route path="admin" element={<AdminDashboard />} />
                    <Route path="admin/events" element={<AdminEvents />} />
                    <Route path="admin/bookings" element={<AdminBookings />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

export default App;