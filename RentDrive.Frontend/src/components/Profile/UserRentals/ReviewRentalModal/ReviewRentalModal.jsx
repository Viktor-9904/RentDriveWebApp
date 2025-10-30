import "./ReviewRentalModal.css"

import { useState } from 'react';
import { FaStar } from 'react-icons/fa';
import { FaTimes } from 'react-icons/fa';

export default function ReviewModal({ show, onClose, onSubmit, rentalId }) {
    const [rating, setRating] = useState(0);
    const [hover, setHover] = useState(0);
    const [comment, setComment] = useState('');

    if (!show) return null;

    const handleSubmit = () => {
        onSubmit({ rating, comment });
        setRating(0);
        setComment('');
        onClose();
    };

    return (
        <div className="review-rental-modal-backdrop" onClick={onClose}>
            <div className="review-rental-modal-content" onClick={(e) => e.stopPropagation()}>
                <button className="review-rental-modal-close-btn" onClick={onClose}>
                    <FaTimes />
                </button>

                <h3 className="review-rental-modal-title">Leave a Review</h3>

                <div className="review-rental-modal-stars">
                    {[...Array(5)].map((_, i) => {
                        const starValue = i + 1;
                        return (
                            <label key={i}>
                                <input
                                    type="radio"
                                    name="rating"
                                    value={starValue}
                                    onClick={() => setRating(starValue)}
                                    className="review-rental-modal-radio"
                                />
                                <FaStar
                                    className="review-rental-modal-star"
                                    size={30}
                                    color={starValue <= (hover || rating) ? "#ffa534" : "#ddd"}
                                    onMouseEnter={() => setHover(starValue)}
                                    onMouseLeave={() => setHover(0)}
                                />
                            </label>
                        );
                    })}
                </div>

                <textarea
                    className="review-rental-modal-comment"
                    placeholder="Optional comment..."
                    value={comment}
                    onChange={(e) => setComment(e.target.value)}
                />

                <div className="review-rental-modal-button-container">
                    <button onClick={handleSubmit} className="review-rental-modal-submit">
                        Submit Review
                    </button>
                </div>
            </div>
        </div>
    );
}
