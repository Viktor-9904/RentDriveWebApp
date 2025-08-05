import React, { useState } from 'react';

export default function AddFundsToBalance({ handleAddFunds, setShowModal }) {
  const [formData, setFormData] = useState({
    amount: '',
    cardNumber: '',
    cardHolderName: '',
    cvv: '',
    expiryMonth: '',
    expiryYear: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;

    if (name === 'cardNumber') {
      const formatted = formatCardNumber(value);
      setFormData({ ...formData, [name]: formatted });
    } else {
      setFormData({ ...formData, [name]: value });
    }
  };

  const onConfirm = () => {
    handleAddFunds(formData);
    console.log(formData)
  };

  return (
    <div
      className="funds-modal__backdrop"
      onClick={() => setShowModal(false)}
    >
      <div
        className="funds-modal__container"
        onClick={(e) => e.stopPropagation()}
      >
        <h4 className="funds-modal__title">Add Funds</h4>

        <form onSubmit={onConfirm}>
          <div className="funds-modal__form-grid">
            <div className="funds-modal__form-group">
              <label htmlFor="amount" className="funds-modal__label">Amount</label>
              <input
                id="amount"
                name="amount"
                type="number"
                min="1"
                placeholder="Enter amount"
                value={formData.amount}
                pattern='^\d+(\.\d{1,2})?$'
                onChange={handleChange}
                className="funds-modal__input"
                required
              />
            </div>

            <div className="funds-modal__form-group">
              <label htmlFor="cardNumber" className="funds-modal__label">Card Number</label>
              <input
                id="cardNumber"
                name="cardNumber"
                type="text"
                placeholder="1234 5678 9012 3456"
                value={formData.cardNumber}
                onChange={handleChange}
                className="funds-modal__input"
                maxLength={19}
                required
              />
            </div>

            <div className="funds-modal__form-group">
              <label htmlFor="cardHolderName" className="funds-modal__label">Cardholder Name</label>
              <input
                id="cardHolderName"
                name="cardHolderName"
                type="text"
                placeholder="John Doe"
                value={formData.cardHolderName}
                onChange={handleChange}
                className="funds-modal__input"
                required
              />
            </div>

            <div className="funds-modal__form-group">
              <label htmlFor="expiryMonth" className="funds-modal__label">Expiration Date</label>
              <div className="funds-modal__expiry-dropdowns">
                <select
                  id="expiryMonth"
                  name="expiryMonth"
                  value={formData.expiryMonth}
                  pattern='`^(0[1-9]'
                  onChange={handleChange}
                  className="funds-modal__select"
                  required
                >
                  <option value="" disabled>Month</option>
                  {[...Array(12)].map((_, i) => {
                    const monthNumber = (i + 1).toString().padStart(2, '0');
                    return (
                      <option key={monthNumber} value={monthNumber}>
                        {new Date(0, i).toLocaleString('default', { month: 'short' })} ({monthNumber})
                      </option>
                    );
                  })}
                </select>

                <select
                  id="expiryYear"
                  name="expiryYear"
                  value={formData.expiryYear}
                  pattern='^\d{2}$'
                  onChange={handleChange}
                  className="funds-modal__select"
                  required
                >
                  <option value="" disabled>Year</option>
                  {Array.from({ length: 12 }, (_, i) => {
                    const year = new Date().getFullYear() + i;
                    return <option key={year} value={year.toString().slice(2)}>{year}</option>;
                  })}
                </select>
              </div>
            </div>

            <div className="funds-modal__form-group">
              <label htmlFor="cvv" className="funds-modal__label">CVV</label>
              <input
                id="cvv"
                name="cvv"
                type="text"
                maxLength={3}
                placeholder="123"
                value={formData.cvv}
                pattern='^\d{3,4}$'
                onChange={handleChange}
                className="funds-modal__input"
                required
              />
            </div>
          </div>

          <div className="funds-modal__buttons">
            <button
              type="submit"
              className="funds-modal__confirm-button"
            >
              Confirm
            </button>
            <button
              type="button"
              className="funds-modal__cancel-button"
              onClick={() => setShowModal(false)}
            >
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

function formatCardNumber(value) {
  return value
    .replace(/\D/g, '')
    .substring(0, 16)
    .replace(/(.{4})/g, '$1 ')
    .trim();
}