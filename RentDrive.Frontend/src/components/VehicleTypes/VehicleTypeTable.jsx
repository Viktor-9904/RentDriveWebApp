import { useState } from "react";
import DeleteConfirmationModal from "./DeleteConfrimationModal";
import VehicleTypeTableItem from "./VehicleTypeTableItem";

export default function VehicleTypeTable({ vehicleTypes }) {
  const [vehicleTypeToDelete, setVehicleTypeToDelete] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);

  const handleEditClick = (id) => {

  };

  const handleCancelClick = () => {
  };

  const handleSaveClick = (id, updatedData) => {

  };

  const handleDeleteClick = (type) => {
    setVehicleTypeToDelete(type);
    setShowDeleteModal(true);
  };

  const confirmDelete = () => {
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
            {vehicleTypes.length > 0 ? (
              vehicleTypes.map((type) => (
                <VehicleTypeTableItem
                  key={type.id}
                  type={type}
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