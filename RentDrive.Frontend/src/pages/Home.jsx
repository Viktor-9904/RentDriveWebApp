import { useEffect } from "react";

import MainBanner from "../components/Home/MainBanner";
import RecentListing from "../components/Home/RecentListings/RecentListings";

export default function Home() {
    return (
        <>
            <MainBanner />
            <RecentListing />
        </>
    )
}