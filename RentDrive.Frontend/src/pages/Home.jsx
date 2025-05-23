import MainBanner from "../components/Home/MainBanner";
import PopularCategories from "../components/Home/PopularCategories";
import RecentListing from "../components/Home/RecentListings";

export default function Home() {
    return (
        <>
            <MainBanner/>
            <RecentListing/>
            <PopularCategories/>
        </>
    )
}