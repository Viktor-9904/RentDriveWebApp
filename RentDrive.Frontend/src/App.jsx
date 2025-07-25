import { Routes, Route, Outlet } from 'react-router'

import './App.css'

import Home from './pages/Home'
import Categories from './pages/Categories'

import Spinner from './components/shared/Spinner'
import Header from './components/shared/Header'
import Footer from './components/shared/Footer'

import Listing from './pages/Listing'
import ContactUs from './pages/ContactUs'
import Register from './pages/Register/Register'
import Login from './pages/Login/Login'
import VehicleDetailsPage from './pages/VehicleDetailsPage'
import VehicleTypeProperties from './pages/VehicleTypeProperties'
import CreateVehicle from './pages/CreateVehicle'
import EditVehicle from './pages/EditVehicle'
import ManageVehicleTypes from './pages/ManageVehicleTypes'
import ManageVehicleTypeCategories from './pages/ManageVehicleTypeCategories'

import { AccountProvider } from './context/AccountContext'

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

function App() {
  return (
    <AccountProvider>
      {/* <Spinner/> */}

      <Header />

      <Routes>
        <Route index element={<Home />} />
        <Route path="/categories" element={<Categories />} />
        <Route path="/listing" element={<Listing />} />
        <Route path="/contact-us" element={<ContactUs />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/api/vehicle/:id" element={<VehicleDetailsPage />} />

        <Route path="/manage" element={<ManageLayout />}>
        
          <Route path="vehicle-type-properties" element={<VehicleTypeProperties />} />
          <Route path="vehicle-types" element={<ManageVehicleTypes />} />
          <Route path="vehicle-type-categories" element={<ManageVehicleTypeCategories />} />

          <Route path="vehicles" element={<VehiclesLayout />}>
            <Route path="create" element={<CreateVehicle />} />
            <Route path="edit/:id" element={<EditVehicle />} />
          </Route>
          
        </Route>

      </Routes>

      <Footer />
    </AccountProvider>
  )
}


export default App