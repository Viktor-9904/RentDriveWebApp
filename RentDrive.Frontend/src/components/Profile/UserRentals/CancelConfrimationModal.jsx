import { ColumnsSettings } from "lucide-react";

export default function CancelConfirmationModal({ show, onClose, onConfirm, rental }) {
    if (!show) return null;

    const handleBackdropClick = (e) => {
        if (e.target.className === 'modal-backdrop') {
            onClose();
        }
    };

    return (
        <div className="modal-backdrop" onClick={handleBackdropClick}>
            <div className="modal-content">
                <h2>Cancel Rental</h2>
                <hr />
                <p>Are you sure you want to cancel your <strong>{rental.vehicleMake} {rental.vehicleModel}</strong> rental?</p>
                <p>You will receive a full refund.</p>
                <p>
                    Rental Period: <strong>
                        {new Date(rental.startDate).toLocaleDateString("en-GB", {
                            day: "numeric",
                            month: "long",
                            year: "numeric",
                        })} -{" "}
                        {new Date(rental.endDate).toLocaleDateString("en-GB", {
                            day: "numeric",
                            month: "long",
                            year: "numeric",
                        })}
                    </strong>
                </p>
                <div className="modal-actions">
                    <button onClick={onClose} className="btn cancel">Keep Rental</button>
                    <button onClick={() => onConfirm(rental.id)} className="btn delete">Cancel Rental</button>
                </div>
            </div>
        </div>

    );
}
