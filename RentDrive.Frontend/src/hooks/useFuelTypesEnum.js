import { useEffect, useState } from "react";
import { useBackendURL } from "./useBackendURL";

export default function useFuelTypesEnum() {
    const [fuelTypeEnum, setFuelTypeEnum] = useState([])
    const [loadingfuelTypeEnum, setLoading] = useState(true)
    const [errorfuelTypeEnum, setError] = useState(null)
    const backEndURL = useBackendURL();

    useEffect(() => {
        const fetchfuelTypeEnum = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/base/fuel-types`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setFuelTypeEnum(data);
            } catch (err) {
                setError(err.message || "Failed to fetch fuel type enum");
            } finally {
                setLoading(false);
            }
        };

        fetchfuelTypeEnum();
    }, []);
    
    return { fuelTypeEnum, loadingfuelTypeEnum, errorfuelTypeEnum };
}