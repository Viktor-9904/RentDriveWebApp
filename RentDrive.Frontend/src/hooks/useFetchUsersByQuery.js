import { useEffect, useState } from "react";
import { useBackendURL } from "./useBackendURL";

export default function useFetchUsersByQuery(searchQuery) {
    const [users, setUsers] = useState([]);
    const [usersLoading, setLoadingUsers] = useState(true);
    const [usersError, setUsersError] = useState(null);
    const backEndURL = useBackendURL();

    useEffect(() => {

        if (!searchQuery || searchQuery === "")
            return;
        const fetchUsers = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/chat/search-users/${searchQuery}`,{
                    method: "GET",
                    credentials: "include"
                });
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setUsers(data);
            } catch (err) {
                setUsersError(err.message || "Failed to fetch users");
            } finally {
                setLoadingUsers(false);
            }
        };

        fetchUsers();
    }, [searchQuery]);

    return { users, usersLoading, usersError };
}