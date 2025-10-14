import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useRegisterValidation } from './useRegisterValidation';
import { useRegisterPost } from './useRegisterPost';
import { useAuth } from '../../../context/AccountContext';

export default function Register() {

    const { registerUser, loading, error } = useRegisterPost()
    const navigate = useNavigate()
    const { user, isAuthenticated, loadUser } = useAuth();

    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    const [usernameTouched, setUsernameTouched] = useState(false);
    const [emailTouched, setEmailTouched] = useState(false);
    const [passwordTouched, setPasswordTouched] = useState(false);
    const [confirmPasswordTouched, setConfirmPasswordTouched] = useState(false);

    const { errors } = useRegisterValidation({
        username,
        usernameTouched,
        email,
        emailTouched,
        password,
        passwordTouched,
        confirmPassword,
        confirmPasswordTouched,
    });

    if (isAuthenticated) {
        navigate('/')
    }

    const handleSubmit = async (e) => {
        e.preventDefault();

        setUsernameTouched(true);
        setEmailTouched(true);
        setPasswordTouched(true);
        setConfirmPasswordTouched(true);

        const hasErrors = Object.values(errors).some(error => error !== "")
        if (hasErrors) {
            return;
        }

        const payload = {
            Username: username,
            Email: email,
            Password: password,
            ComfirmedPassword: confirmPassword
        }
        const wasUserSuccessfullyRegistered = await registerUser(payload)

        if (wasUserSuccessfullyRegistered) {
            console.log("Registered successfully!")
            await loadUser()
            navigate('/')
        } else {
            console.log("Failed to register.")
        }

    };

return (
    <div className="auth-page-container">
        <div className="auth-form-wrapper">
            <div className="auth-form-heading">
                <h2>Create an Account</h2>
                <p>Join us today! Fill in the details to register.</p>
            </div>
            <form onSubmit={handleSubmit}>
                <div className="auth-form-group">
                    <input
                        type="text"
                        name="username"
                        className="form-control"
                        placeholder="Username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        onBlur={() => setUsernameTouched(true)}
                    />
                    {errors.username && <small className="auth-error-text">{errors.username}</small>}
                </div>
                <div className="auth-form-group">
                    <input
                        type="email"
                        name="email"
                        className="form-control"
                        placeholder="Email Address"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        onBlur={() => setEmailTouched(true)}
                    />
                    {errors.email && <small className="auth-error-text">{errors.email}</small>}
                </div>
                <div className="auth-form-group">
                    <input
                        type="password"
                        name="password"
                        className="form-control"
                        placeholder="Password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        onBlur={() => setPasswordTouched(true)}
                    />
                    {errors.password && <small className="auth-error-text">{errors.password}</small>}
                </div>
                <div className="auth-form-group">
                    <input
                        type="password"
                        name="confirmPassword"
                        className="form-control"
                        placeholder="Confirm Password"
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        onBlur={() => setConfirmPasswordTouched(true)}
                    />
                    {errors.confirmPassword && <small className="auth-error-text">{errors.confirmPassword}</small>}
                </div>
                <button type="submit" className="auth-main-button">
                    Register
                </button>
            </form>
            <p className="auth-footer-text">
                Already have an account? <Link to="/login">Login here</Link>
            </p>
        </div>
    </div>
);

}
