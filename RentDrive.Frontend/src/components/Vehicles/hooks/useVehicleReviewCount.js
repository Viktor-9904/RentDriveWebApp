import { useEffect, useState } from "react";

export default function useVehicleReviewsCount(vehicleId) {
    
    const [vehicleReviewsCount, setVehicleReviewsCount] = useState(0)
    const [loadingVehicleReviewsCount, setLoadingVehicleReviewsCount] = useState(true)
    const [errorVehicleReviewsCount, setErrorvehicleReviewsCount] = useState(null)
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchVehicleReviewsCount = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehiclereview/reviews-count/${vehicleId}`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setVehicleReviewsCount(data);
            } catch (err) {
                setErrorvehicleReviewsCount(err.message || "Failed to fetch vehicle review count.");
            } finally {
                setLoadingVehicleReviewsCount(false);
            }
        };

        fetchVehicleReviewsCount();
    }, []);
    
    return { vehicleReviewsCount, loadingVehicleReviewsCount, errorVehicleReviewsCount };
}