import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";

export default function VehicleCalendar({bookedDates}) {
    const today = new Date();
    const sixMonthsLater = new Date();
    sixMonthsLater.setMonth(today.getMonth() + 6);

    const bookedDateObjects = bookedDates.map(dateStr => new Date(dateStr));

    const isSameDay = (date1, date2) =>
        date1.getFullYear() === date2.getFullYear() &&
        date1.getMonth() === date2.getMonth() &&
        date1.getDate() === date2.getDate();

    const tileClassName = ({ date, view }) => {
        if (view !== "month") return "";

        if (bookedDateObjects.some(d => isSameDay(d, date)) && date > today) {
            return "vehicle-calendar__tile vehicle-calendar__tile--booked";
        }

        if (date >= today && date <= sixMonthsLater) {
            return "vehicle-calendar__tile vehicle-calendar__tile--available";
        }

        return "vehicle-calendar__tile";
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
                    <span className="vehicle-calendar__legend-box vehicle-calendar__legend-box--available"></span>
                    <small>Available</small>
                    <span className="vehicle-calendar__legend-box vehicle-calendar__legend-box--booked"></span>
                    <small>Booked</small>
                </div>
            </div>
        </div>
    );
}
