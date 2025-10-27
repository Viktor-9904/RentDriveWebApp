import { useState, useEffect } from "react";
import { useUserProfileDetails } from "./hooks/useUserProfileDetails";
import { useBackendURL } from "../../hooks/useBackendURL";
import { useErrorModal } from "../../context/ErrorModalContext"

import Spinner from "../shared/Spinner/Spinner";

export default function ProfileSettings() {
  const backEndURL = useBackendURL();
  const { userProfileDetails, userProfileDetailsLoading, userProfileDetailsError } = useUserProfileDetails();
  const { setErrorModalMessage } = useErrorModal()

  const [profileData, setProfileData] = useState({
    username: "",
    email: "",
    phoneNumber: "",
  });
  const [savingProfile, setSavingProfile] = useState(false);

  const [passwordData, setPasswordData] = useState({
    currentPassword: "",
    newPassword: "",
    confirmPassword: "",
  });
  const [changingPassword, setChangingPassword] = useState(false);

  useEffect(() => {
    if (userProfileDetails) {
      setProfileData({
        username: userProfileDetails.username,
        email: userProfileDetails.email,
        phoneNumber: userProfileDetails.phoneNumber || "",
      });
    }
  }, [userProfileDetails]);

  const handleProfileChange = (e) => {
    const { name, value } = e.target;
    setProfileData((prev) => ({ ...prev, [name]: value }));
  };

  const handlePasswordChange = (e) => {
    const { name, value } = e.target;
    setPasswordData((prev) => ({ ...prev, [name]: value }));
  };

  const handleProfileSubmit = async (e) => {
    e.preventDefault();
    setSavingProfile(true);

    try {
      const response = await fetch(`${backEndURL}/api/account/update-profile-details`, {
        method: "PUT",
        credentials: "include",
        headers:
        {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(profileData),
      });

      if (!response.ok) {
        const errorMessage = await response.text();
        setErrorModalMessage(errorMessage);
        throw new Error(errorText || "Failed to update profile");
      }
    } catch (error) {
      // alert("Error updating profile: " + error.message);
    } finally {
      setSavingProfile(false);
    }
  };

  const handlePasswordSubmit = async (e) => {
    e.preventDefault();

    if (passwordData.newPassword !== passwordData.confirmPassword) {
      alert("New passwords do not match.");
      return;
    }

    setChangingPassword(true);

    try {
      const response = await fetch(`${backEndURL}/api/account/update-password`, {
        method: "POST",
        credentials: "include",
        headers:
        {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          currentPassword: passwordData.currentPassword,
          newPassword: passwordData.newPassword,
          confirmPassword: passwordData.confirmPassword
        }),
      });

      if (!response.ok) {
        const errorMessage = await response.text();
        setErrorModalMessage(errorMessage);
        throw new Error(errorText || "Failed to change password");
      }

      setPasswordData({ currentPassword: "", newPassword: "", confirmPassword: "" });
    } catch (erorr) {
      // alert("Error changing password: " + erorr.message);
    } finally {
      setChangingPassword(false);
    }
  };

  if (userProfileDetailsLoading) return <Spinner message={"Settings"}/>;
  if (userProfileDetailsError) return <p>Error loading profile: {userProfileDetailsError.message}</p>;

  return (
    <div className="settings-container">
      <h3 className="settings-heading">Account Settings</h3>

      <form className="settings-form" onSubmit={handleProfileSubmit}>
        <div className="form-section">
          <label>Full Name</label>
          <input
            type="text"
            name="username"
            value={profileData.username}
            onChange={handleProfileChange}
            pattern="^.{3,20}$"
            title="Username must be between 3 and 20 characters long."
            required
          />
        </div>

        <div className="form-section">
          <label>Email</label>
          <input
            type="email"
            name="email"
            value={profileData.email}
            onChange={handleProfileChange}
            required
          />
        </div>

        <div className="form-section">
          <label>Phone</label>
          <input
            type="tel"
            name="phoneNumber"
            value={profileData.phoneNumber}
            onChange={handleProfileChange}
            pattern="\+?[0-9]{8,15}"
            title="Phone number must be 8–15 digits, optionally starting with +"
          />
        </div>

        <button type="submit" className="save-btn" disabled={savingProfile}>
          {savingProfile ? "Saving..." : "Save Changes"}
        </button>
      </form>

      <hr className="divider" />

      <h4 className="settings-subheading">Change Password</h4>

      <form className="settings-form" onSubmit={handlePasswordSubmit}>
        <div className="form-section">
          <label>Current Password</label>
          <input
            type="password"
            name="currentPassword"
            value={passwordData.currentPassword}
            onChange={handlePasswordChange}
            pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$"
            title="Password must be 6-20 characters, with at least one uppercase letter, one lowercase letter, and one digit."
            required
          />
        </div>

        <div className="form-section">
          <label>New Password</label>
          <input
            type="password"
            name="newPassword"
            value={passwordData.newPassword}
            onChange={handlePasswordChange}
            required
            pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$"
            title="Password must be 6-20 characters, with at least one uppercase letter, one lowercase letter, and one digit."
          />
        </div>

        <div className="form-section">
          <label>Confirm New Password</label>
          <input
            type="password"
            name="confirmPassword"
            value={passwordData.confirmPassword}
            onChange={handlePasswordChange}
            required
            pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$"
            title="Password must be 6-20 characters, with at least one uppercase letter, one lowercase letter, and one digit."
          />
        </div>

        <button type="submit" className="save-btn" disabled={changingPassword}>
          {changingPassword ? "Updating..." : "Update Password"}
        </button>
      </form>
    </div>
  );
}
