import PageHeading from "../components/shared/PageHeading";
import ListingPage from "../components/Vehicles/Listing/ListingPage";

export default function Listing() {
    return (
        <>
            <PageHeading
                mainTitle="Vehicle Listings of Different Categories"
                subTitle="Checkout our Listings"
            />
            <ListingPage/>
        </>
    )
}