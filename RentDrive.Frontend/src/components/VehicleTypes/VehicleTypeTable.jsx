import { useEffect, useState } from "react";
import DeleteConfirmationModal from "./DeleteConfrimationModal";
import VehicleTypeTableItem from "./VehicleTypeTableItem";

export default function VehicleTypeTable({ vehicleTypes }) {
  const backEndURL = import.meta.env.VITE_API_URL;

  const [vehicleTypeToDelete, setVehicleTypeToDelete] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [localVehicleTypes, setLocalVehicleTypes] = useState(vehicleTypes);
  const [editModel, setEditModel] = useState(null);

  useEffect(() => {
    setLocalVehicleTypes(vehicleTypes);
  }, [vehicleTypes]);

  const handleEditClick = (type) => {
    setEditModel({ id: type.id, name: type.name });
  };

  const handleCancelClick = () => {
    setEditModel(null);
  };

  const handleSaveClick = async () => {
    if (!editModel?.name.trim()) {
      alert("Name cannot be empty.");
      return;
    }

    try {
      const response = await fetch(`${backEndURL}/api/vehicletype/edit/${editModel.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(editModel),
      });

      if (!response.ok) throw new Error("Failed to edit vehicle type");

      setLocalVehicleTypes(prev =>
        prev.map(vt => vt.id === editModel.id ? { ...vt, name: editModel.name } : vt)
      );

      setEditModel(null);
    } catch (err) {
      alert(err.message);
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

      setLocalVehicleTypes(prev =>
        prev.filter(vt => vt.id !== vehicleTypeToDelete.id)
      );
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
                  editModel={editModel}
                  setEditModel={setEditModel}
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
          </tbody>
        </table>
      </div>

      <DeleteConfirmationModal
        show={showDeleteModal}
        onClose={() => setShowDeleteModal(false)}
        onConfirm={confirmDelete}
        itemName={vehicleTypeToDelete?.name}
      />
    </>
  );
}
