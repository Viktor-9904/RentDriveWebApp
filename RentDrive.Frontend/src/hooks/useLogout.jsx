import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AccountContext';
import { useState } from 'react';

export default function useLogout() {
    const [error, setError] = useState(null)
    const { user, isAuthenticated, loadUser } = useAuth();
    const backEndURL = import.meta.env.VITE_API_URL;
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
            console.log("Loogged out successfully!")
            navigate('/');
        }
        await loadUser()
    };
    return logout;
}
