import { useEffect, useState } from "react";
import DeleteConfirmationModal from "./DeleteConfrimationModal";
import VehicleTypeTableItem from "./VehicleTypeTableItem";

export default function VehicleTypeTable({
  vehicleTypes
}) {

  const backEndURL = import.meta.env.VITE_API_URL;
  const [vehicleTypeToDelete, setvehicleTypeToDeleteId] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [localVehicleTypes, setLocalVehicleTypes] = useState(vehicleTypes);

  useEffect(() => {
    setLocalVehicleTypes(vehicleTypes);
  }, [vehicleTypes]);


  const handleEditClick = (id) => {

  };

  const handleCancelClick = () => {
  };

  const handleSaveClick = () => {

  };

  const handleDeleteClick = (type) => {
    setvehicleTypeToDeleteId(type);
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

      if (!response.ok) {
        throw new Error("Failed to delete vehicle type");
      }

      setLocalVehicleTypes(prev => prev.filter(vt => vt.id !== vehicleTypeToDelete.id));
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