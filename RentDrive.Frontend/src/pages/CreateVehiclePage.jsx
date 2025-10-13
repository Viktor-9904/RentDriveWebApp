import PageHeading from "../components/shared/PageHeading";
import CreateVehicleForm from "../components/Vehicles/CreateVehicleForm";

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