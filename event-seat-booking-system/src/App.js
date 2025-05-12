import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import MainLayout from './layouts/MainLayout';
import Home from './pages/Home';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import EventDetails from './pages/EventDetails';
import AdminDashboard from './pages/admin/AdminDashboard';
import AdminUsers from './pages/admin/Users';
import AdminEvents from './pages/admin/Events';
import AdminBookings from './pages/admin/Bookings';
import { Toaster } from 'react-hot-toast';
import { useEffect } from 'react';

function App() {
    return (
        <>
            <Router>
                <Routes>
                    <Route path="/" element={<MainLayout />}>
                        <Route index element={<Home />} />
                        <Route path="login" element={<Login />} />
                        <Route path="dashboard" element={<Dashboard />} />
                        <Route path="event/:id" element={<EventDetails />} />
                        <Route path="/admin" element={<AdminDashboard />} />
                        <Route path="/admin/users" element={<AdminUsers />} />
                        <Route path="/admin/events" element={<AdminEvents />} />
                        <Route path="/admin/bookings" element={<AdminBookings />} />
                    </Route>
                </Routes>
            </Router>
            <Toaster position="top-center" />
        </>
    );
}

export default App;

