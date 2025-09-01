import { useEffect, useState } from "react";
import { useBackendURL } from "../../../hooks/useBackendURL";

export default function useBaseFilterProperties(vehicleTypeId, vehicleTypeCategoryId) {
    const [baseFilterProperties, setBaseFilterProperties] = useState([]);
    const [baseFilterPropertiesLoading, setBaseFilterPropertiesLoading] = useState(true);
    const [baseFilterPropertiesError, setBaseFilterPropertiesError] = useState(null);
    const backEndURL = useBackendURL();

    useEffect(() => {
        const fetchBasefilterProperties = async () => {

            console.log("in base filter properties hook")

            setBaseFilterPropertiesLoading(true);
            setBaseFilterPropertiesError(null);

            const queryParams = new URLSearchParams();
            if (vehicleTypeId !== undefined && vehicleTypeId !== null) {
                queryParams.append("vehicleTypeId", vehicleTypeId);
            }
            if (vehicleTypeCategoryId !== undefined && vehicleTypeCategoryId !== null) {
                queryParams.append("vehicleTypeCategoryId", vehicleTypeCategoryId);
            }

            try {
                const response = await fetch(`${backEndURL}/api/vehicle/base-filter-properties?${queryParams.toString()}`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setBaseFilterProperties(data);
            } catch (err) {
                setBaseFilterPropertiesError(err.message || "Failed to fetch base filter properties");
            } finally {
                setBaseFilterPropertiesLoading(false);
            }
        };

        fetchBasefilterProperties();
    }, [vehicleTypeId, vehicleTypeCategoryId]);

    return { baseFilterProperties, baseFilterPropertiesLoading, baseFilterPropertiesError };
}