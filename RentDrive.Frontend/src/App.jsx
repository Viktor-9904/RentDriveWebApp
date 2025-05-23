import './App.css'
import Home from './pages/Home'
import Spinner from './components/shared/Spinner'
import Header from './components/shared/Header'
import Footer from './components/shared/Footer'

function App() {
  return (
    <>
        <Spinner/>
        
        <Header/>

        <Home/>

        <Footer/>
    </>
  )
}

export default App
