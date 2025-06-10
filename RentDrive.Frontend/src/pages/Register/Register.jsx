import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useRegisterValidation } from './useRegisterValidation';
import { useRegisterPost } from './useRegisterPost';
import { useAuth } from '../../context/AccountContext';

export default function Register() {

    const {registerUser, loading, error } = useRegisterPost()
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

    if(isAuthenticated){
        navigate('/')
    }

    const handleSubmit = async (e) => {
        e.preventDefault();

        setUsernameTouched(true);
        setEmailTouched(true);
        setPasswordTouched(true);
        setConfirmPasswordTouched(true);

        const hasErrors = Object.values(errors).some(error => error !== "")
        if(hasErrors){
            return;
        }

        const payload = {
            Username: username,
            Email: email,
            Password: password,
            ComfirmedPassword: confirmPassword
        }
        const wasUserSuccessfullyRegistered = await registerUser(payload)

        if(wasUserSuccessfullyRegistered){
            console.log("Registered successfully!")
            await loadUser()
            navigate('/')            
        } else{
            console.log("Failed to register: " + JSON.stringify(error))
        }

    };

    return (
        <>
            <div className="page-heading">
                <div className="container">
                    <div className="col-lg-5 mx-auto">
                        <div className="section-heading text-center mb-5">
                            <h2>Create an Account</h2>
                            <p>Join us today! Fill in the details to register.</p>
                        </div>
                        <form onSubmit={handleSubmit}>
                            <div className="form-group mb-3">
                                <input
                                    type="text"
                                    name="username"
                                    className="form-control"
                                    placeholder="Username"
                                    value={username}
                                    onChange={(e) => setUsername(e.target.value)}
                                    onBlur={() => setUsernameTouched(true)}
                                />
                                {errors.username && <small className="error-text">{errors.username}</small>}
                            </div>
                            <div className="form-group mb-3">
                                <input
                                    type="email"
                                    name="email"
                                    className="form-control"
                                    placeholder="Email Address"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    onBlur={() => setEmailTouched(true)}
                                />
                                {errors.email && <small className="error-text">{errors.email}</small>}
                            </div>
                            <div className="form-group mb-3">
                                <input
                                    type="password"
                                    name="password"
                                    className="form-control"
                                    placeholder="Password"
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    onBlur={() => setPasswordTouched(true)}
                                />
                                {errors.password && <small className="error-text">{errors.password}</small>}
                            </div>
                            <div className="form-group mb-4">
                                <input
                                    type="password"
                                    name="confirmPassword"
                                    className="form-control"
                                    placeholder="Confirm Password"
                                    value={confirmPassword}
                                    onChange={(e) => setConfirmPassword(e.target.value)}
                                    onBlur={() => setConfirmPasswordTouched(true)}
                                />
                                {errors.confirmPassword && <small className="error-text">{errors.confirmPassword}</small>}
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
