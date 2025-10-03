import React, { useEffect, useRef, useState } from "react";
import "./Chat.css";
import ChatSidebar from "../ChatSideBar/ChatSideBar";

export default function Chat() {
    const [messages, setMessages] = useState([
        { id: 1, sender: "Alice", text: "Hey! How are you?", time: "10:01 AM", fromMe: false },
        { id: 2, sender: "Me", text: "I’m good, thanks! Working on a new feature.", time: "10:02 AM", fromMe: true },
        { id: 3, sender: "Alice", text: "Nice — tell me when it's ready 😊", time: "10:03 AM", fromMe: false },
    ]);

    const [input, setInput] = useState("");
    const chatBodyRef = useRef(null);

    useEffect(() => {
        if (chatBodyRef.current) {
            chatBodyRef.current.scrollTop = chatBodyRef.current.scrollHeight;
        }
    }, [messages]);

    const sendMessage = (e) => {
        e?.preventDefault();
        const trimmed = input.trim();
        if (!trimmed) return;
        const now = new Date();
        const timeStr = now.toLocaleTimeString([], { hour: "2-digit", minute: "2-digit" });
        const next = {
            id: Date.now(),
            sender: "Me",
            text: trimmed,
            time: timeStr,
            fromMe: true,
        };
        setMessages((prev) => [...prev, next]);
        setInput("");
    };

    return (
            <div className="chat-container">
                <div className="chat-header">
                    <div className="chat-header-left">
                        <div className="chat-title">Chat with Alice</div>
                        <div className="chat-subtitle">Online</div>
                    </div>
                </div>

                <main className="chat-body" ref={chatBodyRef} role="log" aria-live="polite">
                    {messages.map((msg) => (
                        <div
                            key={msg.id}
                            className={`message ${msg.fromMe ? "message--me" : "message--them"}`}
                            title={`${msg.sender} • ${msg.time}`}
                        >
                            {!msg.fromMe && <div className="message-sender">{msg.sender}</div>}
                            <div className="message-bubble">
                                <div className="message-text">{msg.text}</div>
                                <div className="message-time">{msg.time}</div>
                            </div>
                        </div>
                    ))}
                    <div ref={chatBodyRef} />
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
