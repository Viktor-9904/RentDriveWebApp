import PageHeading from "../../components/shared/PageHeading/PageHeading";
import ProfileLayout from "../../components/Profile/ProfileLayout/ProfileLayout";

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