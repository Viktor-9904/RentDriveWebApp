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
      </Routes>

      <Footer />
    </AccountProvider>
  )
}

export default App