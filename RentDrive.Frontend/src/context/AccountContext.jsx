import { createContext, useState, useEffect, useContext } from 'react';

const AccountContext = createContext();

export function AccountProvider({ children }) {
  const backEndURL = import.meta.env.VITE_API_URL;
  const [user, setUser] = useState(null);

  const loadUser = async () => {
    try {
      const response = await fetch(`${backEndURL}/api/account/me`, { credentials: 'include' });
      if (!response.ok) throw new Error('Not authenticated');
      const data = await response.json();
      setUser(data);
    } catch {
      setUser(null);
    }
  };

  useEffect(() => {
    loadUser();
  }, []);

  const isAuthenticated = Boolean(user);

  return (
    <AccountContext.Provider value={{ user, isAuthenticated, loadUser }}>
      {children}
    </AccountContext.Provider>
  );
}

export function useAuth() {
  return useContext(AccountContext);
}
