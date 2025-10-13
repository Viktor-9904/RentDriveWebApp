import PageHeading from "../components/shared/PageHeading";
import EditVehicleForm from "../components/Vehicles/EditVehicleForm";

export default function EditVehiclePage() {
    return(
        <>
            <PageHeading
                mainTitle="Edit an existing vehicle"
                subTitle="Inventory management"
                topPadding={200}
                bottomPadding={90}
            />            
            <EditVehicleForm/>
        </>
    )
}