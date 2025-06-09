import { useNavigate } from 'react-router-dom';

export default function useLogout() {
    const backEndURL = import.meta.env.VITE_API_URL;
    const navigate = useNavigate();

    const logout = () => {
        fetch(`${backEndURL}/api/account/logout`, {
            method: 'POST',
            credentials: 'include',
        }).finally(() => {
            navigate('/');
        });
        console.log("Loogged out successfully!")
    };
    return logout;
}
