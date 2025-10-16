import { Routes, Route, Outlet, Navigate } from 'react-router'

import './App.css'

import Home from './pages/Home'

import Spinner from './components/shared/Spinner/Spinner'
import Header from './components/shared/Header'
import Footer from './components/shared/Footer'

import Listing from './pages/Listing'
import ContactUs from './pages/ContactUs'
import Register from './pages/Auth/Register/Register'
import Login from './pages/Auth/Login/Login'
import VehicleDetailsPage from './pages/VehicleDetailsPage'
import VehicleTypeProperties from './pages/VehicleTypeProperties'
import CreateVehiclePage from './pages/CreateVehiclePage'
import EditVehiclePage from './pages/EditVehiclePage'
import ManageVehicleTypes from './pages/ManageVehicleTypes'
import ManageVehicleTypeCategories from './pages/ManageVehicleTypeCategories'

import ProfileLayoutPage from './pages/Profile/ProfileLayoutPage'
import ProfileOverviewPage from './pages/Profile/ProfileOverviewPage'
import MyRentals from './components/Profile/UserRentals/MyRentals'
import MyListedVehiclesPage from './pages/Profile/MyListedVehiclesPage'

import { AccountProvider, useAuth } from './context/AccountContext'
import ProfileSettingsPage from './pages/Profile/ProfileSettingsPage'
import UserWallet from './components/Profile/UserWallet/UserWallet'
import ChatPage from './pages/ChatPage/ChatPage'
import ScrollToTop from './components/shared/ScrollToTop'

function App() {
  return (
    <AccountProvider>
      <Header />
      <ScrollToTop />

      <Routes>

        <Route index element={<Home />} />
        <Route path="/listing" element={<Listing />} />
        <Route path="/contact-us" element={<ContactUs />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/api/vehicle/:id" element={<VehicleDetailsPage />} />

        <Route element={<EmployeeRoute />}>
          <Route path="/manage" element={<ManageLayout />}>
            <Route path="vehicle-type-properties" element={<VehicleTypeProperties />} />
            <Route path="vehicle-types" element={<ManageVehicleTypes />} />
            <Route path="vehicle-type-categories" element={<ManageVehicleTypeCategories />} />
          </Route>

        </Route>

        <Route element={<AuthenticatedRoute />}>
          <Route path="/manage" element={<ManageLayout />}>
            <Route path="vehicles" element={<VehiclesLayout />}>
              <Route path="create" element={<CreateVehiclePage />} />
              <Route path="edit/:id" element={<EditVehiclePage />} />
            </Route>
          </Route>

          <Route path="/profile" element={<ProfileLayoutPage />}>
            <Route index element={<ProfileOverviewPage />} />
            <Route path="rentals" element={<MyRentals />} />
            <Route path="vehicles" element={<MyListedVehiclesPage />} />
            <Route path="settings" element={<ProfileSettingsPage />} />
            <Route path="wallet" element={<UserWallet />} />
          </Route>

            <Route path="chat" element={<ChatPage />} />
        </Route>

      </Routes>

      <Footer />
    </AccountProvider>
  )
}

function ManageLayout() {
  return (
    <div>
      <Outlet />
    </div>
  )
}

function VehiclesLayout() {
  return (
    <div>
      <Outlet />
    </div>
  )
}

function AuthenticatedRoute() {
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) {
    return <Navigate to="/" replace />;
  }

  return <Outlet />;
}

function EmployeeRoute() {
  const { isAuthenticated, isCompanyEmployee } = useAuth();

  if (!isAuthenticated || !isCompanyEmployee) {
    return <Navigate to="/" replace />;
  }

  return <Outlet />;
}

export default App