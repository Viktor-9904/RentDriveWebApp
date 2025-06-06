import { useState } from "react";

export function useRegisterPost() {
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(null)
    const backEndURL = import.meta.env.VITE_API_URL;

    
    const registerUser = async (payload) => {
        console.log("in post")

        setLoading(true)
        setError(null)

        try {
            const response = await fetch(`${backEndURL}/api/account/register`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(payload)
            })
            console.log(response)

            if (!response.ok) {
                let errorData
                try{
                    errorData = await response.clone().json();
                } catch {
                    errorData = await response.text();
                }

                setError(errorData)
                setLoading(false)
                return false
            }

            setLoading(false)
            return true

        } catch (err) {
            setError(err.message)
            setLoading(false)
            return false
        }
    }
    return {registerUser, loading, error}
}