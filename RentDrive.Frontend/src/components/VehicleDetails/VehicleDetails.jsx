import { Link, useParams } from "react-router-dom";
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
        setCurrentImageIndex(prev => (prev === 0 ? vehicle.imageURLS.length - 1 : prev - 1));
    };

    const handleNextImage = () => {
        setCurrentImageIndex(prev => (prev === vehicle.imageURLS.length - 1 ? 0 : prev + 1));
    };

    if (loading) return <div className="text-center py-5">Loading vehicle details...</div>;
    if (!vehicle) return <div className="text-center py-5 text-danger">Vehicle not found.</div>;

    const currentImage = vehicle.imageURLS?.[currentImageIndex];

    return (
        <div className="container py-5">
            <div className="row">
                {/* Image Section */}
                <div className="col-md-6 position-relative text-center">
                    <div className="position-relative border rounded p-2">
                        <img
                            src={`${backEndURL}${vehicle.imageURLS[currentImageIndex]}`}
                            alt={`${vehicle.make} ${vehicle.model}`}
                            className="img-fluid rounded"
                            style={{ maxHeight: "350px", objectFit: "cover", width: "100%" }}
                        />

                        {/* Arrows */}
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
                </div>

                {/* Metadata Section */}
                <div className="col-md-6">
                    <h2 className="mb-3">{vehicle.make} {vehicle.model}</h2>

                    <div className="border rounded p-3 mb-3 bg-light-subtle">
                        <h5 className="mb-2">Details</h5>
                        <div className="row">
                            <div className="col-sm-6">
                                <p><strong>Price:</strong> {vehicle.pricePerDay.toFixed(2)} €</p>
                                <p><strong>Year:</strong> {new Date(vehicle.dateOfProduction).getFullYear()}</p>
                                <p><strong>Fuel:</strong> {vehicle.fuelType || "N/A"}</p>
                                <p><strong>Color:</strong> {vehicle.color}</p>
                                <p><strong>Odometer:</strong> {vehicle.odoKilometers ? `${vehicle.odoKilometers} km` : "N/A"}</p>
                            </div>
                            <div className="col-sm-6">
                                <p><strong>Type:</strong> {vehicle.vehicleType}</p>
                                <p><strong>Power:</strong> {vehicle.powerInKiloWatts ? `${vehicle.powerInKiloWatts} kW` : "N/A"}</p>
                                <p><strong>Engine:</strong> {vehicle.engineDisplacement ? `${vehicle.engineDisplacement} L` : "N/A"}</p>
                                <p><strong>Weight:</strong> {vehicle.curbWeightInKg} kg</p>
                                <p><strong>Owner:</strong> {vehicle.ownerName || "Unknown"}</p>
                            </div>
                        </div>
                        {vehicle.description && (
                            <p className="mt-3"><strong>Description:</strong> {vehicle.description}</p>
                        )}
                    </div>
                </div>
            </div>

        </div>
    );
}
