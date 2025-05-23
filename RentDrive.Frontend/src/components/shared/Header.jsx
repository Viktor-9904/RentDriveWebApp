import { Link } from 'react-router'

const navigation = [
    { name: 'Home', href: '/' },
    { name: 'categories', href: '/categories' },
    { name: 'listing', href: '/listing' },
    { name: 'contact-us', href: '/contact-us' },
]

export default function Header() {
    return (
        <>
            {/* <!-- ***** Header Area Start ***** --> */}
            <header className="header-area header-sticky wow slideInDown" data-wow-duration="0.75s" data-wow-delay="0s">
                <div className="container">
                    <div className="row">
                        <div className="col-12">
                            <nav className="main-nav">
                                <Link to="/" className="logo"></Link>

                                {/* <!-- ***** Menu Start ***** --> */}

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
                                </ul>
                                <Link className='menu-trigger'>
                                    <span>Menu</span>
                                </Link>
                                {/* <!-- ***** Menu End ***** --> */}
                            </nav>
                        </div>
                    </div>
                </div>
            </header>
            {/* <!-- ***** Header Area End ***** --> */}
        </>
    )
}