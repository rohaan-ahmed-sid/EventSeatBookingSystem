import { useEffect, useState } from 'react';
import api from '../../services/api';

const AdminUsers = () => {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        api.get('/admin/users')
            .then(res => setUsers(res.data))
            .catch(err => console.error('Failed to load users', err));
    }, []);

    return (
        <div className="p-6">
            <h2 className="text-xl font-bold mb-4">All Users</h2>
            <table className="w-full table-auto border">
                <thead className="bg-gray-200">
                    <tr>
                        <th className="px-4 py-2 border">ID</th>
                        <th className="px-4 py-2 border">Email</th>
                        <th className="px-4 py-2 border">Role</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map(u => (
                        <tr key={u.id} className="text-center">
                            <td className="border px-4 py-2">{u.id}</td>
                            <td className="border px-4 py-2">{u.email}</td>
                            <td className="border px-4 py-2">{u.role}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default AdminUsers;
