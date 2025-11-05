import { useEffect } from 'react';
import { Link } from 'react-router-dom'
import StarRating from '../../shared/VehicleStarRating/VehicleStarRating';
import { useBackendURL } from '../../../hooks/useBackendURL';

export default function RecentListingsItem({
    id,
    make,
    model,
    imageURL,
    pricePerDay,
    description,
    ownerName,
    yearOfProduction,
    fuelType,
    vehicleType,
    vehicleTypeCategory,
    starsRating,
    reviewCount
}) {

    const backEndURL = useBackendURL();

    return (
        <div className="col-lg-12">
            <Link to={`/vehicle/${id}`}>
                    <div className="item">
                        <div className="row">
                            <div className="col-lg-12">
                                <div className="listing-item ">
                                    <div className="left-image">
                                        <img src={`${backEndURL}/${imageURL}`} alt="" />
                                    </div>
                                    <div className="right-content align-self-center">
                                        <StarRating rating={starsRating} reviewCount={reviewCount} />
                                        <h4>{`${make} - ${model}`}</h4>
                                        <span className="price"><div className="icon"><img src="assets/images/listing-icon-01.png" alt="" /></div> {`${pricePerDay.toFixed(2)} € / per day with taxes.`}</span>
                                        <hr></hr>
                                        {ownerName && ownerName.length > 0 && <span className="details"><strong>Property of:</strong> {`${ownerName}`}</span>}
                                        <span className="details">Type: <em>{vehicleType} - {vehicleTypeCategory}</em></span>
                                        <span className="details">Year of production: <em>{yearOfProduction}</em></span>
                                        <span className="details">Fuel: <em>{fuelType}</em></span>
                                        <span className="details">Description: <em>{description}</em></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </Link>
        </div>
    )
}