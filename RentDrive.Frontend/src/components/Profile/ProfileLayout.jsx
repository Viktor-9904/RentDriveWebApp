import { Outlet, NavLink } from "react-router-dom";

export default function ProfileLayout() {
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
        </nav>
      </aside>

      <main className="profile-content">
        <Outlet />
      </main>
    </div>
  );
}
