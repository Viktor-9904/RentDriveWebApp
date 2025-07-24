import { useNavigate } from "react-router-dom";

export default function useDeleteVehicle() {
    const navigate = useNavigate();
    const backEndURL = import.meta.env.VITE_API_URL;

    const deleteVehicle = async (vehicleId, redirectPath = "/listing") => {
        try {
            const res = await fetch(`${backEndURL}/api/vehicle/delete/${vehicleId}`, {
                method: "DELETE",
                credentials: "include",
            });

            if (!res.ok) {
                const message = await res.text();
                throw new Error(message || "Failed to delete vehicle");
            }

            navigate(redirectPath);
            return { success: true, error: null };

        } catch (err) {
            return { success: false, error: err.message || "Something went wrong" };
        }
    };

    return { deleteVehicle };
}
