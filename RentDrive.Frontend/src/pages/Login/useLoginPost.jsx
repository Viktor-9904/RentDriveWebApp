import { useState } from "react";

export function useLoginPost() {
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(null)
    const backEndURL = import.meta.env.VITE_API_URL;

    const loginUser = async (payload) => {
        console.log("in post")

        setLoading(true)
        setError(null)

        try {
            const response = await fetch(`${backEndURL}/api/account/login`, {
                method: "POST",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(payload)
            })
            
            if (!response.ok) {
                let errorData
                try {
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
    return { loginUser, loading, error }
}