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
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <button className="modal-close" onClick={onClose}>✕</button>
        <button className="modal-prev" onClick={onPrevious}>‹</button>
        <img
          src={`${backEndURL}/${vehicleImageUrls[currentIndex]}`}
          alt="Vehicle"
          className="modal-image"
        />
        <button className="modal-next" onClick={onNext}>›</button>
      </div>
    </div>
  );
}