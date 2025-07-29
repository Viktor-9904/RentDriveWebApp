import { useEffect, useState } from "react";

export function useUserProfileDetails() {
    const [userProfileDetails, setUserProfileDetails] = useState(null);
    const [userProfileDetailsLoading, setUserProfileDetailsLoading] = useState(true);
    const [userProfileDetailsError, setUserProfileDetailsError] = useState(null);
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchUserProfileDetails = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/account/user-profile-details`, {
                    method: "GET",
                    credentials: "include",
                    headers: {
                        "Content-Type": "application/json",
                    },
                })
                console.log(response)

                if (!response.ok) {
                    throw new Error("Failed to fetch overview details");
                }

                const result = await response.json();
                console.log(result)
                setUserProfileDetails(result);

            } catch (err) {
                setUserProfileDetailsError(err.message);
            } finally {
                setUserProfileDetailsLoading(false);
            }
        };

        fetchUserProfileDetails();
    }, []);

    return { userProfileDetails, userProfileDetailsLoading, userProfileDetailsError };
}
