import React, { useState } from "react";
import "./ChatSidebar.css";

export default function ChatSidebar({ onSelectUser }) {
  const [search, setSearch] = useState("");

  const users = [
    { id: 1, name: "Alice", online: true },
    { id: 2, name: "Bob", online: false },
    { id: 3, name: "Charlie", online: true },
    { id: 4, name: "David", online: true },
    { id: 5, name: "Eve", online: false },
  ];

  const filteredUsers = users.filter(user =>
    user.name.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className="chat-sidebar">
      <div className="chat-sidebar-header">
        <input
          type="text"
          placeholder="Search users..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          className="chat-sidebar-search"
        />
      </div>

      <div className="chat-sidebar-users">
        {filteredUsers.length > 0 ? (
          filteredUsers.map(user => (
            <div
              key={user.id}
              className="chat-sidebar-user"
              onClick={() => onSelectUser(user)}
            >
              <div className="user-avatar">{user.name.charAt(0)}</div>
              <div className="user-name">{user.name}</div>
              {user.online && <div className="user-status online" />}
            </div>
          ))
        ) : (
          <div className="chat-sidebar-empty">No users found</div>
        )}
      </div>
    </div>
  );
}
