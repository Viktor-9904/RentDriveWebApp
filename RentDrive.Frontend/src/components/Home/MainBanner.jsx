import { Link } from 'react-router-dom'

export default function MainBanner() {
    return (
        <>
            <div className="main-banner">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="top-text header-text">
                                <h6>Over 36,500+ Active Listings</h6>
                                <h2>Find Nearby Places &amp; Things</h2>
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <form id="search-form" name="gs" method="submit" role="search" action="#">
                                <div className="row">
                                    <div className="col-lg-3 align-self-center">
                                        <fieldset>
                                            <select name="area" className="form-select" aria-label="Area" id="chooseCategory" defaultValue="All Areas">
                                                <option value="New Village">New Village</option>
                                                <option value="Old Town">Old Town</option>
                                                <option value="Modern City">Modern City</option>
                                            </select>
                                        </fieldset>
                                    </div>
                                    <div className="col-lg-3 align-self-center">
                                        <fieldset>
                                            <input type="address" name="address" className="searchText" placeholder="Enter a location" required />
                                        </fieldset>
                                    </div>
                                    <div className="col-lg-3 align-self-center">
                                        <fieldset>
                                            <select name="price" className="form-select" aria-label="Default select example" id="chooseCategory" defaultValue="Price Range">
                                                <option value="$100 - $250">$100 - $250</option>
                                                <option value="$250 - $500">$250 - $500</option>
                                                <option value="$500 - $1000">$500 - $1,000</option>
                                                <option value="$1000+">$1,000 or more</option>
                                            </select>
                                        </fieldset>
                                    </div>
                                    <div className="col-lg-3">
                                        <fieldset>
                                            <button className="main-button"><i className="fa fa-search"></i> Search Now</button>
                                        </fieldset>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div className="col-lg-10 offset-lg-1">
                            <ul className="categories">
                                <li><Link to="/categories"><span className="icon"><img src="assets/images/search-icon-01.png" alt="Home" /></span> Apartments</Link></li>
                                <li><Link to="/listing"><span className="icon"><img src="assets/images/search-icon-02.png" alt="Food" /></span> Food &amp; Life</Link></li>
                                <li><Link to="#"><span className="icon"><img src="assets/images/search-icon-03.png" alt="Vehicle" /></span> Cars</Link></li>
                                <li><Link to="#"><span className="icon"><img src="assets/images/search-icon-04.png" alt="Shopping" /></span> Shopping</Link></li>
                                <li><Link to="#"><span className="icon"><img src="assets/images/search-icon-05.png" alt="Travel" /></span> Traveling</Link></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}