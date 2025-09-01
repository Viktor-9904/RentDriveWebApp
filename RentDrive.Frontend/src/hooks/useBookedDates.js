import { useEffect, useState } from "react";
import { useBackendURL } from "./useBackendURL";

export default function usebookedDates(id) {
    const [bookedDates, setBookedDates] = useState([])
    const [loadingBookedDates, setLoadingBookedDates] = useState(true)
    const [errorBookedDates, setErrorBookedDates] = useState(null)
    const backEndURL = useBackendURL();

    useEffect(() => {
        const fetchBookedDates = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/rental/${id}`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setBookedDates(data);
            } catch (err) {
                setErrorBookedDates(err.message || "Failed to fetch booked dates.");
            } finally {
                setLoadingBookedDates(false);
            }
        };

        fetchBookedDates();
    }, []);
    
    return { bookedDates, loadingBookedDates, errorBookedDates };
}