import { useState } from "react";

export default function PropertyTable({ filteredProperties, valueAndUnitEnums, onPropertyUpdated }) {
    const backEndURL = import.meta.env.VITE_API_URL;
    const [properties, setProperties] = useState(filteredProperties);
    const [editingId, setEditingId] = useState(null);
    const [editValues, setEditValues] = useState({
        name: '',
        valueType: '',
        unitOfMeasurement: ''
    });

    const handleEditClick = (prop) => {
        setEditingId(prop.id);
        setEditValues({
            name: prop.name,
            valueType: prop.valueType,
            unitOfMeasurement: prop.unitOfMeasurement
        });
    };

    const handleChange = (field, value) => {
        setEditValues(prev => ({
            ...prev,
            [field]: value
        }));
    };

    const handleCancelClick = () => {
        setEditingId(null);
        setEditValues({ name: '', valueType: '', unitOfMeasurement: '' });
    };

    const handleSaveClick = async () => {
        try {
            const payload = {
                id: editingId,
                name: editValues.name,
                valueType: editValues.valueType,
                unitOfMeasurement: editValues.unitOfMeasurement,
            };

            const response = await fetch(`${backEndURL}/api/vehicletypeproperty/edit/${editingId}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload),
            });

            if (!response.ok) {
                throw new Error('Failed to save changes');
            }
            if (onPropertyUpdated) {
                onPropertyUpdated(payload);
            }

            setEditingId(null);
        } catch (error) {
            alert(error.message);
        }
    };
    
    return (
        <div className="table-responsive">
            <table className="table table-bordered table-hover align-middle">
                <thead className="table-light">
                    <tr>
                        <th>Property Name</th>
                        <th>Value Type</th>
                        <th>Unit</th>
                        <th style={{ width: "180px" }}>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredProperties.map((prop) => {
                        const isEditing = editingId === prop.id;

                        return (
                            <tr key={prop.id}>
                                <td>
                                    <input
                                        type="text"
                                        className="form-control"
                                        value={isEditing ? editValues.name : prop.name}
                                        readOnly={!isEditing}
                                        onChange={(e) => handleChange("name", e.target.value)}
                                    />
                                </td>
                                <td>
                                    {isEditing ? (
                                        <select
                                            className="form-select"
                                            value={editValues.valueType}
                                            onChange={(e) => handleChange("valueType", e.target.value)}
                                        >
                                            <option value="">Select Value Type</option>
                                            {valueAndUnitEnums?.valueTypes?.map(vt => (
                                                <option key={vt.id} value={vt.name}>
                                                    {vt.name}
                                                </option>
                                            ))}
                                        </select>
                                    ) : (
                                        <input
                                            type="text"
                                            className="form-control"
                                            value={prop.valueType}
                                            readOnly
                                        />
                                    )}
                                </td>
                                <td>
                                    {isEditing ? (
                                        <select
                                            className="form-select"
                                            value={editValues.unitOfMeasurement}
                                            onChange={(e) => handleChange("unitOfMeasurement", e.target.value)}
                                        >
                                            <option value="">Select Unit</option>
                                            {valueAndUnitEnums?.unitOfMeasurements?.map(uom => (
                                                <option key={uom.id} value={uom.name}>
                                                    {uom.name}
                                                </option>
                                            ))}
                                        </select>
                                    ) : (
                                        <input
                                            type="text"
                                            className="form-control"
                                            value={prop.unitOfMeasurement}
                                            readOnly
                                        />
                                    )}
                                </td>
                                <td className="text-center">
                                    {isEditing ? (
                                        <>
                                            <button
                                                className="btn btn-success btn-sm me-2"
                                                onClick={handleSaveClick}
                                            >
                                                Save
                                            </button>
                                            <button
                                                className="btn btn-secondary btn-sm"
                                                onClick={handleCancelClick} // Cancel edit
                                            >
                                                Cancel
                                            </button>
                                        </>
                                    ) : (
                                        <>
                                            <button
                                                className="btn btn-outline-primary btn-sm me-2"
                                                onClick={() => handleEditClick(prop)}
                                            >
                                                Edit
                                            </button>
                                            <button className="btn btn-outline-danger btn-sm">
                                                Delete
                                            </button>
                                        </>
                                    )}

                                </td>
                            </tr>
                        );
                    })}
                    {filteredProperties.length === 0 && (
                        <tr>
                            <td colSpan="4" className="text-center text-muted">
                                No properties defined for this vehicle type.
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}
