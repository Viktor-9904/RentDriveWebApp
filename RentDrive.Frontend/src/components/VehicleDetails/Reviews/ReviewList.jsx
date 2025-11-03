import "./ReviewList.css"

export default function ReviewList({ reviews }) {
    if (!reviews || reviews.length === 0) {
        return (
            <div className="review-list">
                <div className="row justify-content-center mt-4">
                    <div className="col-md-8 col-lg-6">
                        <div className="orange-box p-4">
                            <h5 className="review-section-heading mb-3">User Reviews</h5>
                            <p className="no-reviews-text mb-0">
                                This vehicle hasn't been reviewed yet.
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        );
    }

    return (
        <div className="review-list">
            <div className="row justify-content-center mt-4">
                <div className="col-md-8 col-lg-6">
                    <div className="orange-box p-4">
                        <h5 className="review-section-heading mb-5">User Reviews</h5>
                        {reviews.map((review, index) => (
                            <div key={index} className="individual-review p-3 mb-3 shadow-sm bg-white rounded">
                                <div className="d-flex justify-content-between align-items-center mb-2">
                                    <span className="review-username">{review.username}</span>
                                    {typeof review.starRating === 'number' && (
                                        <ul className="d-flex list-unstyled mb-0 review-stars">
                                            {[...Array(5)].map((_, i) => {
                                                if (review.starRating >= i + 1) {
                                                    return <li key={i}><i className="fa fa-star"></i></li>;
                                                } else if (review.starRating > i && review.starRating < i + 1) {
                                                    return <li key={i}><i className="fa fa-star-half-o"></i></li>;
                                                } else {
                                                    return <li key={i}><i className="fa fa-star-o"></i></li>;
                                                }
                                            })}
                                            <li className="ms-2"></li>

                                        </ul>
                                    )}
                                </div>
                                {review.comment && (
                                    <p className="review-comment mb-0">{review.comment}</p>
                                )}
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </div>
    );
}
