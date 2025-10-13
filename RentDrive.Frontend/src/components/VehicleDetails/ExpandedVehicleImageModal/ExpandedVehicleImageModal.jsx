import './ExpandedVehicleImageModal.css';

import { useBackendURL } from '../../../hooks/useBackendURL';

export default function ExpandedVehicleImageModal({
  onClose,
  show,
  onPrevious,
  onNext,
  vehicleImageUrls,
  currentIndex
}) {
  const backEndURL = useBackendURL();

  if (!show)
    return null;
  if (!vehicleImageUrls || vehicleImageUrls.length === 0)
    return null;

  return (
    <div className="image-modal-overlay" onClick={onClose}>
      <div className="image-modal-content" onClick={(e) => e.stopPropagation()}>
        <button className="image-modal-close" onClick={onClose}>✕</button>
        <button className="image-modal-prev" onClick={onPrevious}>‹</button>
        <img
          src={`${backEndURL}/${vehicleImageUrls[currentIndex]}`}
          alt="Vehicle"
          className="image-modal-image"
        />
        <button className="image-modal-next" onClick={onNext}>›</button>
      </div>
    </div>
  );
}