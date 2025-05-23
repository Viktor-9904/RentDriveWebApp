import { Routes, Route } from 'react-router'

import './App.css'

import Home from './pages/Home'
import Categories from './pages/Categories'

import Spinner from './components/shared/Spinner'
import Header from './components/shared/Header'
import Footer from './components/shared/Footer'

function App() {
  return (
    <>
      {/* <Spinner/> */}

      <Header />

      <Routes>
        <Route index element={<Home />} />
        <Route path="/categories" element={<Categories />} />
      </Routes>

      <Footer />
    </>
  )
}

export default App
