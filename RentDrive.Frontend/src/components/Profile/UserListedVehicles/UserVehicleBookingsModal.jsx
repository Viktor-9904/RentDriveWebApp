import React from "react";

export default function UserVehicleBookingsModal({ show, onClose, vehicle, bookings }) {
  if (!show || !vehicle) return null;

  const handleBackdropClick = (e) => {
    if (e.target === e.currentTarget) {
      onClose();
    }
  };

  return (
    <div
      className="vehicle-bookings-modal-overlay"
      onClick={handleBackdropClick}
    >
      <div className="vehicle-history-modal">
        <div className="modal-header">
          <h3 className="modal-title">
            Booking History – <span className="vehicle-name">{vehicle.make} {vehicle.model}</span>
          </h3>
          <button className="close-button" onClick={onClose}>✕</button>
        </div>

        {bookings.length === 0 ? (
          <p className="no-bookings-message bordered-message">No bookings found for this vehicle.</p>
        ) : (
          <table className="vehicle-history-table">
            <thead>
              <tr>
                <th className="text-center">Renter</th>
                <th className="text-center">Booked On</th>
                <th className="text-center">Period</th>
                <th className="text-center">Status</th>
                <th className="text-center">Price Per Day</th>
                <th className="text-center">Total Price</th>
              </tr>
            </thead>
            <tbody>
              {bookings.map((booking) => (
                <tr key={booking.id}>
                  <td className="text-center">{booking.username}</td>
                  <td className="text-center">{booking.bookedOn}</td>
                  <td className="text-center">{booking.period}</td>
                  <td className="text-center">
                    <span className={`vehicle-status ${booking.status.toLowerCase()}`}>
                      {booking.status}
                    </span>
                  </td>
                  <td className="text-center">{booking.pricePerDay.toFixed(2)} €</td>
                  <td className="text-center">{booking.totalPrice.toFixed(2)} €</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
}
