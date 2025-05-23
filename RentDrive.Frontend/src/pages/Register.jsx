import { Link } from 'react-router-dom';

export default function Register() {
    return (
        <>
            <div className="page-heading">
                <div className="container">
                    <div className="col-lg-5 mx-auto">
                        <div className="section-heading text-center mb-5">
                            <h2>Create an Account</h2>
                            <p>Join us today! Fill in the details to register.</p>
                        </div>
                        <form action="/register" method="POST">
                            <div className="form-group mb-3">
                                <input
                                    type="text"
                                    name="username"
                                    className="form-control"
                                    placeholder="Username"
                                    required
                                />
                            </div>
                            <div className="form-group mb-3">
                                <input
                                    type="email"
                                    name="email"
                                    className="form-control"
                                    placeholder="Email Address"
                                    required
                                />
                            </div>
                            <div className="form-group mb-3">
                                <input
                                    type="password"
                                    name="password"
                                    className="form-control"
                                    placeholder="Password"
                                    required
                                />
                            </div>
                            <div className="form-group mb-4">
                                <input
                                    type="password"
                                    name="confirmPassword"
                                    className="form-control"
                                    placeholder="Confirm Password"
                                    required
                                />
                            </div>
                            <button type="submit" className="btn btn-primary btn-block main-button">
                                Register
                            </button>
                        </form>
                        <p className="text-center mt-3">
                            Already have an account? <Link to="/login">Login here</Link>
                        </p>
                    </div>
                </div>
            </div>
        </>
    );
}
