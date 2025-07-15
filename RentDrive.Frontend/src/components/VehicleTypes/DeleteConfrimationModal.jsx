import React from 'react';

export default function DeleteConfirmationModal({ show, onClose, onConfirm, itemName }) {
    if (!show) return null;

    const handleBackdropClick = (e) => {
        if (e.target.className === 'modal-backdrop') {
            onClose();
        }
    };

    return (
        <div className="modal-backdrop" onClick={handleBackdropClick}>
            <div className="modal-content">
                <h2>Confirm Deletion</h2>
                <p>Are you sure you want to delete <strong>{itemName}</strong>?</p>
                <div className="modal-actions">
                    <button onClick={onClose} className="btn cancel">Cancel</button>
                    <button onClick={onConfirm} className="btn delete">Delete</button>
                </div>
            </div>
        </div>
    );
}
