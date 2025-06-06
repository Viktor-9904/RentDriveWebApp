import { useEffect, useState } from "react";

export function useRegisterValidation({
    username,
    usernameTouched,
    email,
    emailTouched,
    password,
    passwordTouched,
    confirmPassword,
    confirmPasswordTouched,

}) {
    const [errors, setErrors] = useState({
        username: "",
        email: "",
        password: "",
        confirmPassword: "",
    })

    useEffect(() => {
        const newErrors = {}

        if (!username && usernameTouched) {
            newErrors.username = "Username is required!"
        } else if (username.length > 0 && username.length < 3) {
            newErrors.username = "Username must be at least 3 characters long!"
        } else if (username.length > 20) {
            newErrors.username = "Username must not exceed 20 characters!"
        }
        if (!email && emailTouched) {
            newErrors.email = "Email is required!"
        } else if (email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
            newErrors.email = "Invalid email address!";
        }

        if (!password && passwordTouched) {
            newErrors.password = "Password is required!"
        } else if (password.length > 0 && password.length < 6) {
            newErrors.password = "Password must be at least 6 characters long!"
        } else if (password.length > 20) {
            newErrors.password = "Password must not exceed 50 characters!"
        } else if (!/[a-z]/.test(password) && passwordTouched) {
            newErrors.password = "Password must contain at least one lowercase letter!";
        } else if (!/[A-Z]/.test(password) && passwordTouched) {
            newErrors.password = "Password must contain at least one uppercase letter!";
        } else if (!/[0-9]/.test(password) && passwordTouched) {
            newErrors.password = "Password must contain at least one digit!";
        }


        if (!confirmPassword && confirmPasswordTouched) {
            newErrors.confirmPassword = "Comfirmed password is required!"
        } else if (confirmPassword.length > 0 && confirmPassword !== password) {
            newErrors.confirmPassword = "Password doesn't match!"
        }

        setErrors({
            username: newErrors.username || "",
            email: newErrors.email || "",
            password: newErrors.password || "",
            confirmPassword: newErrors.confirmPassword || ""
        })

    }, [username, usernameTouched, email, emailTouched, password, passwordTouched, confirmPassword, confirmPasswordTouched])

    const isValid = Object.values(errors).every((e) => e === "");

    return { errors, isValid }
}