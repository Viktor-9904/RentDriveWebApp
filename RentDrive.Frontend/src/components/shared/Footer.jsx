import { Link } from 'react-router-dom'

export default function Footer() {
    return (
        <>
            <footer>
                <div className="container">
                    <div className="row">
                        <div className="col-lg-4">
                            <div className="about">
                                <div className="logo">
                                    <img src="assets/images/black-logo.png" alt="Plot Listing" />
                                </div>
                                <p>Copyright © 2021 Plot Listing Co., Ltd. All Rights Reserved.</p>
                            </div>
                        </div>
                        <div className="col-lg-4">
                            <div className="helpful-links">
                                <h4>Helpful Links</h4>
                                <div className="row">
                                    <div className="col-lg-6 col-sm-6">
                                        <ul>
                                            <li><Link to="/categories">Categories</Link></li>
                                            <li><Link to="#">Reviews</Link></li>
                                            <li><Link to="/listing">Listing</Link></li>
                                            <li><Link to="/contact-us">Contact Us</Link></li>
                                        </ul>
                                    </div>
                                    <div className="col-lg-6">
                                        <ul>
                                            <li><Link to="#">About Us</Link></li>
                                            <li><Link to="#">Awards</Link></li>
                                            <li><Link to="#">Useful Sites</Link></li>
                                            <li><Link to="#">Privacy Policy</Link></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="col-lg-4">
                            <div className="contact-us">
                                <h4>Contact Us</h4>
                                <p>27th Street of New Town, Digital Villa</p>
                                <div className="row">
                                    <div className="col-lg-6">
                                        <Link to="#">010-020-0340</Link>
                                    </div>
                                    <div className="col-lg-6">
                                        <Link to="#">090-080-0760</Link>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
        </>
    )
}