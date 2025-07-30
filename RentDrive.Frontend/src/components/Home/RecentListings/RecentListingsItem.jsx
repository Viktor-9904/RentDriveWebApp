import { useEffect } from 'react';
import { Link } from 'react-router-dom'
import useVehicleAverageStarRating from '../../Vehicles/hooks/useVehicleAverageStarRating';
import useVehicleReviewsCount from '../../Vehicles/hooks/useVehicleReviewCount';

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
}) {

    const backEndURL = import.meta.env.VITE_API_URL;
    const { averageStarRating, loadingAverageStarRating, errorAverageStarRating } = useVehicleAverageStarRating(id);
    const { vehicleReviewsCount, loadingVehicleReviewsCount, errorVehicleReviewsCount } = useVehicleReviewsCount(id);

    return (
        <div className="col-lg-12">
            <Link to={`/api/vehicle/${id}`}>
                <div className="owl-carousel owl-listing">
                    <div className="item">
                        <div className="row">
                            <div className="col-lg-12">
                                <div className="listing-item ">
                                    <div className="left-image">
                                        <img src={`${backEndURL}/${imageURL}`} alt="" />
                                    </div>
                                    <div className="right-content align-self-center">
                                        <h4>{`${make} - ${model}`}</h4>
                                        {ownerName && ownerName.length > 0 && <h6>Owner: {`${ownerName}`}</h6>}
                                        <ul className="rate">
                                            {Array.from({ length: 5 }).map((_, index) => {
                                                const rating = averageStarRating;

                                                if (rating >= index + 1) {
                                                    return <li key={index}><i className="fa fa-star"></i></li>;
                                                } else if (rating >= index + 0.5) {
                                                    return <li key={index}><i className="fa fa-star-half-o"></i></li>;
                                                } else {
                                                    return <li key={index}><i className="fa fa-star-o"></i></li>;
                                                }
                                            })}
                                            <li>({vehicleReviewsCount}) Reviews</li>
                                        </ul>
                                        <span className="price"><div className="icon"><img src="assets/images/listing-icon-01.png" alt="" /></div> {`${pricePerDay.toFixed(2)} € / per day with taxes.`}</span>
                                        <span className="details">Type: <em>{vehicleType}</em></span>
                                        <span className="details">Category: <em>{vehicleTypeCategory}</em></span>
                                        <span className="details">Year of production: <em>{yearOfProduction}</em></span>
                                        <span className="details">Fuel: <em>{fuelType}</em></span>
                                        <span className="details">Description: <em>{description}</em></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </Link>
        </div>
    )
}