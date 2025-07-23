import { useState, useEffect } from "react";

export function useUserRentals() {
    const [rentals, setRentals] = useState([]);
    const [rentalsLoading, setRentalsLoading] = useState(true);
    const [rentalError, setRentalError] = useState(null);
    const backEndURL = import.meta.env.VITE_API_URL;


    useEffect(() => {
        const fetchRentals = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/rental/my-rentals`, {
                    method: "GET",
                    credentials: "include",
                    headers: {
                        "Content-Type": "application/json",
                    },
                });

                if (!response.ok) {
                    throw new Error("Failed to fetch rentals.");
                }

                const data = await response.json();
                setRentals(data);
            } catch (err) {
                setRentalError(err.message);
            } finally {
                setRentalsLoading(false);
            }
        }

        fetchRentals();
    }, []);

    return { rentals, rentalsLoading, rentalError };
}
