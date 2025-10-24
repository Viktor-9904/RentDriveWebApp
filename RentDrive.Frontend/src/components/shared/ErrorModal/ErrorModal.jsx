import "./ErrorModal.css";
import { AlertTriangle } from "lucide-react";

export default function ErrorModal({
  show,
  onClose,
  message = "Something went wrong while processing your request. Please try again later."
}) {
  if (!show) return null;

  return (
    <div className="error-modal-overlay" onClick={onClose}>
      <div
        className="error-modal-card"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="error-modal-icon">
          <AlertTriangle size={36} />
        </div>

        <h2 className="error-modal-title">Error</h2>

        <p className="error-modal-message">{message}</p>

        <button className="error-modal-button" onClick={onClose}>
          OK
        </button>
      </div>
    </div>
  );
}
