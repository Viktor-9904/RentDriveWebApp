import { useState } from "react";
import useAllVehicleCategories from "../Vehicles/hooks/useAllVehicleCategories";
import useAllVehicleTypes from "../Vehicles/hooks/useAllVehicleTypes";
import VehicleTypeCategoryTable from "./VehicleTypeCategoryTable";

export default function VehicleTypeCategories() {
  const { vehicleCategories, loadingVehicleCategories, errorVehicleCategories } = useAllVehicleCategories();
  const { vehicleTypes, loadingVehicleTypes, errorVehicleTypes } = useAllVehicleTypes();

  const [isNew, setIsNew] = useState(false);
  const [newCategory, setNewCategory] = useState({
    name: "",
    description: "",
    vehicleTypeId: ""
  });

  return (
    <div className="container py-5">
      <h2 className="mb-4">Manage Vehicle Type Categories</h2>

      <div className="d-flex justify-content-end mb-3">
        <button className="btn btn-success" onClick={() => setIsNew(true)}>
          + Add New Category
        </button>
      </div>

      <VehicleTypeCategoryTable
        categories={vehicleCategories}
        vehicleTypes={vehicleTypes}
        isNew={isNew}
        setIsNew={setIsNew}
        newCategory={newCategory}
        setNewCategory={setNewCategory}
      />
    </div>
  );
}
