import { Link, useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { useLoginPost } from './useLoginPost'
import { useLoginValidation } from './useLoginValidation'

export default function Login() {

    const { loginUser, loading, error } = useLoginPost()
    const navigate = useNavigate()

    const [emailOrUsername, setEmailOrUsername] = useState("");
    const [password, setPassword] = useState("");

    const [emailOrUsernameTouched, setEmailOrUsernameTouched] = useState(false);
    const [passwordTouched, setPasswordTouched] = useState(false);

    const { errors } = useLoginValidation({
        emailOrUsername,
        password,
        emailOrUsernameTouched,
        passwordTouched,
    })

    const handleSubmit = async (e) => {
        e.preventDefault();

        setEmailOrUsernameTouched(true)
        setPasswordTouched(true)

        const hasErrors = Object.values(errors).some(error => error !== "")
        if (hasErrors || !emailOrUsername || !password) {
            return;
        }

        const payload = {
            EmailOrUsername: emailOrUsername,
            Password: password,
        }
        const wasUserSuccessfullyLogged = await loginUser(payload)

        if (wasUserSuccessfullyLogged) {
            console.log("Logged In successfully!")
            navigate('/')            
        } else {
            console.log("Failed to register: " + JSON.stringify(error))
        }
    };

    return (
        <>
            <div className="page-heading">
                <div className="container">
                    <div className="col-lg-5 mx-auto">
                        <div className="section-heading text-center mb-5">
                            <h2>Login to Your Account</h2>
                            <p>Welcome back! Please login to your account.</p>
                        </div>
                        <form onSubmit={handleSubmit}>
                            <div className="form-group mb-3">
                                <input
                                    type="text"
                                    name="emailOrUsername"
                                    className="form-control"
                                    placeholder="Email or Username"
                                    onChange={(e) => setEmailOrUsername(e.target.value)}
                                    onBlur={() => setEmailOrUsernameTouched(true)} />
                                {errors.emailOrUsername && <small className="error-text">{errors.emailOrUsername}</small>}
                            </div>
                            <div className="form-group mb-3">
                                <input
                                    type="password"
                                    name="password"
                                    className="form-control"
                                    placeholder="Password"
                                    onChange={(e) => setPassword(e.target.value)}
                                    onBlur={() => setPasswordTouched(true)} />
                                {errors.password && <small className="error-text">{errors.password}</small>}
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