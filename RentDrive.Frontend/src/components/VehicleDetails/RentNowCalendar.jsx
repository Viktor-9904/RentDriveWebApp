import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";

export default function RentNowCalendar({
  bookedDates,
  selectedDates = [],
  onDayClick,
}) {
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

    const time = date.setHours(0, 0, 0, 0);

    if (selectedDates.some(d => d.setHours(0, 0, 0, 0) === time)) {
      return "rentnow-calendar__tile rentnow-calendar__tile--selected";
    }

    if (bookedDateObjects.some(d => isSameDay(d, date)) && date > today) {
      return "rentnow-calendar__tile rentnow-calendar__tile--booked";
    }

    if (date >= today && date <= sixMonthsLater) {
      return "rentnow-calendar__tile rentnow-calendar__tile--available";
    }

    return "rentnow-calendar__tile";
  };

  return (
    <Calendar
      tileClassName={tileClassName}
      minDate={today}
      maxDate={sixMonthsLater}
      next2Label={null}
      prev2Label={null}
      showNeighboringMonth={false}
      onClickDay={onDayClick}
    />
  );
}
