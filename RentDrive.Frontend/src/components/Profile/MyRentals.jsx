export default function MyRentals() {
    const rentals = [
        {
            id: 1,
            vehicleName: "Tesla Model 3",
            imageUrl: "https://cdn.prod.website-files.com/60ce1b7dd21cd517bb39ff20/6807a8c7597e6b65fa818511_tesla_model3.jpg",
            status: "Active",
            startDate: "07/20/2025",
            endDate: "07/22/2025",
            pricePerDay: 60,
            totalPrice: 180,
        },
        {
            id: 2,
            vehicleName: "Yamaha MT-07",
            imageUrl: "https://cdn.dealerspike.com/imglib/v1/800x600/imglib/Assets/Inventory/F7/58/F7584634-5D75-48ED-B549-121FCE479848.jpg",
            status: "Completed",
            startDate: "06/10/2025",
            endDate: "06/13/2025",
            pricePerDay: 40,
            totalPrice: 120,
        },
    ];


    const handleConfirm = () => {
        console.log("Confirmed rental");

    }

    function formatDate(dateStr) {
        const date = new Date(dateStr);
        return date.toLocaleDateString('en-GB', {
            day: 'numeric',
            month: 'long',
            year: 'numeric'
        });
    }

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
                                    src={rental.imageUrl}
                                    alt={rental.vehicleName}
                                    className="vehicle-image"
                                />
                                {rental.vehicleName}
                            </td>
                            <td>
                                <span className={`rental-status ${rental.status.toLowerCase()}`}>
                                    {rental.status}
                                </span>
                            </td>
                            <td className="date-cell">{formatDate(rental.startDate)}</td>
                            <td className="date-cell">{formatDate(rental.endDate)}</td>

                            <td>${rental.pricePerDay}</td>
                            <td>${rental.totalPrice}</td>
                            <td>
                                {rental.status === "Active" && (
                                    <button
                                        className="confirm-button"
                                        onClick={() => handleConfirm(rental.id)}
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
