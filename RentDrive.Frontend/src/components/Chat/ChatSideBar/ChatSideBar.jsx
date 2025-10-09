import React, { useEffect, useState } from "react";

import useFetchUsersByQuery from "../../../hooks/useFetchUsersByQuery";
import useRecentChatHistory from "../../../hooks/useRecentChatsHistory";
import "./ChatSideBar.css";

export default function ChatSidebar({ onSelectUser }) {
  const [search, setSearch] = useState("");
  const [debouncedSearch, setDebouncedSearch] = useState("");

  const { users, usersLoading, usersError } = useFetchUsersByQuery(debouncedSearch);
  const [searchQueryUsersResult, setSearchQueryUsersResult] = useState([]);

  const { recentChats, recentChatsLoading, recentChatsError } = useRecentChatHistory();
  const [localRecentChats, setLocalRecentChats] = useState([]);

  useEffect(() => {
    setSearchQueryUsersResult(users);
  }, [users])

  useEffect(() => {
    setLocalRecentChats(recentChats)
  }, [recentChats])

  useEffect(() => {
    const handler = setTimeout(() => {
      setDebouncedSearch(search);
    }, 300);

    return () => clearTimeout(handler);
  }, [search]);

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
        {search && (
          <div className="chat-sidebar-dropdown">
            {searchQueryUsersResult.length > 0 ? (
              searchQueryUsersResult.map((user) => (
                <div
                  key={user.userId}
                  className="chat-sidebar-user"
                  onClick={() => {
                    setLocalRecentChats(prev => {
                      const filtered = prev.filter(u => u.userId !== user.userId);
                      return [user, ...filtered];
                    });
                    onSelectUser(user),
                      setSearch("")
                  }}
                >
                  <div className="user-avatar">{user.username.charAt(0)}</div>
                  <div className="user-name">{user.username}</div>
                  {user.online && <div className="user-status online" />}
                </div>
              ))
            ) : (
              <div key="no-results" className="chat-sidebar-noresults">No users found</div>
            )}
          </div>
        )}
      </div>

      <div className="chat-sidebar-users">
        {localRecentChats?.length > 0 ? (
          localRecentChats?.map(user => (
            <div
              key={user.userId}
              className="chat-sidebar-user"
              onClick={() => onSelectUser(user)}
            >
              <div className="user-avatar">{user.username.charAt(0)}</div>
              <div className="user-name">{user.username}</div>
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
