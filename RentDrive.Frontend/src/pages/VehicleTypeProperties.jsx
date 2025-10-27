import PageHeading from "../components/shared/PageHeading/PageHeading";
import VehicleTypePropertiesEditor from "../components/VehicleTypeProperties/VehicleTypeProperties";

export default function VehicleTypeProperties() {
    return (
        <>
            <PageHeading
                mainTitle="Manage Vehicle Type Properties"
                subTitle="Inventory Management"
            />
            <VehicleTypePropertiesEditor />
        </>
    )
}