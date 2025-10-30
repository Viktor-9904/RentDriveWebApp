import "./UserRentals.css"

import { useEffect, useState } from "react";
import { useErrorModal } from "../../../context/ErrorModalContext"

import { useUserRentals } from "../hooks/useUserRentals";
import { useAuth } from "../../../context/AccountContext";
import { useBackendURL } from "../../../hooks/useBackendURL";

import ReviewModal from "./ReviewRentalModal/ReviewRentalModal";
import CancelConfirmationModal from "./CancelRentalModal/CancelRentalConfrimationModal";
import Spinner from "../../shared/Spinner/Spinner";

export default function MyRentals() {
    const { user, isAuthenticated, loadUser } = useAuth();
    const { rentals, rentalsLoading, rentalError } = useUserRentals();

    const backEndURL = useBackendURL();
    const { setErrorModalMessage } = useErrorModal()


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
                const errorMessage = await response.text();
                setErrorModalMessage(errorMessage);
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
                const errorMessage = await response.text();
                setErrorModalMessage(errorMessage);
                throw new Error("Failed to confirm rental.");
            }

            setLocalRentals((prevRentals) =>
                prevRentals.map((rental) =>
                    rental.id === rentalId ? { ...rental, status: "Completed", isCompleted: true } : rental
                )
            );
        } catch (err) {
            // alert(err.message);
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
                const errorMessage = await response.text();
                setErrorModalMessage(errorMessage);
                throw new Error("Failed to cancel rental.");
            }

            setLocalRentals((prevRentals) =>
                prevRentals.map((rental) =>
                    rental.id === rentalId ? { ...rental, status: "Cancelled", IsCancelled: true, isCancellable: false } : rental
                )
            );
            await loadUser();
        } catch (err) {
            // alert(err.message);
        }
        finally{
            setShowCancelModal(false);
        }
    };

    if (rentalsLoading) return <Spinner message={"My Rentals"}/>
    if (rentalError) return <p>Error: {rentalError}</p>;

    return (
        <div className="user-rentals-container">
            <h3 className="user-rentals-heading">My Rentals</h3>

            <div className="rental-table-wrapper">
                {localRentals?.length > 0 ? (<table className="user-rentals-table">
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
                                <td className="user-rentals-vehicle-cell">
                                    <img
                                        src={`${backEndURL}/${rental.imageUrl}`}
                                        alt={`${rental.vehicleMake} ${rental.vehicleModel}`}
                                        className="user-rentals-vehicle-image"
                                    />
                                    {rental.vehicleMake} {rental.vehicleModel}
                                </td>
                                <td>
                                    <span className={`user-rental-status ${rental.status.toLowerCase()}`}>
                                        {rental.status}
                                    </span>
                                </td>
                                <td className="user-rental-date-cell">{new Date(rental.bookedOn).toLocaleDateString("en-GB", { day: "numeric", month: "long", year: "numeric", })}</td>
                                <td className="user-rental-date-cell">{new Date(rental.startDate).toLocaleDateString("en-GB", { day: "numeric", month: "long", year: "numeric", })}</td>
                                <td className="user-rental-date-cell">{new Date(rental.endDate).toLocaleDateString("en-GB", { day: "numeric", month: "long", year: "numeric", })}</td>
                                <td className="user-rental-price-cell">{rental.pricePerDay.toFixed(2)} €</td>
                                <td className="user-rental-price-cell">{rental.totalPrice.toFixed(2)} €</td>
                                <td>
                                    {rental.status === "Active" && rental.isConfirmable && (
                                        <button
                                            className="confirm-user-rental-button"
                                            onClick={() => handleConfirm(rental.id)}
                                            disabled={selectedId === rental.id}
                                        >
                                            Confirm Rental
                                        </button>
                                    )}
                                    {rental.isCompleted && rental.hasReviewedVehicle === false && (
                                        <button
                                            className="leave-rental-review-button"
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
                                            className="cancel-user-rental-button"
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
