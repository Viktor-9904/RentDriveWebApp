import PageHeading from "../components/shared/PageHeading/PageHeading";
import CreateVehicleForm from "../components/Vehicles/CreateVehicle/CreateVehicleForm";

export default function CreateVehiclePage() {
    return (
        <>
            <PageHeading
                mainTitle="Create a new vehicle listing"
                subTitle="Inventory managment"
                topPadding={200}
                bottomPadding={90}
            />
            <CreateVehicleForm />
        </>
    )
}