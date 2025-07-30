import ListingPageItem from './ListingPageItem';
import useAllVehicles from '../hooks/useAllVehicles';

export default function ListingPage() {

    const { vehicles, loading, error } = useAllVehicles();

    return (
        <div className="listing-page">
            <div className="container">
                <div className="row">
                    <div className="col-lg-12">
                        <div className="row">
                            {vehicles.map(vehicle => <ListingPageItem
                                key={vehicle.id}
                                id={vehicle.id}
                                make={vehicle.make}
                                model={vehicle.model}
                                vehicleType={vehicle.vehicleType}
                                vehicleTypeCategory={vehicle.vehicleTypeCategory}
                                yearOfProduction={vehicle.yearOfProduction}
                                pricePerDay={vehicle.pricePerDay}
                                fuelType={vehicle.fuelType}
                                imageURL={vehicle.imageURL}
                                ownerName={vehicle.ownerName}
                                starsRating={vehicle.starsRating}
                                reviewCount={vehicle.reviewCount}
                            />)}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
