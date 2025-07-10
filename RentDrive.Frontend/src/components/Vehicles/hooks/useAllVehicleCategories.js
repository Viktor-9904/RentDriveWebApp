import { useEffect, useState } from "react";

export default function useAllVehicleCategories() {
    const [vehicleCategories, setVehicleCategories] = useState([]);
    const [loadingVehicleCategories, setLoadingVehicleCategories] = useState(true);
    const [errorVehicleCategories, setErrorVehicleCategories] = useState(null);
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchVehicleCategories = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehicletypecategory/all`);
                if (!response.ok) {
                    throw new setErrorVehicleCategories(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setVehicleCategories(data);
            } catch (err) {
                setErrorVehicleCategories(err.message || "Failed to fetch vehicle categories");
            } finally {
                setLoadingVehicleCategories(false);
            }
        };

        fetchVehicleCategories();
    }, []);

    return { vehicleCategories, loadingVehicleCategories, errorVehicleCategories };
}