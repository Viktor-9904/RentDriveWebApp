export default function VehicleTypeCategoryTableItem({
  category,
  inputModel,
  setInputModel,
  onEditClick,
  onSaveClick,
  onCancelClick,
  onDeleteClick,
  vehicleTypes,
}) {
  const isEditing = inputModel?.id === category.id;
  const vehicleTypeName = vehicleTypes.find(v => v.id === category.vehicleTypeId)?.name || "";

  return (
    <tr>
      <td className="text-center">
        <input
          className="form-control"
          value={isEditing ? inputModel.name : category.name}
          readOnly={!isEditing}
          required
          onChange={(e) =>
            isEditing &&
            setInputModel((prev) => ({ ...prev, name: e.target.value }))
          }
        />
      </td>
      <td className="text-center">
        <input
          className="form-control"
          type="text-area"
          value={isEditing ? inputModel.description : category.description}
          readOnly={!isEditing}
          required
          onChange={(e) =>
            isEditing &&
            setInputModel((prev) => ({ ...prev, description: e.target.value }))
          }
        />
      </td>
      <td className="text-center">
        <input
          className="form-control"
          value={vehicleTypeName}
          readOnly
          required
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
            <button
              className="btn btn-outline-primary btn-sm me-2"
              onClick={() => onEditClick(category)}
            >
              Edit
            </button>
            <button
              className="btn btn-outline-danger btn-sm"
              onClick={() => onDeleteClick(category)}
            >
              Delete
            </button>
          </>
        )}
      </td>
    </tr>
  );
}
