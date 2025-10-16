import { useEffect, useState } from "react";

import VehicleTypeTable from "./VehicleTypeTable";
import Spinner from "../shared/Spinner/Spinner";

import useAllVehicleTypes from "../Vehicles/hooks/useAllVehicleTypes";

export default function VehicleTypes() {
  const { vehicleTypes, loadingVehicleTypes, errorVehicleTypes } = useAllVehicleTypes();
  const [isNew, setIsNew] = useState(false);
  const [newTypeName, setNewTypeName] = useState("");

  if (loadingVehicleTypes) return <Spinner message={"Vehicle Types"} />

  return (
    <div className="container py-5">
      <div className="d-flex justify-content-end mb-3">
        <button className="btn btn-success" onClick={() => setIsNew(true)}>
          + Add New Type
        </button>
      </div>

      <VehicleTypeTable
        vehicleTypes={vehicleTypes}
        loadingVehicleTypes={loadingVehicleTypes}
        isNew={isNew}
        setIsNew={setIsNew}
        newTypeName={newTypeName}
        setNewTypeName={setNewTypeName}
      />
    </div>
  );
}
