import "./Spinner.css"

export default function Spinner() {
  return (
    <div className="local-spinner-wrapper">
      <div className="local-spinner-box">
        <img
          src="/assets/images/car-spinner.gif"
          alt="Loading..."
          className="local-spinner"
        />
        <p className="local-spinner-text">Loading vehicles...</p>
      </div>
    </div>
  );
}
