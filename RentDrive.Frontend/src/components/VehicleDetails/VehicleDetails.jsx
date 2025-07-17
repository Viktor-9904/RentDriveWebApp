import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import DeleteConfirmationModal from "../Vehicles/DeleteConfirmationModal";
import useVehicleDetails from "../Vehicles/hooks/useVehicleDetails";

export default function VehicleDetails() {
    const { id } = useParams();
    const { vehicle, loadingVehicle, errorVehicle } = useVehicleDetails(id);
    const [currentImageIndex, setCurrentImageIndex] = useState(0);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const navigate = useNavigate();

    const backEndURL = import.meta.env.VITE_API_URL;

    const handlePrevImage = () => {
        setCurrentImageIndex(prev =>
            prev === 0 ? vehicle.imageURLS.length - 1 : prev - 1
        );
    };

    const handleNextImage = () => {
        setCurrentImageIndex(prev =>
            prev === vehicle.imageURLS.length - 1 ? 0 : prev + 1
        );
    };

    const handleEdit = () => {
        console.log("Edit button clicked");
        navigate(`/manage/vehicles/edit/${id}`)
    };

    const handleDelete = () => {
        console.log("Delete button clicked");
        setShowDeleteModal(true)
    };

    const confirmDelete = async () => {
        try {
            const response = await fetch(
                `${backEndURL}/api/vehicle/delete/${vehicle.id}`,
                {
                    method: "DELETE",
                }
            );
            if (!response.ok) {
                throw new Error("Failed to delete property");
            }
            setShowDeleteModal(false);
            navigate("/listing")
        } catch (err) {
            alert(err.message);
        }
    };


    if (loadingVehicle) return <div className="text-center py-5">LoadingVehicle vehicle details...</div>;
    if (!vehicle) return <div className="text-center py-5 text-danger">Vehicle not found.</div>;

    const allProperties = [
        { label: "Year", value: new Date(vehicle.dateOfProduction).getFullYear() },
        { label: "Fuel", value: vehicle.fuelType || "N/A" },
        { label: "Color", value: vehicle.color },
        { label: "Type", value: vehicle.vehicleType },
        { label: "Weight", value: `${vehicle.curbWeightInKg} kg` },
        ...(vehicle.ownerName ? [{ label: "Owner", value: vehicle.ownerName }] : []),
        ...vehicle.vehicleProperties.map(p => ({
            label: p.vehicleTypePropertyName,
            value: `${p.vehicleTypePropertyValue} ${p.unitOfMeasurement !== "None" ? p.unitOfMeasurement : ""}`.trim()
        }))
    ];

    return (
        <>
            <div className="container py-5">
                <div className="row">
                    <div className="col-md-5">
                        <div className="position-relative border rounded p-2 mb-3 text-center">
                            <img
                                src={`${backEndURL}/${vehicle.imageURLS[currentImageIndex]}`}
                                alt={`${vehicle.make} ${vehicle.model}`}
                                className="img-fluid rounded"
                                style={{ maxHeight: "350px", objectFit: "cover", width: "100%" }}
                            />
                            <button
                                onClick={handlePrevImage}
                                className="btn btn-light position-absolute top-50 start-0 translate-middle-y"
                                style={{ zIndex: 1 }}
                            >
                                ‹
                            </button>
                            <button
                                onClick={handleNextImage}
                                className="btn btn-light position-absolute top-50 end-0 translate-middle-y"
                                style={{ zIndex: 1 }}
                            >
                                ›
                            </button>
                        </div>

                        <div className="vehicle-price-card text-center">
                            <h4 className="mb-1 text-success">
                                {vehicle.pricePerDay.toFixed(2)} € / day
                            </h4>
                            <small className="text-muted">Rental Price</small>
                        </div>
                    </div>

                    <div className="col-md-7">
                        <h2 className="mb-3">{vehicle.make} {vehicle.model}</h2>

                        <div className="row">
                            {allProperties.map((prop, index) => (
                                <div key={index} className="col-sm-6">
                                    <div className="property-box">
                                        <strong>{prop.label}:</strong> {prop.value}
                                    </div>
                                </div>
                            ))}
                        </div>

                        {vehicle.description && (
                            <div className="description-box mt-4">
                                <strong>Description:</strong>
                                <p className="mb-0">{vehicle.description}</p>
                            </div>
                        )}

                        <div className="mt-4 d-flex gap-2">
                            <button className="btn btn-primary" onClick={handleEdit}>
                                Edit
                            </button>
                            <button className="btn btn-danger" onClick={handleDelete}>
                                Delete
                            </button>
                        </div>
                    </div>
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
