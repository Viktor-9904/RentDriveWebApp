import { Link } from 'react-router';
import useLogout from '../../hooks/useLogout';
import { useAuth } from '../../context/AccountContext';
import { useEffect, useState } from 'react';
import { GiConsoleController } from 'react-icons/gi';

export default function Header() {
    const { user, isAuthenticated } = useAuth();
    const [showBalance, setShowBalance] = useState(false);

    useEffect(() => {
        if(isAuthenticated){
            setShowBalance(true)
            console.log(user)
        }
    }, [user, isAuthenticated])

    const navigation = [
        { name: 'Home', href: '/' },
        { name: 'Categories', href: '/categories' },
        { name: 'Listing', href: '/listing' },
        ...(user?.isCompanyEmployee
            ? [
                {
                    name: 'Manage',
                    dropdown: true,
                    children: [
                        { name: 'Vehicle Types', href: '/manage/vehicle-types' },
                        { name: 'Vehicle Type Properties', href: '/manage/vehicle-type-properties' },
                        { name: 'Vehicle Categories', href: '/manage/vehicle-type-categories' },
                    ],
                },
            ]
            : []),
        ...(!isAuthenticated
            ? [
                { name: 'Register', href: '/register' },
                { name: 'Login', href: '/login' }
            ]
            : []),
        ...(isAuthenticated
            ? [
                { name: 'My Profile', href: '/profile' },
            ]
            : []),
        ,
    ];

    return (
        <>
            <header className="header-area header-sticky wow slideInDown" data-wow-duration="0.75s" data-wow-delay="0s">
                <div className="container">
                    <div className="row">
                        <div className="col-12">
                            <nav className="main-nav">
                                <Link to="/" className="logo" />

                                {/*TODO: ADD - className="active" */}
                                <ul className="nav">
                                    {navigation.map((item) => (
                                        item.dropdown ? (
                                            <li className="has-sub" key={item.name}>
                                                <Link to="#">{item.name}</Link>
                                                <ul className="sub-menu">
                                                    {item.children.map((child) => (
                                                        <li key={child.name}>
                                                            <Link to={child.href}>{child.name}</Link>
                                                        </li>
                                                    ))}
                                                </ul>
                                            </li>
                                        ) : (
                                            <li key={item.name}>
                                                <Link to={item.href}>{item.name}</Link>
                                            </li>
                                        )
                                    ))}
                                     {showBalance && <li className="balance-display">
                                        <span>Balance:</span>
                                        <span className="amount">{user?.balance?.toFixed(2) ?? '0.00'}€</span>
                                        {user?.pendingBalance > 0 && <span className='pending-balance'>(Pending: {user.pendingBalance.toFixed(2)}€)</span>}
                                    </li>}
                                </ul>
                            </nav>
                        </div>
                    </div>
                </div>
            </header>
        </>
    );
}