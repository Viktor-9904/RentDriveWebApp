import { useEffect, useState } from "react";
import { useBackendURL } from "../../../hooks/useBackendURL";

export default function useRecentVehicles() {
    const [recentVehicles, setRecentVehicles] = useState([]);
    const [recentVehiclesLoading, setRecentVehiclesLoading] = useState(true);
    const [recentVehiclesError, setRecentVehiclesError] = useState(null);
    const backEndURL = useBackendURL();

    useEffect(() => {
        const fetchRecentVehicles = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehicle/recent`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setRecentVehicles(data);
            } catch (err) {
                setRecentVehiclesError(err.message || "Failed to fetch recent vehicles");
            } finally {
                setRecentVehiclesLoading(false);
            }
        };

        fetchRecentVehicles();
    }, []);

    return { recentVehicles, recentVehiclesLoading, recentVehiclesError };
}