import { Link } from 'react-router-dom'

export default function Login() {
    return (
        <>
            <div className="page-heading">
                <div className="container">
                    <div className="col-lg-5 mx-auto">
                        <div className="section-heading text-center mb-5">
                            <h2>Login to Your Account</h2>
                            <p>Welcome back! Please login to your account.</p>
                        </div>
                        <form action="/login" method="POST">
                            <div className="form-group mb-3">
                                <input type="email" name="email" className="form-control" placeholder="Email Address" required />
                            </div>
                            <div className="form-group mb-4">
                                <input type="password" name="password" className="form-control" placeholder="Password" required />
                            </div>
                            <button type="submit" className="btn btn-primary btn-block main-button">Login</button>
                        </form>
                        <p className="text-center mt-3">
                            Don't have an account? <Link to="/register">Register here</Link>
                        </p>

                    </div>
                </div>
            </div>
        </>
    )
}