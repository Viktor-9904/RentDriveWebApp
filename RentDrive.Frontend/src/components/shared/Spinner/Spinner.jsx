import "./Spinner.css"

export default function Spinner({message = ""}) {
  return (
    <div className="local-spinner-wrapper">
      <div className="local-spinner-box">
        <img
          src="/assets/images/car-spinner.gif"
          alt="Loading..."
          className="local-spinner"
        />
        <p className="local-spinner-text">Loading {message}...</p>
      </div>
    </div>
  );
}
