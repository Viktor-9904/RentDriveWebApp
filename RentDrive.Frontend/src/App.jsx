import { Routes, Route } from 'react-router'

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

import { AccountProvider } from './context/AccountContext'

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
        <Route path="/api/vehicle/:id" element={<VehicleDetailsPage/>}/>
        <Route path="/manage-vehicle-type-properties" element={<VehicleTypeProperties/>}/>
        <Route path="/manage-vehicles/create" element={<CreateVehicle/>}/>
      </Routes>

      <Footer />
    </AccountProvider>
  )
}

export default App