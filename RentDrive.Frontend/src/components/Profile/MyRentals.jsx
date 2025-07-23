import { useUserRentals } from "./hooks/useUserRentals";

export default function MyRentals() {
    const { rentals, loading, error } = useUserRentals();
    const backEndURL = import.meta.env.VITE_API_URL;

    if (loading) return <p>Loading rentals...</p>;
    if (error) return <p>Error: {error}</p>;

    return (
        <div className="rentals-container">
            <h3 className="rentals-heading">My Rentals</h3>

            <table className="rentals-table">
                <thead>
                    <tr>
                        <th>Vehicle</th>
                        <th>Status</th>
                        <th>Start Date</th>
                        <th>End Date</th>
                        <th>Price Per Day</th>
                        <th>Total Price</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {rentals.map((rental) => (
                        <tr key={rental.id}>
                            <td className="vehicle-cell">
                                <img
                                    src={`${backEndURL}/${rental.imageUrl}`}
                                    alt={`${rental.vehicleMake} ${rental.vehicleModel}`}
                                    className="vehicle-image"
                                />
                                {rental.vehicleMake} {rental.vehicleModel}
                            </td>
                            <td>
                                <span className={`rental-status ${rental.status.toLowerCase()}`}>
                                    {rental.status}
                                </span>
                            </td>
                            <td className="date-cell">{new Date(rental.startDate).toLocaleDateString('en-GB', { day: 'numeric', month: 'long', year: 'numeric' })}</td>
                            <td className="date-cell">{new Date(rental.endDate).toLocaleDateString('en-GB', { day: 'numeric', month: 'long', year: 'numeric' })}</td>

                            <td className="price-cell">{rental.pricePerDay.toFixed(2)} €</td>
                            <td className="price-cell">{rental.totalPrice.toFixed(2)} €</td>
                            <td>
                                {rental.status === "Active" && (
                                    <button
                                        className="confirm-button"
                                        onClick={() => console.log(`Confirm rental ${rental.id}`)}
                                    >
                                        Confirm
                                    </button>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}
