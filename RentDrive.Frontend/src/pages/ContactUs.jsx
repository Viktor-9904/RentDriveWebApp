import ContactPage from "../components/Contact-Us/ContactPage";
import PageHeading from "../components/shared/PageHeading/PageHeading";

export default function ContactUs() {
    return (
        <>
            <PageHeading
                subTitle="Keep in touch with us"
                mainTitle="Feel free to send us a message"
            />
            <ContactPage/>
        </>
    )
}