import PageHeading from "../components/shared/PageHeading/PageHeading";
import VehicleTypes from "../components/VehicleTypes/VehicleTypes";

export default function ManageVehicleTypes (){
    return(
        <>
            <PageHeading
                mainTitle="Manage Vehicle Types"
                subTitle="Inventory Management"
            />
            <VehicleTypes/>
        </>
    )
}