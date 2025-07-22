export default function ProfileSettings() {
  return (
    <div className="settings-container">
      <h3 className="settings-heading">Account Settings</h3>

      <form className="settings-form">
        <div className="form-section">
          <label>Full Name</label>
          <input type="text" defaultValue="Jane Doe" />
        </div>

        <div className="form-section">
          <label>Email</label>
          <input type="email" defaultValue="jane.doe@example.com" />
        </div>

        <div className="form-section">
          <label>Phone</label>
          <input type="tel" defaultValue="+1 (555) 123-4567" />
        </div>

        <button type="submit" className="save-btn">Save Changes</button>
      </form>

      <hr className="divider" />

      <h4 className="settings-subheading">Change Password</h4>
      <form className="settings-form">
        <div className="form-section">
          <label>Current Password</label>
          <input type="password" />
        </div>

        <div className="form-section">
          <label>New Password</label>
          <input type="password" />
        </div>

        <div className="form-section">
          <label>Confirm New Password</label>
          <input type="password" />
        </div>

        <button type="submit" className="save-btn">Update Password</button>
      </form>
      
      <div className="danger-zone">
        <button className="delete-btn">Delete Account</button>
      </div>
    </div>
  );
}
