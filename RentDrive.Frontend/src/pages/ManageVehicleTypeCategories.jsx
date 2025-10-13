import PageHeading from "../components/shared/PageHeading";
import VehicleTypeCategories from "../components/VehicleTypeCategories/VehicleTypeCategories";

export default function ManageVehicleTypeCategories () {
    return (
        <>
            <PageHeading
                mainTitle="Manage Vehicle Type Categories"
                subTitle="Inventory Management"
            />
            <VehicleTypeCategories/>
        </>
    )
}