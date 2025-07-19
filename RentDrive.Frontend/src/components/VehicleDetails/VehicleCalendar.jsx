import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";

export default function VehicleCalendar() {
    const today = new Date();
    const sixMonthsLater = new Date();
    sixMonthsLater.setMonth(today.getMonth() + 6);

    const bookedDates = [
        new Date("2025-07-22"),
        new Date("2025-07-23"),
        new Date("2025-07-27"),
        new Date("2025-07-28"),
        new Date("2025-07-29"),
    ];

    const isSameDay = (date1, date2) =>
        date1.getFullYear() === date2.getFullYear() &&
        date1.getMonth() === date2.getMonth() &&
        date1.getDate() === date2.getDate();

    const tileClassName = ({ date, view }) => {
        if (view !== "month") return "";

        if (bookedDates.some(d => isSameDay(d, date)) && date > today) {
            return "booked-tile";
        }

        if (date >= today && date <= sixMonthsLater) {
            return "available-tile";
        }

        return "";
    };
    
    return (
        <div className="calendar-container mt-5">
            <div className="calendar-box shadow-sm p-3 rounded">
                <Calendar
                    tileClassName={tileClassName}
                    minDate={today}
                    maxDate={sixMonthsLater}
                    next2Label={null}
                    prev2Label={null}
                    showNeighboringMonth={false}
                />
                <div className="legend mt-3 d-flex gap-3">
                    <span className="legend-box available"></span>
                    <small>Available</small>
                    <span className="legend-box booked"></span>
                    <small>Booked</small>
                </div>
            </div>
            <button className="btn btn-success rent-now-btn">Rent Now</button>
        </div>
    );
}
