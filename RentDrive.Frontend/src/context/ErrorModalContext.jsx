import React, { createContext, useContext, useState } from "react";
import ErrorModal from "../components/shared/ErrorModal/ErrorModal";

const ErrorModalContext = createContext();

export function useErrorModal() {
  return useContext(ErrorModalContext);
}

export function ErrorModalProvider({ children }) {
  const [errorModalIsVisible, setErrorModalIsVisible] = useState(false);
  const [errorModalMessage, setErrorModalMessageInternal] = useState("");

  const setErrorModalMessage = (message) => {
    setErrorModalMessageInternal(message);
    setErrorModalIsVisible(true);
  };

  const hideModal = () => setErrorModalIsVisible(false);

  return (
    <ErrorModalContext.Provider value={{ errorModalIsVisible, setErrorModalMessage, hideModal }}>
      {children}
      <ErrorModal
        show={errorModalIsVisible}
        onClose={hideModal}
        message={errorModalMessage}
      />
    </ErrorModalContext.Provider>
  );
}
