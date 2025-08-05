import React, { useState, useEffect } from 'react';

import { useAuth } from '../../../context/AccountContext';
import { useWalletTransactionHistory } from '../hooks/useWalletTransactionHistory';

import AddFundsToBalance from './AddFundsModal';
import { formatDate } from 'react-calendar/dist/shared/dateFormatter.js';


export default function UserWallet() {

    const backEndURL = import.meta.env.VITE_API_URL;

    const { user, isAuthenticated, loadUser } = useAuth();
    const { walletTransactionHistory, walletTransactionHistoryLoading, walletTransactionHistoryError } = useWalletTransactionHistory()

    const [showModal, setShowModal] = useState(false);
    const [transactions, setTransactions] = useState([]);

    useEffect(() => {
        setTransactions(walletTransactionHistory)
    }, [walletTransactionHistory]);

    const handleAddFunds = async (formData, event) => {
        event.preventDefault();

        console.log(formData)
        
        try {
            const response = await fetch(`${backEndURL}/api/wallet/add-funds`, {
                method: "POST",
                credentials: "include",
                headers: {
                    "Content-Type": "application/json" 
                },
                body: JSON.stringify(formData),
            });

            if (!response.ok) {
                throw new Error("Failed to add funds to wallet.");
            }

            const newTransaction = await response.json();
            setTransactions(prev => [newTransaction, ...prev]);
            loadUser()
            setShowModal(false);
        } catch (error) {
            alert(error.message);
            console.error("Error adding funds:", error);
        }
    };

    function getTransactionTypeClass(type) {
        switch (type) {
            case 'Deposit':
            case 'Rental Profit':
            case 'Company Rental Fee Profit':
                return 'transaction-positive';
            case 'Withdraw':
            case 'Rental Payment':
                return 'transaction-negative';
            case 'Refund':
            default:
                return 'transaction-neutral';
        }
    }

    return (
        <div className="user-wallet-container">
            <div className="user-wallet-header">
                <div className="wallet-balance-box">
                    <span>Balance:</span>
                    <span className="wallet-balance-amount">
                        {user?.balance?.toFixed(2) ?? '0.00'} €
                    </span>
                </div>
                <button className="wallet-add-funds-button" onClick={() => setShowModal(true)}>
                    Add Funds
                </button>
            </div>

            {showModal &&
                <AddFundsToBalance
                    handleAddFunds={handleAddFunds}
                    setShowModal={setShowModal}
                />}

            <div className="wallet-transaction-history">
                <h4>Transaction History</h4>
                {transactions?.length > 0 ? (
                    <table className="transaction-table">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Type</th>
                                <th>Amount</th>
                            </tr>
                        </thead>
                        <tbody>
                            {transactions?.map((tx) => (
                                <tr key={tx.id}>
                                    <td>{tx.createdOn}</td>
                                    <td>
                                        <span className={`transaction-type ${getTransactionTypeClass(tx.type)}`}>
                                            {tx.type}
                                        </span>
                                    </td>
                                    <td
                                        className={
                                            tx.amount < 0
                                                ? 'wallet-transaction-negative'
                                                : 'wallet-transaction-positive'
                                        }
                                    >
                                        {tx.amount.toFixed(2)} €
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                ) : (
                    <div className="wallet-transaction-history__empty">
                        <h6>No transaction history available.</h6>
                    </div>
                )}
            </div>
        </div>
    );
};