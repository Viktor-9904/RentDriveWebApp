import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AccountContext';
import { useState } from 'react';
import { useBackendURL } from './useBackendURL';

export default function useLogout() {
    const [error, setError] = useState(null)
    const { user, isAuthenticated, loadUser } = useAuth();
    const backEndURL = useBackendURL();
    const navigate = useNavigate();

    const logout = async () => {
        try {
            const response = await fetch(`${backEndURL}/api/account/logout`, {
                method: 'POST',
                credentials: 'include',
            });

            if (!response.ok) {
                throw new Error('Logout failed');
            }
            await loadUser();
            navigate('/');
        } catch (err) {
            setError(err);
            console.error('Logout error:', err);
        }
    };
    return logout;
}