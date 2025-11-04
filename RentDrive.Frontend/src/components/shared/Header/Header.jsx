import "./Header.css"

import { Link } from 'react-router';
import { Menu, X } from "lucide-react";
import { useEffect, useState } from 'react';

import { useAuth } from '../../../context/AccountContext';

export default function Header() {
    const { user, isAuthenticated } = useAuth();
    const [showBalance, setShowBalance] = useState(false);

    useEffect(() => {
        if (isAuthenticated) {
            setShowBalance(true)
        }
        else {
            setShowBalance(false);
        }
    }, [user, isAuthenticated])

    const navigation = [
        // { name: 'Home', href: '/' },
        { name: 'Listing', href: '/listing' },
        ...(isAuthenticated
            ? [
                { name: 'Chat', href: '/chat' },
            ]
            : []),
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

    const [menuOpen, setMenuOpen] = useState(false);
    const [openSubmenu, setOpenSubmenu] = useState(null);
    const toggleMenu = () => setMenuOpen(!menuOpen);

    return (
        <header className="header-area header-sticky wow slideInDown" data-wow-duration="0.75s" data-wow-delay="0s">
            <div className="container">
                <div className="row">
                    <div className="col-12">
                        <nav className="main-nav">
                            <Link 
                                to="/" 
                                className="logo" 
                                onClick={() => {
                                    setOpenSubmenu(null);
                                    setMenuOpen(false);
                                    window.scrollTo(0, 0);
                            }} />
                            <div className="menu-trigger" onClick={toggleMenu}>
                                {menuOpen ? (
                                    <X size={28} className="menu-icon" />
                                ) : (
                                    <Menu size={28} className="menu-icon" />
                                )}
                            </div>
                            <ul className={`nav ${menuOpen ? "active" : ""}`}>
                                {navigation.map((item, index) => (
                                    item.dropdown ? (
                                        <li
                                            className={`has-sub ${openSubmenu === index ? "open" : ""}`}
                                            key={item.name}
                                        >
                                            <Link
                                                to="#"
                                                onClick={(e) => {
                                                    e.preventDefault();
                                                    setOpenSubmenu(openSubmenu === index ? null : index);
                                                }}
                                            >
                                                {item.name}
                                            </Link>
                                            <ul className="sub-menu">
                                                {item.children.map((subItem) => (
                                                    <li key={subItem.name}>
                                                        <Link
                                                            to={subItem.href}
                                                            onClick={() => {
                                                                setOpenSubmenu(null);
                                                                setMenuOpen(false);
                                                                window.scrollTo(0, 0);
                                                            }}
                                                        >
                                                            {subItem.name}
                                                        </Link>
                                                    </li>
                                                ))}
                                            </ul>
                                        </li>
                                    ) : (
                                        <li key={item.name}>
                                            <Link
                                                to={item.href}
                                                onClick={() => {
                                                    setOpenSubmenu(null);
                                                    setMenuOpen(false);
                                                    window.scrollTo(0, 0);
                                                }}
                                            >
                                                {item.name}
                                            </Link>
                                        </li>
                                    )
                                ))}
                                {showBalance && (
                                    <li className="balance-display">
                                        <span>Balance:</span>
                                        <span className="amount">
                                            {user?.balance?.toFixed(2) ?? "0.00"}€
                                        </span>
                                        {user?.pendingBalance > 0 && (
                                            <span className="pending-balance">
                                                (Pending: {user.pendingBalance.toFixed(2)}€)
                                            </span>
                                        )}
                                    </li>
                                )}
                            </ul>
                        </nav>
                    </div>
                </div>
            </div>
        </header>
    );
}