import { useEffect, useState } from 'react';

import RecentListingsItem from './RecentListingsItem';
import { useBackendURL } from '../../../hooks/useBackendURL';

export default function RecentListing() {

    const backEndURL = useBackendURL();
    const [recentVehicles, setRecentVehicles] = useState([])

    useEffect(() => {
        fetch(`${backEndURL}/api/vehicle/recent`)
            .then(res => res.json())
            .then(data => {
                console.log(data);
                setRecentVehicles(data)
            })
            .catch(err => console.error("API error:", err));
    }, []);

    return (
        <>
            <div className="recent-listing">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="section-heading">
                                <h2>Recent Listing</h2>
                                <h6>Check Them Out</h6>
                            </div>
                        </div>
                        {recentVehicles.length > 0 &&
                            recentVehicles.map(vehicle => <RecentListingsItem
                                key={vehicle.id}
                                id={vehicle.id}
                                make={vehicle.make} 
                                model={vehicle.model}
                                imageURL={vehicle.imageURL}
                                pricePerDay={vehicle.pricePerDay}
                                description={vehicle.description}
                                yearOfProduction={vehicle.yearOfProduction}
                                vehicleType={vehicle.vehicleType}
                                vehicleTypeCategory={vehicle.vehicleTypeCategory}
                                fuelType={vehicle.fuelType}
                                ownerName={vehicle.ownerName}
                                starsRating={vehicle.starsRating}
                                reviewCount={vehicle.reviewCount}
                            />)}
                    </div>
                </div>
            </div>
        </>
    )
}