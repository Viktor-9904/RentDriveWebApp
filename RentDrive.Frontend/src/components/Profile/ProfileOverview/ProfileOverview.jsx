import Spinner from "../../shared/Spinner/Spinner";
import "./ProfileOverview.css"

import { useOverview } from "../hooks/useOverview";

export default function profileOverview() {
  const { overviewData, overviewLoading, overviewError } = useOverview();

  if (overviewLoading) return <Spinner message={"User Details"}/>
  if (overviewError) return <p className="overviewError">Error: {overviewError}</p>;
  if (!overviewData) return null;

  return (
    <div className="profile-container">
      <h3 className="profile-heading">User Information</h3>

      <div className="profile-info">
        <p><strong>Username:</strong> {overviewData.username}</p>
        <p><strong>Email:</strong> {overviewData.email}</p>
        <p><strong>Phone:</strong> {overviewData.phoneNumber || "N/A"}</p>
        <p><strong>Member Since:</strong> {new Date(overviewData.memberSince).toLocaleDateString('en-GB', { day: 'numeric', month: 'long', year: 'numeric' })}</p>
      </div>

      <h3 className="profile-heading">Profile Summary</h3>

      <div className="profile-overview-summary">
        <div className="profile-overview-summary-card profile-overview-summary-purple">
          <p className="profile-overview-summary-number">{overviewData.completedRentalsCount}</p>
          <p className="profile-overview-summary-label">Completed Rentals</p>
        </div>

        <div className="profile-overview-summary-card profile-overview-summary-amber">
          <p className="profile-overview-summary-number">{overviewData.userRating === 0 ? 0 : overviewData.userRating.toFixed(1)}</p>
          <p className="profile-overview-summary-label">User Rating</p>
        </div>

        <div className="profile-overview-summary-card profile-overview-summary-green">
          <p className="profile-overview-summary-number">{overviewData.vehiclesListedCount}</p>
          <p className="profile-overview-summary-label">Vehicles Listed</p>
        </div>
      </div>
    </div>
  );
}
