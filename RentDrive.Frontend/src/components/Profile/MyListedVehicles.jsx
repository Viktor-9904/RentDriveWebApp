import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import DeleteConfirmationModal from "../Vehicles/DeleteConfirmationModal";
import useDeleteVehicle from "../Vehicles/hooks/useDeleteVehicle";
import useUserVehicles from "./hooks/useUserVehicles";

export default function MyVehicles() {
  const { userVehicles, uservehiclesLoading, uservehiclesError } = useUserVehicles()
  const [myVehicles, setMyVehicles] = useState([]);
  const [showDeleteModal, setShowDeleteModal] = useState(false)
  const [vehicleToDelete, setVehicleToDelete] = useState({});
  const backEndURL = import.meta.env.VITE_API_URL;

  useEffect(() => {
    if (userVehicles) {
      setMyVehicles(userVehicles)
    }
  }, [userVehicles])

  useEffect(() => {
    console.log(vehicleToDelete)
  }, [vehicleToDelete])

  const { deleteVehicle } = useDeleteVehicle();

  const navigate = useNavigate();
  const location = useLocation();

  const handleEdit = (vehicleId) => {
    navigate(`/manage/vehicles/edit/${vehicleId}`)
  };

  const handleDelete = (vehicle) => {
    setVehicleToDelete(vehicle)
    setShowDeleteModal(true)
  };

  const handleAddNew = () => {
    navigate('/manage/vehicles/create')
  };

  const handleConfirmDelete = async () => {
    const { success, error } = await deleteVehicle(vehicleToDelete.id, location.pathname);

    if (success) {
      console.log("Vehicle deleted!");

      setMyVehicles((prev) => prev.filter(v => v.id !== vehicleToDelete.id));

      setShowDeleteModal(false);
      setVehicleToDelete({});
    } else {
      console.error("Delete failed:", error);
      alert(error);
    }
  };

  return (
    <div className="my-vehicles-container">
      <div className="my-vehicles-header">
        <h3 className="my-vehicles-heading">My Vehicles</h3>
        <button className="add-my-vehicle-button" onClick={handleAddNew}>
          Add New Vehicle
        </button>
      </div>


      {myVehicles.length > 0 ? (
        <table className="my-vehicles-table">
          <thead>
            <tr>
              <th>Image</th>
              <th>Make & Model</th>
              <th>Fuel Type</th>
              <th>Price/Day</th>
              <th>Times Booked</th>
              <th>Rating</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {myVehicles.map((vehicle) => (
              <tr key={vehicle.id}>
                <td>
                  <img
                    src={`${backEndURL}/${vehicle.imageUrl}`}
                    alt={`${vehicle.make} ${vehicle.model}`}
                    className="my-vehicle-image"
                  />
                </td>
                <td>{vehicle.make} {vehicle.model}</td>
                <td>{vehicle.fuelType}</td>
                <td>{vehicle.pricePerDay.toFixed(2)} €</td>
                <td>{vehicle.timesBooked}</td>
                <td>{vehicle.rating} / 10</td>
                <td>
                  <button
                    onClick={() => handleEdit(vehicle.id)}
                    className="edit-button"
                  >
                    Edit
                  </button>
                  <button
                    onClick={() => handleDelete(vehicle)}
                    className="delete-button"
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p className="no-vehicles-message">No vehicles posted.</p>
      )}


      <DeleteConfirmationModal
        show={showDeleteModal}
        onClose={() => {
          setShowDeleteModal(false);
          setVehicleToDelete({});
        }}
        onConfirm={handleConfirmDelete}
        Make={vehicleToDelete?.make}
        Model={vehicleToDelete?.model}
      />
    </div>
  );
}
