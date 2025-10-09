import { useEffect, useState } from "react";
import { useBackendURL } from "./useBackendURL";

export default function useLoadChatHistory(receiverId) {
    const [chatMessages, setChatMessages] = useState([]);
    const [chatMessagesLoading, setChatMessagesLoading] = useState(true);
    const [chatMessagesError, setChatMessagesError] = useState(null);
    const backEndURL = useBackendURL();

    useEffect(() => {
        if (!receiverId || receiverId === "")
            return;
        
        const fetchChatHistory = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/chat/load-chat-history/${receiverId}`,{
                    method: "GET",
                    credentials: "include"
                });
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setChatMessages(data);
            } catch (err) {
                setChatMessagesError(err.message || "Failed to fetch recent chats.");
            } finally {
                setChatMessagesLoading(false);
            }
        };

        fetchChatHistory();
    }, [receiverId]);

    return { chatMessages, chatMessagesLoading, chatMessagesError };
}