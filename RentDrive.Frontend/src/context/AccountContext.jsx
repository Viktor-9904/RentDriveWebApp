import { createContext, useState, useEffect, useContext } from 'react';
import { useBackendURL } from '../hooks/useBackendURL';

const AccountContext = createContext();

export function AccountProvider({ children }) {
  const backEndURL = useBackendURL();
  const [user, setUser] = useState(null);
  const [isUserLoading, setIsUserLoading] = useState(true);

  const loadUser = async () => {
    try {
      const response = await fetch(`${backEndURL}/api/account/me`,
        {
          credentials: 'include'
        });
      if (!response.ok) throw new Error('Not authenticated');
      const data = await response.json();
      setUser(data);
      
    } catch {
      setUser(null);
    }
    finally{
      setIsUserLoading(false)
    }
  };

  useEffect(() => {
    loadUser();
  }, []);

  const isCompanyEmployee = user?.isCompanyEmployee;
  const isAuthenticated = Boolean(user);

  return (
    <AccountContext.Provider value={{ user, isAuthenticated, isCompanyEmployee, isUserLoading, loadUser }}>
      {children}
    </AccountContext.Provider>
  );
}

export function useAuth() {
  return useContext(AccountContext);
}
