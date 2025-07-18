import { useEffect, useState } from "react";

export default function useUnitsEnum() {
    const [unitsEnum, setunitsEnum] = useState([])
    const [loadingUnitsEnum, setLoading] = useState(true)
    const [errorUnitsEnum, setError] = useState(null)
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchUnitsEnum = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/base/units`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setunitsEnum(data);
            } catch (err) {
                setError(err.message || "Failed to fetch units enum");
            } finally {
                setLoading(false);
            }
        };

        fetchUnitsEnum();
    }, []);
    
    return { unitsEnum, loadingUnitsEnum, errorUnitsEnum };
}