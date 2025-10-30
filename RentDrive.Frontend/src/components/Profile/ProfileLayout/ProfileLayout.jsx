import "./ProfileLayout.css"

import { Outlet, NavLink } from "react-router-dom";
import useLogout from "../../../hooks/useLogout";

export default function ProfileLayout() {

  const handleLogout = useLogout();

  return (
    <div className="profile-layout">
      <aside className="profile-sidebar">
        <h2>My Profile</h2>
        <nav>
          <NavLink to="/profile" end className={({ isActive }) => isActive ? "active" : ""}>
            Overview
          </NavLink>
          <NavLink to="/profile/rentals" className={({ isActive }) => isActive ? "active" : ""}>
            My Rentals
          </NavLink>
          <NavLink to="/profile/vehicles" className={({ isActive }) => isActive ? "active" : ""}>
            My Vehicles
          </NavLink>
          <NavLink to="/profile/settings" className={({ isActive }) => isActive ? "active" : ""}>
            Settings
          </NavLink>
          <NavLink to="/profile/wallet" className={({ isActive }) => isActive ? "active" : ""}>
            My Wallet
          </NavLink>
        </nav>

        <div style={{ marginTop: "auto" }}>
          <button
            className="profile-layout-logout-button"
            onClick={handleLogout}
            onMouseDown={(e) => e.currentTarget.blur()}
          >
            Logout
          </button>
        </div>
      </aside>


      <main className="profile-content">
        <Outlet />
      </main>
    </div>
  );
}
