const handleResponse = async (response) => {
    if (!response.ok) {
        const error = await response.json().catch(() => ({
            message: response.statusText,
        }));
        throw new Error(error.message || 'Something went wrong');
    }
    return response.json();
};

const api = {
    // Events
    getEvents: () =>
        fetch('https://localhost:44319/api/events')
            .then(response => {
                console.log('Events API response:', response);
                return handleResponse(response);
            })
            .catch(error => {
                console.error('Events API error:', error);
                throw error;
            }),

    getEvent: (id) =>
        fetch(`/api/events/${id}`)
            .then(handleResponse),

    // Bookings
    getBookings: (userId) =>
        fetch(`/api/bookings${userId ? `?userId=${userId}` : ''}`)
            .then(handleResponse),

    createBooking: (bookingData) =>
        fetch('/api/bookings', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(bookingData),
        }).then(handleResponse),

    // Admin
    getAdminEvents: () =>
        fetch('/api/admin/events')
            .then(handleResponse),

    getAdminBookings: () =>
        fetch('/api/admin/bookings')
            .then(handleResponse),

    getAdminUsers: () =>
        fetch('/api/admin/users')
            .then(handleResponse),
};

export default api;