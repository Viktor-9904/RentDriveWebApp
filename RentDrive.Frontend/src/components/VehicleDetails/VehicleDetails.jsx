import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";

export default function VehicleDetails() {
    const { id } = useParams();
    const [vehicle, setVehicle] = useState(null);
    const [loading, setLoading] = useState(true);
    const [currentImageIndex, setCurrentImageIndex] = useState(0);

    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        fetch(`${backEndURL}/api/vehicle/${id}`)
            .then(res => res.json())
            .then(data => {
                setVehicle(data);
                setLoading(false);
            })
            .catch(err => {
                console.error("Failed to load vehicle:", err);
                setLoading(false);
            });
    }, [id]);

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

    if (loading) return <div className="text-center py-5">Loading vehicle details...</div>;
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
        <div className="container py-5">
            <div className="row">
                <div className="col-md-5">
                    <div className="position-relative border rounded p-2 mb-3 text-center">
                        <img
                            {...console.log(vehicle.imageURLS[0])}
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
                </div>
            </div>
        </div>
    );
}
