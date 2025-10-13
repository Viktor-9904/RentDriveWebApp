import "./DeleteConfirmationModal.css"

export default function DeleteConfirmationModal({ show, onClose, onConfirm, item="item name" }) {
    if (!show) return null;

    return (
        <div className="delete-modal-backdrop" onClick={onClose}>
            <div className="delete-modal-content" onClick={(e) => e.stopPropagation()}>
                <h2>Confirm Deletion</h2>
                <p>Are you sure you want to delete <strong>{item}</strong>?</p>
                <div className="delete-modal-actions">
                    <button onClick={onClose} className="btn cancel">Cancel</button>
                    <button onClick={onConfirm} className="btn delete">Delete</button>
                </div>
            </div>
        </div>
    );
}
