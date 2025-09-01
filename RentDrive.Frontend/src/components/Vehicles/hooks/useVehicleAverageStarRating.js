import { useEffect, useState } from "react";
import { useBackendURL } from "../../../hooks/useBackendURL";

export default function useVehicleAverageStarRating(vehicleId) {

    const [averageStarRating, setAverageStarRating] = useState(0)
    const [loadingAverageStarRating, setLoadingaverageStarRating] = useState(true)
    const [errorAverageStarRating, setErrorAverageStarRating] = useState(null)
    const backEndURL = useBackendURL();

    useEffect(() => {
        const fetchAverageStarRating = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehiclereview/average-star-rating/${vehicleId}`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setAverageStarRating(data);
            } catch (err) {
                setErrorAverageStarRating(err.message || "Failed to fetch average rating.");
            } finally {
                setLoadingaverageStarRating(false);
            }
        };

        fetchAverageStarRating();
    }, []);
    
    return { averageStarRating, loadingAverageStarRating, errorAverageStarRating };
}