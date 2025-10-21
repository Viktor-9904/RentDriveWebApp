import '../AuthPage.css';

import { Link, useNavigate } from 'react-router-dom';
import { useState } from 'react';

import { useLoginPost } from './useLoginPost'
import { useLoginValidation } from './useLoginValidation'
import { useAuth } from '../../../context/AccountContext';


export default function Login() {

    const { loginUser, loading, error } = useLoginPost()
    const navigate = useNavigate()
    const { user, isAuthenticated, loadUser } = useAuth();

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

    if (isAuthenticated) {
        navigate('/')
    }

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
            await loadUser();
            navigate('/')
        } else {
            console.log("Failed to login!")
        }
    };


    return (
        <div className="auth-page-container">
            <div className="auth-form-wrapper">
                <div className="auth-form-heading">
                    <h2>Login to Your Account</h2>
                    <p>Welcome back! Please login to your account.</p>
                </div>
                <form onSubmit={handleSubmit}>
                    <div className="auth-form-group">
                        <input
                            type="text"
                            name="emailOrUsername"
                            className="form-control"
                            placeholder="Email or Username"
                            onChange={(e) => setEmailOrUsername(e.target.value)}
                            onBlur={() => setEmailOrUsernameTouched(true)}
                        />
                        {errors.emailOrUsername && <small className="auth-error-text">{errors.emailOrUsername}</small>}
                    </div>
                    <div className="auth-form-group">
                        <input
                            type="password"
                            name="password"
                            className="form-control"
                            placeholder="Password"
                            onChange={(e) => setPassword(e.target.value)}
                            onBlur={() => setPasswordTouched(true)}
                        />
                        {errors.password && <small className="auth-error-text">{errors.password}</small>}
                    </div>
                    <button type="submit" className="auth-main-button">Login</button>
                </form>
                <p className="auth-footer-text">
                    Don't have an account? <Link to="/register">Register here</Link>
                </p>
            </div>
        </div>
    )
}