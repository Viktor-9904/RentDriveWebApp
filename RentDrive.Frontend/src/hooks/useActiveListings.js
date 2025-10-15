import { useEffect, useState } from "react";
import { useBackendURL } from "./useBackendURL";

export default function useActiveListings() {
    const [activeListings, setActiveListings] = useState(0)
    const [loadingActiveListings, setLoadingActiveListings] = useState(true)
    const [errorActiveListing, setErrorActiveListings] = useState(null)
    const backEndURL = useBackendURL();

    useEffect(() => {
        const fetchActiveListings = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehicle/active-listings`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setActiveListings(data);
            } catch (err) {
                setErrorActiveListings(err.message || "Failed to fetch active listings.");
            } finally {
                setLoadingActiveListings(false);
            }
        };

        fetchActiveListings();
    }, []);
    
    return { activeListings, loadingActiveListings, errorActiveListing };
}