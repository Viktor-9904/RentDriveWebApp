export default function ContactPage() {
    return (
        <>
            <div className="contact-page">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="inner-content">
                                <div className="row">
                                    <div className="col-lg-6">
                                        <div id="map">
                                            <iframe
                                                src="https://www.google.com/maps/embed?pb=!1m14!1m12!1m3!1d508.6463914962445!2d25.952971693615446!3d43.848812171121075!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!5e0!3m2!1sen!2sbg!4v1747997509985!5m2!1sen!2sbg"
                                                width="100%"
                                                height="650px"
                                                frameBorder={0}
                                                style={{ border: 0 }}
                                                allowFullScreen=""
                                            />
                                        </div>
                                    </div>
                                    <div className="col-lg-6 align-self-center">
                                        <form id="contact" action="" method="get">
                                            <div className="row">
                                                <div className="col-lg-6">
                                                    <fieldset>
                                                        <input type="name" name="name" id="name" placeholder="Name" autoComplete="on" required />
                                                    </fieldset>
                                                </div>
                                                <div className="col-lg-6">
                                                    <fieldset>
                                                        <input type="surname" name="surname" id="surname" placeholder="Surname" autoComplete="on" required />
                                                    </fieldset>
                                                </div>
                                                <div className="col-lg-12">
                                                    <fieldset>
                                                        <input type="text" name="email" id="email" pattern="[^ @]*@[^ @]*" placeholder="Your Email" required="" />
                                                    </fieldset>
                                                </div>
                                                <div className="col-lg-12">
                                                    <ul>
                                                        <li><input type="checkbox" name="option1" value="cars" /><span>Cars</span></li>
                                                        <li><input type="checkbox" name="option2" value="aparmtents" /><span>Apartments</span></li>
                                                        <li><input type="checkbox" name="option3" value="shopping" /><span>Shopping</span></li>
                                                        <li><input type="checkbox" name="option4" value="food" /><span>Food &amp; Life</span></li>
                                                        <li><input type="checkbox" name="option5" value="traveling" /><span>Traveling</span></li>
                                                    </ul>
                                                </div>
                                                <div className="col-lg-12">
                                                    <fieldset>
                                                        <textarea name="message" type="text" className="form-control" id="message" placeholder="Message" required=""></textarea>
                                                    </fieldset>
                                                </div>
                                                <div className="col-lg-12">
                                                    <fieldset>
                                                        <button type="submit" id="form-submit" className="main-button "><i className="fa fa-paper-plane"></i> Send Message</button>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}