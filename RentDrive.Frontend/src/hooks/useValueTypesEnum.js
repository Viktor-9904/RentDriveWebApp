import { useEffect, useState } from "react";

export default function useValueTypesEnum() {
    const [valueTypeEnum, setValueTypeEnum] = useState([])
    const [loadingValueTypeEnum, setLoading] = useState(true)
    const [errorValueTypeEnum, setError] = useState(null)
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchValueTypeEnum = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/base/value-types`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setValueTypeEnum(data);
            } catch (err) {
                setError(err.message || "Failed to fetch value types enum");
            } finally {
                setLoading(false);
            }
        };

        fetchValueTypeEnum();
    }, []);
    
    return { valueTypeEnum, loadingValueTypeEnum, errorValueTypeEnum };
}