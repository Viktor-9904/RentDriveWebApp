import { useState, useEffect } from 'react';

export default function useAuth() {
    const [user, setUser] = useState(null);
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchUser = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/account/me`, {
                    credentials: 'include'
                });

                if (!response.ok) {
                    throw new Error('Not authenticated');
                }

                const data = await response.json();
                setUser(data);
            } catch (error) {
                setUser(null);
            }
        };

        fetchUser();
    }, [backEndURL]);

    const isAuthenticated = Boolean(user);

    return { user, isAuthenticated };
}