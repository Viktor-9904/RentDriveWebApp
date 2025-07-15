import { useState } from "react";

export default function VehicleTypeTableItem({
  type,
  onEditClick,
  onSaveClick,
  onCancelClick,
  onDeleteClick,
}) {
  const [isEditing, setIsEditing] = useState(false);
  const [editName, setEditName] = useState(type.name);

  const handleEdit = () => {
    setIsEditing(true);
    onEditClick(type.id);
  };

  const handleCancel = () => {
    setIsEditing(false);
    setEditName(type.name);
    onCancelClick();
  };

  const handleSave = () => {
    onSaveClick(type.id, { name: editName });
    setIsEditing(false);
  };

  return (
    <tr>
      <td>
        <input
          type="text"
          className="form-control"
          value={editName}
          readOnly={!isEditing}
          onChange={(e) => setEditName(e.target.value)}
        />
      </td>
      <td className="text-center">
        {isEditing ? (
          <>
            <button className="btn btn-success btn-sm me-2" onClick={handleSave}>
              Save
            </button>
            <button className="btn btn-secondary btn-sm" onClick={handleCancel}>
              Cancel
            </button>
          </>
        ) : (
          <>
            <button className="btn btn-outline-primary btn-sm me-2" onClick={handleEdit}>
              Edit
            </button>
            <button
              className="btn btn-outline-danger btn-sm"
              onClick={() => onDeleteClick(type)}
            >
              Delete
            </button>
          </>
        )}
      </td>
    </tr>
  );
}