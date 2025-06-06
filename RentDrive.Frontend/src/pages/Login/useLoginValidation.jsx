import { useEffect, useState } from "react";

export function useLoginValidation({
    emailOrUsername,
    password,
    emailOrUsernameTouched,
    passwordTouched,
}) {
    const [errors, setErrors] = useState({
        emailOrUsername: "",
        password: ""
    })

    useEffect(() => {
        const newErrors = {}

        if (!emailOrUsername && emailOrUsernameTouched) {
            newErrors.emailOrUsername = "Email or Username is required!"
        }
        if (!password && passwordTouched) {
            newErrors.password = "Password is required!"
        }

        setErrors({
            emailOrUsername: newErrors.emailOrUsername || "",
            password: newErrors.password || ""
        })
    }, [emailOrUsername, emailOrUsernameTouched, password, passwordTouched])

    const isValid = Object.values(errors).every((e) => e === "");

    return { errors, isValid }
}