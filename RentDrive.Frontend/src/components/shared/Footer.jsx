import { data } from 'jquery'
import { Link } from 'react-router-dom'

export default function Footer() {

    const appPublishYear = 2025;
    const currentYear = new Date().getFullYear();
    
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
                                <p>Copyright © {currentYear > appPublishYear ? `${appPublishYear} - ${currentYear}` : currentYear} Rent Drive Co., Ltd. All Rights Reserved.</p>
                            </div>
                        </div>
                        <div className="col-lg-4">
                            <div className="helpful-links">
                                <h4>Helpful Links</h4>
                                <div className="row">
                                    <div className="col-lg-6 col-sm-6">
                                        <ul>
                                            <li><Link to="/listing">Listing</Link></li>
                                            <li><Link to="/contact-us">Contact Us</Link></li>
                                        </ul>
                                    </div>
                                    <div className="col-lg-6">
                                        <ul>
                                            <li><Link to="#">About Us</Link></li>
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
                                        <a href="tel:0100200340">010-020-0340</a>
                                        <br />
                                        <a href="tel:0800800760">080-080-0760</a>
                                        <br />
                                        <a href="tel:09008009040">090-080-9040</a>
                                    </div>
                                    <div className="col-lg-6">
                                        <a href="support@rentdrive.eu">support@rentdrive.eu</a>
                                        <br />
                                        <a href="https://github.com/viktor-9904" target="_blank" rel="noopener noreferrer">GitHub</a>
                                        <br />
                                        <a href="https://www.linkedin.com/in/viktor-stanev-67b55a366/" target="_blank" rel="noopener noreferrer">LinkedIn</a>
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