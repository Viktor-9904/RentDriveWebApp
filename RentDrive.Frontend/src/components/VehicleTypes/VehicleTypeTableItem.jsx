export default function VehicleTypeTableItem({
  type,
  editModel,
  setEditModel,
  onEditClick,
  onSaveClick,
  onCancelClick,
  onDeleteClick,
}) {
  const isEditing = editModel?.id === type.id;

  return (
    <tr>
      <td className="text-center">
        <input
          type="text"
          className="form-control"
          value={isEditing ? editModel.name : type.name}
          readOnly={!isEditing}
          required
          onChange={(e) =>
            isEditing && setEditModel(prev => ({ ...prev, name: e.target.value }))
          }
        />
      </td>
      <td className="text-center">
        {isEditing ? (
          <>
            <button className="btn btn-success btn-sm me-2" onClick={onSaveClick}>
              Save
            </button>
            <button className="btn btn-secondary btn-sm" onClick={onCancelClick}>
              Cancel
            </button>
          </>
        ) : (
          <>
            <button className="btn btn-outline-primary btn-sm me-2" onClick={() => onEditClick(type)}>
              Edit
            </button>
            <button className="btn btn-outline-danger btn-sm" onClick={() => onDeleteClick(type)}>
              Delete
            </button>
          </>
        )}
      </td>
    </tr>
  );
}
