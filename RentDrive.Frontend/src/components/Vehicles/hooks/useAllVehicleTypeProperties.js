import { useEffect, useState } from "react";

export default function useAllVehicleTypeProperties() {
    const [vehicleTypeProperties, setVehicleTypeProperties] = useState([]);
    const [loadingVehicleTypeProperties, setLoading] = useState(true);
    const [errorVehicleTypeProperties, setError] = useState(null);
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchVehicleTypeProperties = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehicletypeproperty/types/properties`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setVehicleTypeProperties(data);
            } catch (err) {
                setError(err.message || "Failed to fetch vehicleTypeProperties");
            } finally {
                setLoading(false);
            }
        };

        fetchVehicleTypeProperties();
    }, []);

    return { vehicleTypeProperties, loadingVehicleTypeProperties, errorVehicleTypeProperties };
}