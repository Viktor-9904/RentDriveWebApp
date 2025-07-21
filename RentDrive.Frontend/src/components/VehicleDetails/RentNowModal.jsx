import React, { useEffect, useState } from "react";
import RentNowCalendar from "./RentNowCalendar";

export default function RentNowModal({ showRentNowModal, onClose, bookedDates, pricePerDay, handleRent }) {
    const [selectedDates, setSelectedDates] = useState([]);

    const formatDate = (date) => {
        if (!date) return "";
        const day = date.getDate();
        const month = date.toLocaleString("default", { month: "long" });
        const year = date.getFullYear();
        return `${day} ${month} ${year}`;
    };

    const totalPrice = selectedDates.length > 0 ? pricePerDay * selectedDates.length : 0;

    useEffect(() => {
        const formattedDates = selectedDates.map(formatDate);
        console.log("Selected:", formattedDates);
    }, [selectedDates]);

    if (!showRentNowModal) return null;

    const clearSelection = () => {
        setSelectedDates([]);
    };

    const handleClose = () => {
        clearSelection();
        onClose();
    };

    const handleDateClick = (clickedDate) => {
        const sorted = [...selectedDates].sort((a, b) => a - b);
        const time = clickedDate.setHours(0, 0, 0, 0);
        const exists = selectedDates.some((date) => date.getTime() === time);

        if (!selectedDates.length) {
            setSelectedDates([new Date(time)]);
            return;
        }

        if (exists) {
            const index = sorted.findIndex((date) => date.getTime() === time);
            if (index === sorted.length - 1) {
                setSelectedDates(sorted.slice(0, -1));
            } else {
                setSelectedDates(sorted.slice(0, index));
            }
            return;
        }

        const last = sorted[sorted.length - 1];
        const nextDay = new Date(last);
        nextDay.setDate(last.getDate() + 1);

        if (time === nextDay.getTime()) {
            setSelectedDates([...sorted, new Date(time)]);
        }
    };

    return (
        <div className="rent-now-modal-overlay" onClick={handleClose}>
            <div className="rent-now-modal" onClick={(e) => e.stopPropagation()}>
                <button className="rent-now-modal-close" onClick={handleClose}>
                    ×
                </button>
                <h4>Select Rental Dates</h4>

                <div className="calendar-wrapper mb-3">
                    <RentNowCalendar
                        bookedDates={bookedDates}
                        selectedDates={selectedDates}
                        onDayClick={handleDateClick}
                    />
                    <div className="legend mt-3 d-flex gap-3 justify-content-center">
                        <div className="rentnow-calendar__legend-box rentnow-calendar__legend-box--available"></div>
                        <small>Available</small>
                        <span className="vehicle-calendar__legend-box vehicle-calendar__legend-box--booked"></span>
                        <small>Booked</small>
                        <div className="rentnow-calendar__legend-box rentnow-calendar__legend-box--selected"></div>
                        <small>Selected</small>
                    </div>
                </div>

                <div
                    className={`selected-date-range ${selectedDates.length === 0 ? "empty" : ""
                        }`}
                >
                    {selectedDates.length === 0
                        ? "Select available dates."
                        : formatDate(selectedDates[0]) +
                        (selectedDates.length > 1 ? ` - ${formatDate(selectedDates[selectedDates.length - 1])}` : "")}
                </div>

                <div className="price-info">
                    <div className="price-box">
                        Total price: <strong>{totalPrice.toFixed(2)}€</strong>
                    </div>
                </div>

                <div className="rent-now-modal-footer">
                    <button className="btn btn-success" onClick={() => handleRent && handleRent(selectedDates)}
                    >
                        Confirm Rent
                    </button>
                    <button className="btn btn-secondary" onClick={handleClose}>
                        Cancel
                    </button>
                </div>

            </div>
        </div>
    );
}
