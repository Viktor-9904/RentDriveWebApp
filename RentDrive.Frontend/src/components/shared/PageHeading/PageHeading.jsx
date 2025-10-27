import "./PageHeading.css"

export default function PageHeading({
  topPadding = 230,
  bottomPadding = 120,
  subTitle = "Not Set",
  mainTitle = "Not Set"
}) {
  return (
    <div
      className="page-heading"
      style={{ padding: `${topPadding}px 0px ${bottomPadding}px 0px` }}
    >
      <div className="container">
        <div className="row">
          <div className="col-lg-8">
            <div
              className="top-text header-text">
              <h6>{subTitle}</h6>
              <h2>{mainTitle}</h2>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
