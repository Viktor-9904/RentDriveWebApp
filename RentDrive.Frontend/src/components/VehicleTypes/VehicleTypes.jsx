import { useEffect, useState } from "react";

import useAllVehicleTypes from "../Vehicles/hooks/useAllVehicleTypes";
import VehicleTypeTable from "./VehicleTypeTable";

export default function VehicleTypes() {
  const { vehicleTypes, loadingVehicleTypes, errorVehicleTypes } = useAllVehicleTypes();
  const [isNew, setIsNew] = useState(false);
  const [newTypeName, setNewTypeName] = useState("");

  return (
    <div className="container py-5">
      <div className="d-flex justify-content-end mb-3">
        <button className="btn btn-success" onClick={() => setIsNew(true)}>
          + Add New Type
        </button>
      </div>

      <VehicleTypeTable
        vehicleTypes={vehicleTypes}
        isNew={isNew}
        setIsNew={setIsNew}
        newTypeName={newTypeName}
        setNewTypeName={setNewTypeName}
      />
    </div>
  );
}
