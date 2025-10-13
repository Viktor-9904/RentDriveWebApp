import { useEffect, useState } from "react";
import VehicleTypeTableItem from "./VehicleTypeTableItem";
import { useBackendURL } from "../../hooks/useBackendURL";
import DeleteConfirmationModal from "../shared/DeleteConfirmationModal/DeleteConfirmationModal";

export default function VehicleTypeTable({
  vehicleTypes,
  isNew,
  setIsNew,
  newTypeName,
  setNewTypeName,
}) {
  const backEndURL = useBackendURL();

  const [vehicleTypeToDelete, setVehicleTypeToDelete] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [localVehicleTypes, setLocalVehicleTypes] = useState(vehicleTypes);
  const [inputModel, setInputModel] = useState(null);

  useEffect(() => {
    setLocalVehicleTypes(vehicleTypes);
  }, [vehicleTypes]);

  const handleEditClick = (type) => {
    setInputModel({
      id: type.id,
      name: type.name,
    });
  };

  const handleCancelClick = () => {
    setInputModel(null);
  };

const handleSaveClick = async () => {
  try {
    const payload = {
      id: inputModel?.id,
      name: isNew ? newTypeName?.trim() : inputModel?.name?.trim(),
      isNew: isNew,
    };

    if (!payload.name) {
      alert("Name cannot be empty.");
      return;
    }

    const url = `${backEndURL}/api/vehicletype/${payload.isNew ? "create" : "edit"}${payload.isNew ? "" : "/" + payload.id}`;
    const method = payload.isNew ? "POST" : "PUT";

    const response = await fetch(url, {
      method,
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    if (!response.ok) {
      throw new Error(`Failed to ${payload.isNew ? "add" : "edit"} vehicle type`);
    }

    const savedType = await response.json();

    setLocalVehicleTypes((prev) => {
      if (payload.isNew) {
        return [...prev, savedType];
      } else {
        return prev.map((vt) => (vt.id === savedType.id ? savedType : vt));
      }
    });

    setInputModel(null);
    setIsNew(false);
    setNewTypeName("");

  } catch (error) {
    alert(error.message);
  }
};

  const handleDeleteClick = (type) => {
    setVehicleTypeToDelete(type);
    setShowDeleteModal(true);
  };

  const confirmDelete = async () => {
    try {
      const response = await fetch(
        `${backEndURL}/api/vehicletype/delete/${vehicleTypeToDelete.id}`,
        {
          method: "DELETE",
        }
      );

      if (!response.ok) throw new Error("Failed to delete vehicle type");

      setLocalVehicleTypes((prev) => prev.filter((vt) => vt.id !== vehicleTypeToDelete.id));
      setShowDeleteModal(false);
    } catch (err) {
      alert(err.message);
    }

    setShowDeleteModal(false);
  };

  return (
    <>
      <div className="table-responsive">
        <table className="table table-bordered table-hover align-middle">
          <thead className="table-light">
            <tr>
              <th>Vehicle Type Name</th>
              <th style={{ width: "180px" }}>Actions</th>
            </tr>
          </thead>
          <tbody>
            {localVehicleTypes.length > 0 ? (
              localVehicleTypes.map((type) => (
                <VehicleTypeTableItem
                  key={type.id}
                  type={type}
                  inputModel={inputModel}
                  setInputModel={setInputModel}
                  onEditClick={handleEditClick}
                  onSaveClick={handleSaveClick}
                  onCancelClick={handleCancelClick}
                  onDeleteClick={handleDeleteClick}
                />
              ))
            ) : (
              <tr>
                <td colSpan="2" className="text-center text-muted">
                  No vehicle types defined.
                </td>
              </tr>
            )}
            {isNew && (
              <tr className="text-center">
                <td>
                  <input
                    type="text"
                    className="form-control"
                    value={newTypeName}
                    onChange={(e) => setNewTypeName(e.target.value)}
                    placeholder="Enter vehicle type name"
                    required
                  />
                </td>
                <td>
                  <button
                    className="btn btn-success btn-sm me-2"
                    onClick={handleSaveClick}
                    disabled={!newTypeName.trim()}
                  >
                    Save
                  </button>
                  <button
                    className="btn btn-secondary btn-sm"
                    onClick={() => {
                      setIsNew(false);
                      setNewTypeName("");
                    }}
                  >
                    Cancel
                  </button>
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>

      <DeleteConfirmationModal
        show={showDeleteModal}
        onClose={() => setShowDeleteModal(false)}
        onConfirm={confirmDelete}
        item={`Vehicle Type - ${vehicleTypeToDelete?.name}`}
      />
    </>
  );
}
