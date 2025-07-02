import { useEffect, useState } from "react";

import PropertyTable from "./PropertyTable";

import useAllVehicleTypes from "./hooks/useAllVehicleTypes";
import useAllVehicleTypeProperties from "./hooks/useAllVehicleTypeProperties";
import useAllvalueAndUnitEnums from "./hooks/useValueAndUnitEnums";

export default function VehicleTypePropertyManager() {

    const { vehicleTypes, loadingVehicleTypes, errorVehicleTypes } = useAllVehicleTypes();
    const { vehicleTypeProperties, loadingVehicleTypeProperties, errorVehicleTypeProperties } = useAllVehicleTypeProperties();
    const { valueAndUnitEnums, loadingValueAndUnitEnums, errorValueAndUnitEnums } = useAllvalueAndUnitEnums();
    const [selectedTypeId, setSelectedTypeId] = useState("");
    const [editingId, setEditingId] = useState(null);
    const [editValues, setEditValues] = useState({
        name: '',
        valueType: '',
        unitOfMeasurement: '',
        isNew: false,
    });
    const [filteredProperties, setFilteredProperties] = useState([]);

    useEffect(() => {
        if (vehicleTypes.length > 0 && selectedTypeId === "") {
            setSelectedTypeId(vehicleTypes[0].id);
        }
    }, [vehicleTypes, selectedTypeId]);

    useEffect(() => {
        if (vehicleTypeProperties && selectedTypeId !== null) {
            const filtered = vehicleTypeProperties.filter(
                p => p.vehicleTypeId === selectedTypeId
            );
            setFilteredProperties(filtered);
        }
    }, [vehicleTypeProperties, selectedTypeId]);

    const handlePropertyUpdated = (updatedProperty) => {
        setFilteredProperties(prev =>
            prev.map(p =>
                p.id === updatedProperty.id ? updatedProperty : p
            )
        );
    };
<<<<<<< Updated upstream
=======

    const handleDeleteProperty = (id) => {
        setFilteredProperties(prev => prev.filter(p => p.id !== id));
    };
>>>>>>> Stashed changes

    const handleAddNewProperty = () => {
        const tempId = `temp-${Date.now()}`;

        const newProperty = {
            id: tempId,
            name: '',
            valueType: '',
            unitOfMeasurement: '',
            isNew: true,
        };

        setFilteredProperties(prev => [...prev, newProperty]);
        setEditingId(tempId);
        setEditValues({
            name: '',
            valueType: '',
            unitOfMeasurement: '',
            isNew: true,
        });
    };

    return (
        <div className="container py-5">
            <h2 className="mb-4">Manage Vehicle Type Properties</h2>

            <div className="mb-4">
                <label className="form-label">Select Vehicle Type:</label>
                <select
                    className="form-select"
                    value={selectedTypeId}
                    onChange={(e) => setSelectedTypeId(Number(e.target.value))}
                >
                    {vehicleTypes.map(type => (
                        <option key={type.id} value={type.id}>
                            {type.name}
                        </option>
                    ))}
                </select>
            </div>

            <div className="d-flex justify-content-end mb-3">
                <button className="btn btn-success" onClick={handleAddNewProperty}>
                    + Add New Property
                </button>
            </div>

            <PropertyTable
                setFilteredProperties={setFilteredProperties}
                filteredProperties={filteredProperties}
                valueAndUnitEnums={valueAndUnitEnums}
                onPropertyUpdated={handlePropertyUpdated}
<<<<<<< Updated upstream
=======
                onDeleteSuccess={handleDeleteProperty}
                setEditingId={setEditingId}
                editingId={editingId}
                setEditValues={setEditValues}
                editValues={editValues}
                selectedTypeId={selectedTypeId}
>>>>>>> Stashed changes
            />

        </div>
    );
}
