import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const useVehicleEdit = (id) => {
    const [vehicle, setVehicle] = useState(null);
    const [loadingVehicle, setVehicleLoading] = useState(true);
    const [errorVehicle, setVehicleError] = useState(null);
    const navigate = useNavigate();
    const backEndURL = import.meta.env.VITE_API_URL;

    useEffect(() => {
        const fetchVehicleDetails = async () => {
            try {
                const response = await fetch(`${backEndURL}/api/vehicle/edit/${id}`, {
                    method: "GET",
                    headers: { "Content-Type": "application/json" },
                });

                if (!response.ok) {
                    navigate("/listing");
                    throw new Error("Failed to fetch vehicle details.");
                }

                const data = await response.json();
                setVehicle(data);
            } catch (err) {
                setVehicleError(err.message);
                console.error(err);
            } finally {
                setVehicleLoading(false);
            }
        };

        if (id) {
            fetchVehicleDetails();
        }
    }, [id, navigate]);

    return { vehicle, loadingVehicle, errorVehicle };
};

export default useVehicleEdit;
