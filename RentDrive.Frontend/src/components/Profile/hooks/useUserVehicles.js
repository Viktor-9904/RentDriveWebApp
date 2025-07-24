import { useEffect, useState } from "react";

export default function useUserVehicles() {
    const [userVehicles, setUserVehicles] = useState([]);
    const [uservehiclesLoading, setUserVehiclesLoading] = useState(true);
    const [uservehiclesError, setUserVehiclesError] = useState(null);
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchUserVehicles = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehicle/user-vehicles`, {
                    method: "GET",
                    credentials: "include",
                });
                if (!response.ok) {
                    throw new Error(`HTTP Error! status: ${response.status}`);
                }
                const data = await response.json();
                setUserVehicles(data);
            } catch (err) {
                setUserVehiclesError(err.message || "Failed to fetch user uservehicles");
            } finally {
                setUserVehiclesLoading(false);
            }
        };

        fetchUserVehicles();
    }, []);

    return { userVehicles, uservehiclesLoading, uservehiclesError };
}