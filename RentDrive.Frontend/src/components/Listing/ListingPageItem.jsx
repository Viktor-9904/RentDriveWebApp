import { Link } from "react-router-dom";

export default function ListingPageItem({
    id,
    make,
    model,
    vehicleType,
    vehicleTypeCategory,
    yearOfProduction,
    pricePerDay,
    fuelType,
    imageURL,
    ownerName
}) {
    const backEndURL = import.meta.env.VITE_API_URL;

    return (
        <div className="col-lg-3 col-md-6 mb-4">
            <div className="vehicle-listing-item listing-item d-flex flex-column h-100">
                <div className="left-image mb-3 text-center">
                    <Link to={'/'}>
                        <img
                            src={`${backEndURL}${imageURL}`}
                            alt={`${make} ${model}`}
                            className="img-fluid rounded border"
                        />
                    </Link>
                </div>
                <div className="right-content d-flex flex-column justify-content-between flex-grow-1">
                    <div>
                        <Link to={'/'} className="vehicle-title-link">
                            <h4 className="mb-1 vehicle-title">{make} {model}</h4>
                        </Link>
                        <hr className="my-2" />
                        <span className="price">{`${pricePerDay.toFixed(2)} € / per day with taxes.`}</span>
                        <div className="details d-block mt-2">
                            <div>Type: <em>{vehicleType}</em></div>
                            <div>Category: <em>{vehicleTypeCategory}</em></div>
                            <div>Year: <em>{yearOfProduction}</em></div>
                            {fuelType && fuelType.length > 0 && <div>Fuel: {fuelType}</div>}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
