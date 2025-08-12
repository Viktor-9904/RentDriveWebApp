import { useEffect, useState } from "react";

export default function useFilterVehiclePropertiesByTypeId(typeId) {
    const [filterVehicleProperties, setFilterProperties] = useState([]);
    const [filterVehiclePropertiesLoading, setFilterPropertiesLoading] = useState(false);
    const [filterVehiclePropertiesError, setFilterPropertiesError] = useState(null);
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        if(!typeId) return;

        setFilterPropertiesLoading(true)

        const fetchFilterProperties = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehicletypepropertyvalue/filter/${typeId}`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setFilterProperties(data);
            } catch (err) {
                setFilterPropertiesError(err.message || "Failed to fetch filtered vehicle properties.");
            } finally {
                setFilterPropertiesLoading(false);
            }
        };

        fetchFilterProperties();
    }, [typeId]);

    return { filterVehicleProperties, filterVehiclePropertiesLoading, filterVehiclePropertiesError };
}