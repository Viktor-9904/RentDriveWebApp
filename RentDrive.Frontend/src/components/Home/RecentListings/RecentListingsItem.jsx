import { Link } from 'react-router-dom'

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

    return (
        <div className="col-lg-12">
            <div className="owl-carousel owl-listing">
                <div className="item">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="listing-item">
                                <div className="left-image">
                                    <Link to="#"><img src={`${backEndURL}/${imageURL}`} alt="" /></Link>
                                </div>
                                <div className="right-content align-self-center">
                                    <Link to="#"><h4>{`${make} - ${model}`}</h4></Link>
                                    {ownerName && ownerName.length > 0 && <h6>Owner: {`${ownerName}`}</h6>}
                                    <ul className="rate">
                                        <li><i className="fa fa-star-o"></i></li>
                                        <li><i className="fa fa-star-o"></i></li>
                                        <li><i className="fa fa-star-o"></i></li>
                                        <li><i className="fa fa-star-o"></i></li>
                                        <li><i className="fa fa-star-o"></i></li>
                                        <li>(18) Reviews</li>
                                    </ul>
                                    <span className="price"><div className="icon"><img src="assets/images/listing-icon-01.png" alt="" /></div> {`${pricePerDay.toFixed(2)} € / per day with taxes.`}</span>
                                    <span className="details">Type: <em>{vehicleType}</em></span>
                                    <span className="details">Category: <em>{vehicleTypeCategory}</em></span>
                                    <span className="details">Year of production: <em>{yearOfProduction}</em></span>
                                    <span className="details">Fuel: <em>{fuelType}</em></span>
                                    <span className="details">Description: <em>{description}</em></span>
                                    <div className="main-white-button">
                                        <Link to="/contact-us"><i className="fa fa-eye"></i> Open</Link>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}