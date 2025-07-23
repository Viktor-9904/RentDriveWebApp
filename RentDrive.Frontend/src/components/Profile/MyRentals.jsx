import { useUserRentals } from "./hooks/useUserRentals";
import { useEffect, useState } from "react";

export default function MyRentals() {
    const { rentals, rentalsLoading, rentalError } = useUserRentals();
    const backEndURL = import.meta.env.VITE_API_URL;

    const [localRentals, setLocalRentals] = useState([]);
    const [confirmingId, setConfirmingId] = useState("");

    useEffect(() => {
        if (rentals) {
            setLocalRentals(rentals);
        }
    }, [rentals]);

    const handleConfirm = async (rentalId) => {
        setConfirmingId(rentalId);

        try {
            const response = await fetch(`${backEndURL}/api/rental/confirm-rental/${rentalId}`, {
                method: "POST",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json",
                },
            });

            if (!response.ok) {
                throw new Error("Failed to confirm rental.");
            }

            setLocalRentals((prevRentals) =>
                prevRentals.map((rental) =>
                    rental.id === rentalId ? { ...rental, status: "Completed" } : rental
                )
            );
        } catch (err) {
            setConfirmError(err.message || "Something went wrong.");
        } finally {
            setConfirmingId(null);
        }
    };

    if (rentalsLoading) return <p>Loading rentals...</p>;
    if (rentalError) return <p>Error: {rentalError}</p>;

    return (
        <div className="rentals-container">
            <h3 className="rentals-heading">My Rentals</h3>

            <table className="rentals-table">
                <thead>
                    <tr>
                        <th>Vehicle</th>
                        <th>Status</th>
                        <th>Booked On</th>
                        <th>Start Date</th>
                        <th>End Date</th>
                        <th>Price Per Day</th>
                        <th>Total Price</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {localRentals.map((rental) => (
                        <tr key={rental.id}>
                            <td className="vehicle-cell">
                                <img
                                    src={`${backEndURL}/${rental.imageUrl}`}
                                    alt={`${rental.vehicleMake} ${rental.vehicleModel}`}
                                    className="vehicle-image"
                                />
                                {rental.vehicleMake} {rental.vehicleModel}
                            </td>
                            <td>
                                <span className={`rental-status ${rental.status.toLowerCase()}`}>
                                    {rental.status}
                                </span>
                            </td>
                            <td className="date-cell">{new Date(rental.bookedOn).toLocaleDateString("en-GB", { day: "numeric", month: "long", year: "numeric", })}</td>
                            <td className="date-cell">{new Date(rental.startDate).toLocaleDateString("en-GB", { day: "numeric", month: "long", year: "numeric", })}</td>
                            <td className="date-cell">{new Date(rental.endDate).toLocaleDateString("en-GB", { day: "numeric", month: "long", year: "numeric", })}</td>
                            <td className="price-cell">{rental.pricePerDay.toFixed(2)} €</td>
                            <td className="price-cell">{rental.totalPrice.toFixed(2)} €</td>
                            <td>
                                {rental.status === "Active" && (
                                    <button
                                        className="confirm-button"
                                        onClick={() => handleConfirm(rental.id)}
                                        disabled={confirmingId === rental.id}
                                    >
                                        Confirm
                                    </button>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
