const vehicles = [
  {
    id: 1,
    title: "BMW M3",
    type: "Car - Sedan",
    productionYear: "2019",
    fuelType: "Petrol",
    pricePerDay: "$120",
    imageUrl:
      "https://cdn.motor1.com/images/mgl/1ZQrxK/s1/2023-bmw-m3-cs-first-drive-review.webp",
  },
  {
    id: 2,
    title: "Harley Davidson Iron 883",
    type: "Motorcycle - Naked",
    productionYear: "2021",
    fuelType: "Petrol",
    pricePerDay: "$60",
    imageUrl:
      "https://d1oi5m316zaa67.cloudfront.net/921fe5ee-02de-456d-9e2f-3df2eeef415b-Harley-Davidson-Sportster-Iron-883-Custom-Right-2048x1536.jpg",
  },
];

export default function MyListedVehicles() {
  return (
    <div className="my-listed-vehicles">
      <h3 className="my-listed-vehicles__heading">My Vehicles</h3>

      {vehicles.map((vehicle) => (
        <div className="my-listed-vehicles__card" key={vehicle.id}>
          <img
            src={vehicle.imageUrl}
            alt={vehicle.title}
            className="my-listed-vehicles__image"
          />

          <div className="my-listed-vehicles__info">
            <h4 className="my-listed-vehicles__title">{vehicle.title}</h4>
            <p>
              <strong>Type:</strong> {vehicle.type}
            </p>
            <p>
              <strong>Date of Production:</strong> {vehicle.productionYear}
            </p>
            <p>
              <strong>Fuel Type:</strong> {vehicle.fuelType}
            </p>
            <p>
              <strong>Price/Day:</strong> {vehicle.pricePerDay}
            </p>
          </div>
        </div>
      ))}
    </div>
  );
}
