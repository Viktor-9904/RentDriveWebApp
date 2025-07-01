import { useEffect, useState } from "react";

export default function useAllvalueAndUnitEnums() {
    const [valueAndUnitEnums, setvalueAndUnitEnums] = useState([])
    const [loadingValueAndUnitEnums, setLoading] = useState(true)
    const [errorValueAndUnitEnums, setError] = useState(null)
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchvalueAndUnitEnums = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehicletypeproperty/value-and-unit-enums`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setvalueAndUnitEnums(data);
            } catch (err) {
                setError(err.message || "Failed to fetch valueAndUnitEnums");
            } finally {
                setLoading(false);
            }
        };

        fetchvalueAndUnitEnums();
    }, []);
    
    return { valueAndUnitEnums, loadingValueAndUnitEnums, errorValueAndUnitEnums };
}