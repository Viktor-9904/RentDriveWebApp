import { useState } from "react";
import Chat from "../../components/Chat/ChatMessenger/Chat";
import ChatSidebar from "../../components/Chat/ChatSideBar/ChatSideBar";
import PageHeading from "../../components/shared/PageHeading";
import "./ChatPage.css";

export default function ChatPage() {
  const [selectedUser, setSelectedUser] = useState(null);

  return (
    <>
      <PageHeading
        topPadding={180}
        bottomPadding={90}
        subTitle="Chat Dashboard"
        mainTitle="Your Messages"
      />

      <div className="chat-page-container">
        <ChatSidebar onSelectUser={setSelectedUser} />
        
        <div className="chat-main">
          {selectedUser ? (
            <Chat key={selectedUser.id} user={selectedUser} />
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
