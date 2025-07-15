import { useEffect, useState } from "react";

import useAllVehicleTypes from "../Vehicles/hooks/useAllVehicleTypes";
import VehicleTypeTable from "./VehicleTypeTable";

export default function VehicleTypes() {

    const { vehicleTypes, loadingVehicleTypes, errorVehicleTypes } = useAllVehicleTypes();

    const handleAddNewProperty = () => {

    };

    return (
        <div className="container py-5">
            <h2 className="mb-4">Manage Vehicle Types</h2>

            <div className="d-flex justify-content-end mb-3">
                <button className="btn btn-success" onClick={handleAddNewProperty}>
                    + Add New Type
                </button>
            </div>

            <VehicleTypeTable
                vehicleTypes={vehicleTypes}
            />

        </div>
    );
}