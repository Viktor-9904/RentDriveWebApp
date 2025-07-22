export default function ProfileOverview() {
  return (
    <div className="profile-container">
      <h3 className="profile-heading">User Information</h3>

      <div className="profile-info">
        <p><strong>Full Name:</strong> Jane Doe</p>
        <p><strong>Email:</strong> jane.doe@example.com</p>
        <p><strong>Phone:</strong> +1 (555) 123-4567</p>
        <p><strong>Member Since:</strong> January 2023</p>
      </div>

      <h3 className="profile-heading">Profile Summary</h3>

      <div className="profile-summary">
        <div className="summary-card summary-green">
          <p className="summary-number">3</p>
          <p className="summary-label">Vehicles Listed</p>
        </div>

        <div className="summary-card summary-purple">
          <p className="summary-number">6</p>
          <p className="summary-label">Completed Rentals</p>
        </div>

        <div className="summary-card summary-amber">
          <p className="summary-number">8.7</p>
          <p className="summary-label">User Rating</p>
        </div>
      </div>
    </div>
  );
}
