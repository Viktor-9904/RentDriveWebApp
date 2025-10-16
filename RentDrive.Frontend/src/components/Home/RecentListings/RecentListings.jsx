import { useEffect, useState } from 'react';

import RecentListingsItem from './RecentListingsItem';
import Spinner from '../../shared/Spinner/Spinner';
import useRecentVehicles from '../../Vehicles/hooks/useRecentVehicles';

export default function RecentListing() {

    const { recentVehicles, recentVehiclesLoading, recentVehiclesError } = useRecentVehicles();
    const [localRecentVehicles, setLocalRecentVehicles] = useState([])

    useEffect(() => {
        setLocalRecentVehicles(recentVehicles)
    }, [recentVehicles])


    if (recentVehiclesLoading) return <Spinner message={"Recent Vehicles"} />

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
                        {localRecentVehicles.length > 0 &&
                            localRecentVehicles.map(vehicle => <RecentListingsItem
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