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
import useAuth from './hooks/useAuth'
import { AccountContext } from './context/AccountContext'

function App() {
  const { user, isAuthenticated } = useAuth();

  // console.log(`isAuthenticated - `, isAuthenticated)
  // console.log(`user - `, user)

  return (
    <AccountContext.Provider value={{user, isAuthenticated}}>
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
    </AccountContext.Provider>
  )
}

export default App