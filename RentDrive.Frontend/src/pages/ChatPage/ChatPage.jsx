import { useEffect, useState } from "react";

import Chat from "../../components/Chat/ChatMessenger/Chat";
import ChatSidebar from "../../components/Chat/ChatSideBar/ChatSideBar";
import PageHeading from "../../components/shared/PageHeading/PageHeading";

import "./ChatPage.css";

export default function ChatPage() {
  const [selectedUser, setSelectedUser] = useState(null);

  return (
    <>
      <PageHeading
        topPadding={180}
        bottomPadding={90}
        subTitle={<span>Chat Dashboard</span>}
        mainTitle={<span>Your Messages<br/><br/></span>}
      />

      <div className={`chat-page-container ${selectedUser ? "chat-active" : "chat-list-view"}`}>
        <ChatSidebar onSelectUser={setSelectedUser} />

        <div className="chat-main">
          {selectedUser ? (
            <Chat selectedUser={selectedUser} />
          ) : (
            <div className="chat-placeholder">
              Select a user to start chatting
            </div>
          )}
        </div>
      </div>
    </>
  );
}
