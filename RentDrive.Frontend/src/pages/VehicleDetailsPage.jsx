import PageHeading from "../components/shared/PageHeading";
import VehicleDetails from "../components/VehicleDetails/VehicleDetails/VehicleDetails";

export default function VehicleDetailsPage() {
    return (
        <>
            <PageHeading
                mainTitle="Vehicle Details"
                subTitle="Specifications and availability"
                topPadding={200}
                bottomPadding={90}
            />
            <VehicleDetails/>
        </>
    )
}