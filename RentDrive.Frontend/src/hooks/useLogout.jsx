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
            fetch(`${backEndURL}/api/account/logout`, {
                method: 'POST',
                credentials: 'include',
            })
        } catch (err) {
            setError(err)
        } finally{
            navigate('/');
        }
        await loadUser()
    };
    return logout;
}
