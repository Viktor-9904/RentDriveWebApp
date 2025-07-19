import { useNavigate, useParams } from "react-router-dom";
import { useState } from "react";
import DeleteConfirmationModal from "../Vehicles/DeleteConfirmationModal";
import useVehicleDetails from "../Vehicles/hooks/useVehicleDetails";
import VehicleCalendar from "./VehicleCalendar";

export default function VehicleDetails() {
    const { id } = useParams();
    const { vehicle, loadingVehicle } = useVehicleDetails(id);
    const [currentImageIndex, setCurrentImageIndex] = useState(0);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const navigate = useNavigate();

    const backEndURL = import.meta.env.VITE_API_URL;

    const handleEdit = () => navigate(`/manage/vehicles/edit/${id}`);
    const handleDelete = () => setShowDeleteModal(true);

    const confirmDelete = async () => {
        try {
            const res = await fetch(`${backEndURL}/api/vehicle/delete/${vehicle.id}`, {
                method: "DELETE"
            });
            if (!res.ok) throw new Error("Failed to delete vehicle");
            navigate("/listing");
        } catch (err) {
            alert(err.message);
        }
    };

    if (loadingVehicle) return <div className="text-center py-5">Loading vehicle details...</div>;
    if (!vehicle) return <div className="text-center py-5 text-danger">Vehicle not found.</div>;

    const baseProperties = [
        { label: "Type", value: vehicle.vehicleType + " - " + vehicle.vehicleTypeCategory },
        { label: "Year", value: new Date(vehicle.dateOfProduction).getFullYear() },
        { label: "Color", value: vehicle.color },
        { label: "Fuel", value: vehicle.fuelType || "N/A" },
        { label: "Weight", value: `${vehicle.curbWeightInKg} kg` },
        ...(vehicle.ownerName ? [{ label: "Owner", value: vehicle.ownerName }] : [])
    ];

    const typeProperties = vehicle.vehicleProperties.map(p => ({
        label: p.vehicleTypePropertyName,
        value: `${p.vehicleTypePropertyValue} ${p.unitOfMeasurement !== "None" ? p.unitOfMeasurement : ""}`.trim()
    }));

    return (
        <>
            <div className="container py-5 vehicle-details">

                <div className="vehicle-header mb-4 d-flex justify-content-between align-items-center">
                    <h2 className="make-model">{vehicle.make} {vehicle.model}</h2>
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
                            <VehicleCalendar />
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
                                        <span className="property-value">{prop.value}</span>
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

                <div className="mt-4 d-flex gap-2">
                    <button className="btn btn-primary" onClick={handleEdit}>Edit</button>
                    <button className="btn btn-danger" onClick={handleDelete}>Delete</button>
                </div>
            </div>

            <DeleteConfirmationModal
                show={showDeleteModal}
                onClose={() => setShowDeleteModal(false)}
                onConfirm={confirmDelete}
                Make={vehicle?.make}
                Model={vehicle?.model}
            />
        </>
    );
}
