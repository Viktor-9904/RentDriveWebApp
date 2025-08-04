import { useEffect, useState } from "react";

export function useWalletTransactionHistory() {
    const [walletTransactionHistory, setWalletTransactionHistory] = useState(null);
    const [walletTransactionHistoryLoading, setWalletTransactionHistoryLoading] = useState(true);
    const [walletTransactionHistoryError, setWalletTransactionHistoryError] = useState(null);
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchWalletTransactionHistory = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/wallettransaction/history`, {
                    method: "GET",
                    credentials: "include",
                    headers: {
                        "Content-Type": "application/json",
                    },
                })
                console.log(response)

                if (!response.ok) {
                    throw new Error("Failed to fetch overview details");
                }

                const result = await response.json();
                console.log(result)
                setWalletTransactionHistory(result);

            } catch (err) {
                setWalletTransactionHistoryError(err.message);
            } finally {
                setWalletTransactionHistoryLoading(false);
            }
        };

        fetchWalletTransactionHistory();
    }, []);

    return { walletTransactionHistory, walletTransactionHistoryLoading, walletTransactionHistoryError };
}
