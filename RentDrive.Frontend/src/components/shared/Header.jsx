import { Link } from 'react-router'
import useLogout from '../../hooks/useLogout'

const navigation = [
    { name: 'Home', href: '/' },
    { name: 'categories', href: '/categories' },
    { name: 'listing', href: '/listing' },
    { name: 'contact-us', href: '/contact-us' },
    { name: 'Register', href: '/register' },
    { name: 'Login', href: '/login' },
]

export default function Header() {

    const handleLogout = useLogout()

    return (
        <>
            {/* <!-- ***** Header Area Start ***** --> */}
            <header className="header-area header-sticky wow slideInDown" data-wow-duration="0.75s" data-wow-delay="0s">
                <div className="container">
                    <div className="row">
                        <div className="col-12">
                            <nav className="main-nav">
                                <Link to="/" className="logo"></Link>

                                {/*TODO: ADD - className="active" */}
                                <ul className="nav">
                                    {navigation.map((item) => (
                                        <li key={item.name}>
                                            <Link
                                                to={item.href}>
                                                {item.name}
                                            </Link>
                                        </li>
                                    ))}
                                    <li>
                                        <button
                                            className="logout-button"
                                            onClick={handleLogout}
                                            onMouseDown={e => e.currentTarget.blur()}
                                        >
                                            Logout
                                        </button>
                                    </li>
                                </ul>
                            </nav>
                        </div>
                    </div>
                </div>
            </header>
            {/* <!-- ***** Header Area End ***** --> */}
        </>
    )
}