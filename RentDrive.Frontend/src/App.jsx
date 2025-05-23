import { Routes, Route } from 'react-router'

import './App.css'

import Home from './pages/Home'

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
      </Routes>

      <Footer />
    </>
  )
}

export default App
