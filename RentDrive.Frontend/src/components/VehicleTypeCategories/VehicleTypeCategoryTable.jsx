import { useEffect, useState } from "react";

import { useBackendURL } from "../../hooks/useBackendURL";

import VehicleTypeCategoryTableItem from "./VehicleTypeCategoryTableItem";
import DeleteConfirmationModal from "../shared/DeleteConfirmationModal/DeleteConfirmationModal";

export default function VehicleTypeCategoryTable({
  categories,
  vehicleTypes,
  isNew,
  setIsNew,
  newCategory,
  setNewCategory
}) {
  const backEndURL = useBackendURL();
  const [localCategories, setLocalCategories] = useState(categories);
  const [categoryToDelete, setCategoryToDelete] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [inputModel, setInputModel] = useState(null);

  useEffect(() => {
    setLocalCategories(categories);
  }, [categories]);

  const handleEditClick = (category) => {
    setInputModel({ ...category });
  };

  const handleCancelClick = () => {
    setInputModel(null);
  };

  const handleSaveClick = async () => {
    const payload = isNew ? {
      ...newCategory,
      name: newCategory.name.trim(),
      description: newCategory.description.trim(),
    } : {
      id: inputModel.id,
      name: inputModel.name.trim(),
      description: inputModel.description.trim(),
      vehicleTypeId: inputModel.vehicleTypeId,
    };

    if (!payload.name || !payload.vehicleTypeId || !payload.description) {
      alert("Please enter valid data!");
      return;
    }

    try {
      const method = isNew ? "POST" : "PUT";
      const url = `${backEndURL}/api/vehicletypecategory/${isNew ? "create" : "edit"}/${isNew ? "" : payload.id}`;
      const response = await fetch(url, {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        throw new Error(`Failed to ${isNew ? "add" : "edit"} vehicle type category`);
      }

      const savedCategory = await response.json();

      setLocalCategories((prev) =>
        isNew ? [...prev, savedCategory] : prev.map((c) => (c.id === savedCategory.id ? savedCategory : c))
      );

      setIsNew(false);
      setNewCategory({ name: "", description: "", vehicleTypeId: "" });
      setInputModel(null);
    } catch (err) {
      alert(err.message);
    }
  };

  const handleDeleteClick = (category) => {
    setCategoryToDelete(category);
    setShowDeleteModal(true);
  };

  const confirmDelete = async () => {
    try {
      const response = await fetch(`${backEndURL}/api/vehicletypecategory/delete/${categoryToDelete.id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        throw new Error("Delete failed");
      }

      setLocalCategories((prev) => prev.filter((c) => c.id !== categoryToDelete.id));
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
              <th>Name</th>
              <th>Description</th>
              <th>Vehicle Type</th>
              <th style={{ width: "180px" }}>Actions</th>
            </tr>
          </thead>
          <tbody>
            {localCategories.length > 0 ? (
              localCategories.map((cat) => (
                <VehicleTypeCategoryTableItem
                  key={cat.id}
                  category={cat}
                  inputModel={inputModel}
                  setInputModel={setInputModel}
                  onEditClick={handleEditClick}
                  onSaveClick={handleSaveClick}
                  onCancelClick={handleCancelClick}
                  onDeleteClick={handleDeleteClick}
                  vehicleTypes={vehicleTypes}
                />
              ))
            ) : (
              <tr>
                <td colSpan="4" className="text-center text-muted">
                  No vehicle type categories defined.
                </td>
              </tr>
            )}
            {isNew && (
              <tr>
                <td>
                  <input
                    className="form-control"
                    value={newCategory.name}
                    onChange={(e) =>
                      setNewCategory((prev) => ({ ...prev, name: e.target.value }))
                    }
                  />
                </td>
                <td>
                  <input
                    className="form-control"
                    value={newCategory.description}
                    onChange={(e) =>
                      setNewCategory((prev) => ({ ...prev, description: e.target.value }))
                    }
                  />
                </td>
                <td>
                  <select
                    className="form-select"
                    value={newCategory.vehicleTypeId}
                    onChange={(e) =>
                      setNewCategory((prev) => ({
                        ...prev,
                        vehicleTypeId: e.target.value,
                      }))
                    }
                  >
                    <option value="">Select Vehicle Type</option>
                    {vehicleTypes.map((vt) => (
                      <option key={vt.id} value={vt.id}>
                        {vt.name}
                      </option>
                    ))}
                  </select>
                </td>
                <td className="text-center">
                  <button className="btn btn-success btn-sm me-2" onClick={handleSaveClick}>
                    Save
                  </button>
                  <button
                    className="btn btn-secondary btn-sm"
                    onClick={() => {
                      setIsNew(false);
                      setNewCategory({ name: "", description: "", vehicleTypeId: "" });
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
        item={`Vehicle Category - ${categoryToDelete?.name}`}
      />
    </>
  );
}
