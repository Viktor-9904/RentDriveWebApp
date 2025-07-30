const StarRating = ({ rating = 0, reviewCount = 0 }) => {
    const fullStars = Math.floor(rating);
    const hasHalfStar = rating - fullStars >= 0.5;
    const emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);

    return (
        <ul className="rate d-flex list-unstyled align-items-center mb-2">
            {[...Array(fullStars)].map((_, i) => (
                <li key={`full-${i}`}>
                    <i className="fa fa-star"></i>
                </li>
            ))}
            {hasHalfStar && (
                <li>
                    <i className="fa fa-star-half-o"></i>
                </li>
            )}
            {[...Array(emptyStars)].map((_, i) => (
                <li key={`empty-${i}`}>
                    <i className="fa fa-star-o"></i>
                </li>
            ))}
            <li className="ms-2">({reviewCount} Reviews)</li>
        </ul>
    );
};

export default StarRating;
