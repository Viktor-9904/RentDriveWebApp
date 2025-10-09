import { useEffect, useState } from "react";
import { useBackendURL } from "./useBackendURL";

export default function useRecentChatHistory() {
    const [recentChats, setRecentChats] = useState([]);
    const [recentChatsLoading, setRecentChatsLoading] = useState(true);
    const [recentChatsError, setRecentChatsError] = useState(null);
    const backEndURL = useBackendURL();

    useEffect(() => {
        const fetchRecentChats = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/chat/recent-chats`,{
                    method: "GET",
                    credentials: "include"
                });
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setRecentChats(data);
            } catch (err) {
                setRecentChatsError(err.message || "Failed to fetch recent chats.");
            } finally {
                setRecentChatsLoading(false);
            }
        };

        fetchRecentChats();
    }, []);

    return { recentChats, recentChatsLoading, recentChatsError };
}