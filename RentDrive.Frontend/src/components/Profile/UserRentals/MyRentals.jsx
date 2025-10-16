import { useUserRentals } from "../hooks/useUserRentals";
import { useEffect, useState } from "react";
import ReviewModal from "../ReviewModal";
import { SiPayloadcms } from "react-icons/si";
import CancelConfirmationModal from "./CancelConfrimationModal";
import { Flag } from "lucide-react";
import { useAuth } from "../../../context/AccountContext";
import { useBackendURL } from "../../../hooks/useBackendURL";
import Spinner from "../../shared/Spinner/Spinner";

export default function MyRentals() {
    const { user, isAuthenticated, loadUser } = useAuth();
    const { rentals, rentalsLoading, rentalError } = useUserRentals();
    const backEndURL = useBackendURL();

    const [localRentals, setLocalRentals] = useState([]);
    const [selectedId, setSelectedId] = useState("");
    const [selectedRental, setSelectedRental] = useState({});

    const [showReviewModal, setShowReviewModal] = useState(false);
    const [showCancelModal, setShowCancelModal] = useState(false);

    useEffect(() => {
        if (rentals) {
            setLocalRentals(rentals)
        }
    }, [rentals]);

    const handleReviewSubmit = async (data) => {
        const payload = {
            rentalId: selectedId,
            starRating: data.rating,
            comment: data.comment
        }
        try {
            const response = await fetch(`${backEndURL}/api/vehiclereview/add`, {
                method: "POST",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(payload),
            });


            if (!response.ok) {
                throw new Error("Failed to add review to vehicle.");
            }

            setLocalRentals((prevRentals) =>
                prevRentals.map((rental) =>
                    rental.id === selectedId ? { ...rental, hasReviewedVehicle: true } : rental
                )
            );

        } catch (error) {
            console.error("Leaving a review failed:", error.message);
        }
        finally {
            setSelectedId(null);
        }
    };


    const handleConfirm = async (rentalId) => {
        setSelectedId(rentalId);

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
                    rental.id === rentalId ? { ...rental, status: "Completed", isCompleted: true } : rental
                )
            );
        } catch (err) {
            setConfirmError(err.message || "Something went wrong.");
        } finally {
            setSelectedId(null);
        }
    };

    const handleCancel = async (rentalId) => {

        try {
            const response = await fetch(`${backEndURL}/api/rental/cancel-rental/${rentalId}`, {
                method: "POST",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json",
                },
            });

            if (!response.ok) {
                throw new Error("Failed to cancel rental.");
            }

            setLocalRentals((prevRentals) =>
                prevRentals.map((rental) =>
                    rental.id === rentalId ? { ...rental, status: "Cancelled", IsCancelled: true, isCancellable: false } : rental
                )
            );
            setShowCancelModal(false);
            await loadUser();
        } catch (err) {
            setConfirmError(err.message || "Something went wrong.");
        }
    };

    if (rentalsLoading) return <Spinner message={"My Rentals"}/>
    if (rentalError) return <p>Error: {rentalError}</p>;

    return (
        <div className="rentals-container">
            <h3 className="rentals-heading">My Rentals</h3>

            <div className="rental-table-wrapper">
                {localRentals?.length > 0 ? (<table className="rentals-table">
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
                                    {rental.status === "Active" && rental.isConfirmable && (
                                        <button
                                            className="confirm-rental-button"
                                            onClick={() => handleConfirm(rental.id)}
                                            disabled={selectedId === rental.id}
                                        >
                                            Confirm Rental
                                        </button>
                                    )}
                                    {rental.isCompleted && rental.hasReviewedVehicle === false && (
                                        <button
                                            className="leave-review-rental-button"
                                            onClick={() => {
                                                setShowReviewModal(true);
                                                setSelectedId(rental.id)
                                            }}
                                        >
                                            Leave a Review
                                        </button>
                                    )}
                                    {rental.isCancellable && rental.isCancelled == false && (
                                        <button
                                            className="cancel-rental-button"
                                            onClick={() => {
                                                setShowCancelModal(true);
                                                setSelectedId(rental.id)
                                                setSelectedRental(rental)
                                            }}
                                        >
                                            Cancel Rental
                                        </button>
                                    )}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                ) : (
                    <div className="col-12 text-center p-5">
                        <p>No rentals found.</p>
                    </div>)}

            </div>
            <ReviewModal
                show={showReviewModal}
                onClose={() => {
                    setShowReviewModal(false)
                    setSelectedId("")
                }}
                onSubmit={handleReviewSubmit}
            />
            <CancelConfirmationModal
                show={showCancelModal}
                onClose={() =>{
                    setShowCancelModal(false)
                    setSelectedId("")
                    setSelectedRental({})
                }}
                onConfirm={handleCancel}
                rental={selectedRental}
            />
        </div>
    );
}
