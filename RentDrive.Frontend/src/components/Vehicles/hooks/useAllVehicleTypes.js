import { useEffect, useState } from "react";
import { useBackendURL } from "../../../hooks/useBackendURL";

export default function useAllVehicleTypes() {
    const [vehicleTypes, setVehicleTypes] = useState([])
    const [loadingVehicleTypes, setLoading] = useState(true)
    const [errorVehicleTypes, setError] = useState(null)
    const backEndURL = useBackendURL();

    useEffect(() => {
        const fetchvehicleTypes = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehicletype/types`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setVehicleTypes(data);
            } catch (err) {
                setError(err.message || "Failed to fetch vehicleTypes");
            } finally {
                setLoading(false);
            }
        };

        fetchvehicleTypes();
    }, []);
    
    return { vehicleTypes, loadingVehicleTypes, errorVehicleTypes };
}