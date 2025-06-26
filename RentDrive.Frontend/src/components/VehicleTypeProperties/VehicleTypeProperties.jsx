import { useEffect, useState } from "react";
import useAllVehicleTypes from "./hooks/useAllVehicleTypes";
import useAllVehicleTypeProperties from "./hooks/useAllVehicleTypeProperties";
import PropertyTable from "./PropertyTable";

export default function VehicleTypePropertyManager() {

    const { vehicleTypes, loadingVehicleTypes, errorVehicleTypes } = useAllVehicleTypes();
    const { vehicleTypeProperties, loadingVehicleTypeProperties, errorVehicleTypeProperties } = useAllVehicleTypeProperties();
    const [selectedTypeId, setSelectedTypeId] = useState(null);
    const [filteredProperties, setFilteredProperties] = useState([]);

    useEffect(() => {
        if (vehicleTypes && vehicleTypes.length > 0) {
            setSelectedTypeId(vehicleTypes[0].id);
        }
    }, [vehicleTypes]);

    useEffect(() => {
        if (vehicleTypeProperties && selectedTypeId !== null) {
            const filtered = vehicleTypeProperties.filter(
                p => p.vehicleTypeId === selectedTypeId
            );
            setFilteredProperties(filtered);
        }
    }, [vehicleTypeProperties, selectedTypeId]);

    return (
        <div className="container py-5">
            <h2 className="mb-4">Manage Vehicle Type Properties</h2>

            <div className="mb-4">
                <label className="form-label">Select Vehicle Type:</label>
                <select
                    className="form-select"
                    value={selectedTypeId}
                    onChange={(e) => setSelectedTypeId(parseInt(e.target.value))}
                >
                    {vehicleTypes.map(type => (
                        <option key={type.id} value={type.id}>
                            {type.name}
                        </option>
                    ))}
                </select>
            </div>

            <div className="d-flex justify-content-end mb-3">
                <button className="btn btn-success">
                    + Add New Property
                </button>
            </div>

            <PropertyTable filteredProperties={filteredProperties}/>
        </div>
    );
}
