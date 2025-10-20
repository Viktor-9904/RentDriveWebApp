import "./VehicleDetails.css"

import { Link, useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { Check, MoreVertical, MoreVerticalIcon, Settings, X } from "lucide-react";

import { useAuth } from "../../../context/AccountContext";
import { useBackendURL } from "../../../hooks/useBackendURL";

import RentNowModal from "../RentNowModal";
import VehicleCalendar from "../VehicleCalendar";
import ReviewList from "../ReviewList";
import Spinner from "../../shared/Spinner/Spinner";
import StarRating from "../../shared/VehicleStarRating";
import ExpandedVehicleImageModal from "../ExpandedVehicleImageModal/ExpandedVehicleImageModal";
import DeleteConfirmationModal from "../../shared/DeleteConfirmationModal/DeleteConfirmationModal";

import useVehicleDetails from "../../Vehicles/hooks/useVehicleDetails";
import usebookedDates from "../../../hooks/useBookedDates";
import useDeleteVehicle from "../../Vehicles/hooks/useDeleteVehicle";

export default function VehicleDetails() {
    const { id } = useParams();
    const { user, isAuthenticated, loadUser, isUserLoading } = useAuth();

    const { vehicle, loadingVehicle } = useVehicleDetails(id);
    const { bookedDates, loadingBookedDates, errorBookedDates } = usebookedDates(id);
    const { deleteVehicle } = useDeleteVehicle();

    const [bookedDatesState, setBookedDatesState] = useState([]);
    const [currentImageIndex, setCurrentImageIndex] = useState(0);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [showRentNowModal, setRentNowShowModal] = useState(false);
    const [showImageModal, setShowImageModal] = useState(false);

    const navigate = useNavigate();
    const backEndURL = useBackendURL();

    useEffect(() => {
        if (bookedDates && bookedDates.length > 0) {
            setBookedDatesState(bookedDates.map(d => new Date(d)));
        }
    }, [bookedDates]);

    const handleEdit = () => navigate(`/manage/vehicles/edit/${id}`);
    const handleDelete = () => setShowDeleteModal(true);

    const confirmDelete = async () => {
        const { success, error } = await deleteVehicle(vehicle?.id);

        if (!success) {
            alert("Failed to remove vehicle");
        }
    };

    const handleRent = async (selectedDates) => {
        if (!vehicle?.id || selectedDates.length === 0) {
            return;
        }

        const payload = {
            bookedDates: selectedDates.map(date =>
                `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date.getDate().toString().padStart(2, '0')}`
            )
        };

        try {
            const response = await fetch(`${backEndURL}/api/rental/rent/${id}`, {
                method: "POST",
                credentials: 'include',
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(payload),
            });

            if (!response.ok) {
                throw new Error("Failed to rent the vehicle.");
            }

            const newDates = selectedDates.map(d =>
                new Date(d.getFullYear(), d.getMonth(), d.getDate())
            );

            setBookedDatesState(prev => [...prev, ...newDates]);

            setRentNowShowModal(false);
            loadUser()

        } catch (error) {
            console.error("Renting failed:", error.message);
        }
    };

    if (loadingVehicle || isUserLoading) return <Spinner message={"Vehicle Details"} />;
    if (!vehicle) return <div className="text-center py-5 text-danger">Vehicle not found.</div>;

    const baseProperties = [
        ...(vehicle.ownerName ? [{ label: "Property of", value: vehicle.ownerName }] : []),
        { label: "Type", value: vehicle.vehicleType + " - " + vehicle.vehicleTypeCategory },
        { label: "Year", value: new Date(vehicle.dateOfProduction).getFullYear() },
        { label: "Color", value: vehicle.color },
        { label: "Fuel", value: vehicle.fuelType || "N/A" },
        { label: "Weight", value: `${vehicle.curbWeightInKg} kg` },
    ];

    const typeProperties = vehicle.vehicleProperties.map(p => ({
        label: p.vehicleTypePropertyName,
        value: `${p.vehicleTypePropertyValue} ${p.unitOfMeasurement !== "None" ? p.unitOfMeasurement : ""}`.trim()
    }));

    return (
        <>
            <div className="container py-5 vehicle-details">

                <div className="vehicle-header mb-4 d-flex justify-content-between align-items-center">
                    <div className="vehicle-info">
                        <div className="name-and-dots">
                            <h2 className="make-model mb-1">{vehicle.make} {vehicle.model}</h2>
                            {isAuthenticated && (user?.isCompanyEmployee || user?.id === vehicle.ownerId) && (
                                <div className="dropdown">
                                    <MoreVerticalIcon className="dots-icon" />
                                    <ul className="dropdown-menu">
                                        {(user?.id === vehicle.ownerId || (vehicle.isCompanyProperty && user?.isCompanyEmployee)) && (<li onClick={handleEdit}>Edit</li>)}
                                        <li onClick={handleDelete}>Delete</li>
                                    </ul>
                                </div>
                            )}
                        </div>
                        <StarRating rating={vehicle.starsRating} reviewCount={vehicle.reviewCount} />
                    </div>
                    <div className="price-tag">
                        {vehicle.pricePerDay.toFixed(2)} € / day
                    </div>
                </div>


                <div className="row g-4">
                    <div className="col-md-7">
                        <div className="card shadow-sm p-3 position-relative">
                            <img
                                src={`${backEndURL}/${vehicle.imageURLS[currentImageIndex]}`}
                                alt={`${vehicle.make} ${vehicle.model}`}
                                className="vehicle-details-image"
                                onClick={() => setShowImageModal(true)}
                            />
                            <button
                                onClick={() => setCurrentImageIndex(i => i === 0 ? vehicle.imageURLS.length - 1 : i - 1)}
                                className="btn btn-light position-absolute top-50 start-0 translate-middle-y"
                            >
                                ‹
                            </button>
                            <button
                                onClick={() => setCurrentImageIndex(i => i === vehicle.imageURLS.length - 1 ? 0 : i + 1)}
                                className="btn btn-light position-absolute top-50 end-0 translate-middle-y"
                            >
                                ›
                            </button>
                        </div>
                    </div>

                    <div className="col-md-5 d-flex flex-column align-items-center justify-content-center">
                        <div className="calendar-wrapper w-100 mb-3">
                            <VehicleCalendar
                                bookedDates={bookedDatesState}
                                setRentNowShowModal={setRentNowShowModal}
                            />
                            {isAuthenticated && vehicle?.ownerId !== user?.id && (
                                <button
                                    className="btn btn-success rent-now-btn mt-3 w-100"
                                    onClick={() => setRentNowShowModal(true)}
                                >
                                    Rent Now
                                </button>
                            )}
                            </div>
                    </div>
                </div>

                <div className="row g-4 mt-4">
                    <div className="col-md-6">
                        <div className="card shadow-sm p-4 h-100 properties-card">
                            <h5 className="section-heading mb-3">Vehicle Information</h5>
                            <div className="row">
                                {baseProperties.map((prop, i) => (
                                    <div key={i} className="col-sm-12 mb-3">
                                        <strong className="property-label">{prop.label}:</strong>
                                        <span className="property-value">{prop.value}</span>
                                    </div>
                                ))}
                            </div>
                        </div>
                    </div>

                    <div className="col-md-6">
                        <div className="card shadow-sm p-4 h-100 properties-card">
                            <h5 className="section-heading mb-3">Details</h5>
                            <div className="row">
                                {typeProperties.map((prop, i) => (
                                    <div key={i} className="col-sm-6 mb-3">
                                        <strong className="property-label">{prop.label}:</strong>
                                        <span className="property-value">
                                            {prop.value === "true" ? (
                                                <Check className="text-green-500 inline" />
                                            ) : prop.value === "false" ? (
                                                <X className="text-red-500 inline" />
                                            ) : (
                                                prop.value
                                            )}
                                        </span>

                                    </div>
                                ))}
                            </div>
                        </div>
                    </div>
                </div>

                {vehicle.description && (
                    <div className="row mt-4">
                        <div className="col-12">
                            <div className="card shadow-sm p-4 description-card">
                                <h5 className="section-heading mb-3">Description</h5>
                                <p className="description-text">{vehicle.description}</p>
                            </div>
                        </div>
                    </div>
                )}
            </div>

            <ReviewList reviews={vehicle.vehicleReviews} />

            <DeleteConfirmationModal
                show={showDeleteModal}
                onClose={() => setShowDeleteModal(false)}
                onConfirm={confirmDelete}
                item={`${vehicle?.make} ${vehicle?.model}`}
            />

            <RentNowModal
                showRentNowModal={showRentNowModal}
                onClose={() => setRentNowShowModal(false)}
                bookedDates={bookedDates}
                pricePerDay={vehicle.pricePerDay}
                handleRent={handleRent}
                userBalance={user?.balance}
                isAuthenticated={isAuthenticated}
            />

            <ExpandedVehicleImageModal
                show={showImageModal}
                onClose={() => setShowImageModal(false)}
                onPrevious={() => setCurrentImageIndex(i => i === 0 ? vehicle.imageURLS.length - 1 : i - 1)}
                onNext={() => setCurrentImageIndex(i => i === vehicle.imageURLS.length - 1 ? 0 : i + 1)}
                vehicleImageUrls={vehicle?.imageURLS}
                currentIndex={currentImageIndex}
            />
        </>
    );
}
