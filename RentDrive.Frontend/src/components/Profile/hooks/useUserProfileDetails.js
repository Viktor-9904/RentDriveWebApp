import { useEffect, useState } from "react";
import { useBackendURL } from "../../../hooks/useBackendURL";

export function useUserProfileDetails() {
    const [userProfileDetails, setUserProfileDetails] = useState(null);
    const [userProfileDetailsLoading, setUserProfileDetailsLoading] = useState(true);
    const [userProfileDetailsError, setUserProfileDetailsError] = useState(null);
    const backEndURL = useBackendURL();

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

                if (!response.ok) {
                    throw new Error("Failed to fetch overview details");
                }

                const result = await response.json();
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
