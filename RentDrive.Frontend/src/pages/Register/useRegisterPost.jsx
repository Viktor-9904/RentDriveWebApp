import { useState } from "react";
import { useBackendURL } from "../../hooks/useBackendURL";

export function useRegisterPost() {
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(null)
    const backEndURL = useBackendURL();

    
    const registerUser = async (payload) => {
        setLoading(true)
        setError(null)

        try {
            const response = await fetch(`${backEndURL}/api/account/register`, {
                method: "POST",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(payload)
            })

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