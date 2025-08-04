export default function AddFundsToBalance({ handleAddFunds, setShowModal }) {
    return (
        (
            <div className="wallet-modal-backdrop" onClick={() => setShowModal(false)}>
                <div className="wallet-modal" onClick={(e) => e.stopPropagation()}>
                    <h4>Add Funds</h4>
                    <input
                        type="number"
                        placeholder="Enter amount"
                    />
                    <div>
                        <button className="wallet-add-funds-button" onClick={handleAddFunds}>
                            Confirm
                        </button>
                        <button
                            className="wallet-modal-cancel-button"
                            onClick={() => setShowModal(false)}
                        >
                            Cancel
                        </button>
                    </div>
                </div>
            </div>
        )
    )
}