import PageHeading from "../../components/shared/PageHeading";
import ProfileLayout from "../../components/Profile/ProfileLayout";

export default function ProfileLayoutPage() {
    return (
        <>
            <PageHeading
                subTitle="Account Details"
                mainTitle="Manage Your Profile and Rentals"
            />
            <ProfileLayout />
        </>
    );
}