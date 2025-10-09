import React, { useEffect, useRef, useState } from "react";
import * as signalR from "@microsoft/signalr";

import { useBackendURL } from "../../../hooks/useBackendURL";
import { useAuth } from "../../../context/AccountContext";
import useLoadChatHistory from "../../../hooks/useLoadChatHistory";

import "./Chat.css";

export default function Chat({ selectedUser }) {
    const { user, isAuthenticated, loadUser } = useAuth();
    const backEndURL = useBackendURL();

    const { chatMessages, chatMessagesLoading, chatMessagesError } = useLoadChatHistory(selectedUser?.userId)
    const [localMessages, setLocalMessages] = useState([]);
    const [input, setInput] = useState("");

    const bottomRef = useRef(null);
    const hubRef = useRef(null);

    useEffect(() => {
        if (bottomRef.current) {
            bottomRef.current.scrollIntoView({ behavior: "smooth" });
        }
    }, [localMessages]);

    useEffect(() => {
        setLocalMessages(chatMessages);
    }, [chatMessages])

    useEffect(() => {
        //connect to signalR
        const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`${backEndURL}/chat`, {
                withCredentials: true,
            })
            .withAutomaticReconnect()
            .build();

        hubConnection.start()
            // .then(() => console.log("Connected"))
            // .catch(err => console.warn("Initial connection attempt failed:", err));

        //listening for messages
        hubConnection.on("ReceiveMessage", (message) => {
            setLocalMessages(prev => [
                ...prev,
                {
                    senderId: message.senderId,
                    receiverId: message.receiverId,
                    text: message.text,
                    timeSent: new Date(),
                }
            ]);
        });

        hubRef.current = hubConnection;

        return () => hubConnection.stop();
    }, [user]);

    const sendMessage = async (e) => {
        e?.preventDefault();
        const trimmed = input.trim();
        if (!trimmed || !hubRef.current || !user) return;

        try {
            const messageObj = {
                receiverId: selectedUser?.userId,
                text: input.trim(),
            };

            await hubRef.current.invoke("SendMessage", messageObj);

            setLocalMessages(prev => [
                ...prev,
                {
                    id: Date.now(),
                    senderId: user?.id,
                    text: trimmed,
                    timeSent: new Date(),
                }
            ]);

            setInput("");
        } catch (err) {
            console.error("Error sending message:", err);
        }
    };

    return (
        <div className="chat-container">
            <div className="chat-header">
                <div className="chat-header-left">
                    <div className="chat-title">Chat with {selectedUser?.username || "..."}</div>
                    <div className="chat-subtitle">Online</div>
                </div>
            </div>

            <main className="chat-body" role="log" aria-live="polite">
                {localMessages.map((msg, index) => {
                    const fromMe = msg.senderId === user?.id;
                    return (
                        <div
                            key={index}
                            className={`message ${fromMe ? "message--me" : "message--them"}`}
                            title={`${fromMe ? "Me" : "Them"} • ${new Date(msg.timeSent).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}`}
                        >
                            {!fromMe && <div className="message-sender">{selectedUser?.username}</div>}
                            <div className="message-bubble">
                                <div className="message-text">{msg.text}</div>
                                <div className="message-time">
                                    {new Date(msg.timeSent).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}
                                </div>
                            </div>
                        </div>
                    );
                })}
                <div ref={bottomRef} />
            </main>


            <form className="chat-input-area" onSubmit={sendMessage}>
                <input
                    type="text"
                    aria-label="Type a message"
                    placeholder="Type a message..."
                    className="chat-input"
                    value={input}
                    onChange={(e) => setInput(e.target.value)}
                />
                <button type="submit" className="chat-send" aria-label="Send message">
                    Send
                </button>
            </form>
        </div>
    );
}
