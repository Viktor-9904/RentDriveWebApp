import { useEffect } from "react";

import MainBanner from "../components/Home/MainBanner";
import PopularCategories from "../components/Home/PopularCategories";
import RecentListing from "../components/Home/RecentListings/RecentListings";

export default function Home() {
    return (
        <>
            <MainBanner />
            <RecentListing />
            <PopularCategories />
        </>
    )
}