import { useEffect, useState } from "react";
import { useBackendURL } from "../../../hooks/useBackendURL";

export default function useSearchQuery(searchQuery) {
    const [searchQueryVehicles, setSearchQueryVehicles] = useState([]);
    const [searchQueryVehiclesLoading, setSearchQueryVehiclesLoading] = useState(true);
    const [searchQueryVehiclesError, setSearchQueryVehiclesError] = useState(null);
    const backEndURL = useBackendURL();

    useEffect(() => {
        if (!searchQuery || searchQuery.trim() === "") {
            setSearchQueryVehicles([]);
            setSearchQueryVehiclesLoading(false);
            setSearchQueryVehiclesError(null);
            return;
        }

        const fetchSearchQueryVehicles = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehicle/search-vehicles?searchQuery=${encodeURIComponent(searchQuery)}`);

                if (!response.ok) {
                    throw new Error(`HTTP Search Query Error! status: ${response.status}`);
                }
                const data = await response.json();
                setSearchQueryVehicles(data);
            } catch (err) {
                setSearchQueryVehiclesError(err.message || "Failed to fetch searched Vehicles");
            } finally {
                setSearchQueryVehiclesLoading(false);
            }
        };

        fetchSearchQueryVehicles();
    }, [searchQuery]);

    return { searchQueryVehicles, searchQueryVehiclesLoading, searchQueryVehiclesError };
}