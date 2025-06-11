import { useEffect, useState } from "react";

export default function useAllVehicles() {
    const [vehicles, setVehicles] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchVehicles = async () => {
            try {
                const response = await fetch("https://localhost:7299/api/vehicle/all");
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setVehicles(data);
            } catch (err) {
                setError(err.message || "Failed to fetch vehicles");
            } finally {
                setLoading(false);
            }
        };

        fetchVehicles();
    }, []);

    return { vehicles, loading, error };
}