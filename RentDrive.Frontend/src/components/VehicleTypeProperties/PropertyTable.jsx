export default function PropertyTable({
    filteredProperties
}) {
    return(
        <div className="table-responsive">
                <table className="table table-bordered table-hover align-middle">
                    <thead className="table-light">
                        <tr>
                            <th>Property Name</th>
                            <th>Value Type</th>
                            <th>Unit</th>
                            <th style={{ width: "160px" }}>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredProperties.map(prop => (
                            <tr key={prop.id}>
                                <td>
                                    <input
                                        type="text"
                                        className="form-control"
                                        value={prop.name}
                                        readOnly
                                    />
                                </td>
                                <td>
                                    <input
                                        type="text"
                                        className="form-control"
                                        value={prop.valueType}
                                        readOnly
                                    />
                                </td>
                                <td>
                                    <input
                                        type="text"
                                        className="form-control"
                                        value={prop.unitOfMeasurement}
                                        readOnly
                                    />
                                </td>
                                <td className="text-center">
                                    <button className="btn btn-outline-primary btn-sm me-2">
                                        Edit
                                    </button>
                                    <button className="btn btn-outline-danger btn-sm">
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        ))}
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
    )
}