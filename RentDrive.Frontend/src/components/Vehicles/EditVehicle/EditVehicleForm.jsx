import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import useFuelTypes from "../../../hooks/useFuelTypesEnum";
import useVehicleEdit from "../hooks/useVehicleEdit";
import { useBackendURL } from "../../../hooks/useBackendURL";


export default function EditVehicleForm() {
  const { id } = useParams();
  const backEndURL = useBackendURL();
  const navigate = useNavigate();

  const { vehicle, loadingVehicle, errorVehicle } = useVehicleEdit(id)
  const { fuelTypeEnum, loadingfuelTypeEnum, errorfuelTypeEnum } = useFuelTypes();

  const [selectedTypeId, setSelectedTypeId] = useState("")
  const [selectedCategoryTypeId, setSelectedCategoryTypeId] = useState("");
  const [existingImages, setExistingImages] = useState([]);
  const [newImages, setNewImages] = useState([]);


  const [baseData, setBaseData] = useState({
    make: "",
    model: "",
    color: "",
    pricePerDay: "",
    dateOfProduction: "",
    curbWeight: "",
    description: "",
    fuelType: "",
    vehicleTypeId: "",
    vehicleType: "",
    vehicleTypeCategoryId: "",
    vehicleTypeCategory: "",
    vehicleTypePropertyValues: []
  });

  useEffect(() => {
    if (!vehicle) return;
  
    setBaseData({
      make: vehicle.make,
      model: vehicle.model,
      color: vehicle.color,
      pricePerDay: vehicle.pricePerDay.toString(),
      dateOfProduction: vehicle.dateOfProduction.split("T")[0],
      curbWeight: vehicle.curbWeightInKg.toString(),
      description: vehicle.description,
      fuelType: vehicle.fuelType || "",
      vehicleType: vehicle.vehicleType,
      vehicleTypeCategory: vehicle.vehicleTypeCategory,
      vehicleTypePropertyValues: vehicle.vehicleTypePropertyValues
    });

    if (vehicle.imageURLs && vehicle.imageURLs.length > 0) {
      setExistingImages(
        vehicle.imageURLs.map(url => ({ url: `${backEndURL}/${url}` }))
      );
    }


    setNewImages([]);
    setSelectedTypeId(vehicle.vehicleTypeId);
    setSelectedCategoryTypeId(vehicle.vehicleTypeCategoryId);
    
  }, [vehicle, selectedTypeId, selectedCategoryTypeId]);

  const handleBaseChange = (e) => {
    const { name, value, type, checked } = e.target;
    setBaseData((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handleImageChange = (e) => {
    const files = Array.from(e.target.files);
    const remainingSlots = 15 - (existingImages.length + newImages.length);
    const newFiles = files.slice(0, remainingSlots);

    const newPreviews = newFiles.map((file) => ({
      file,
      url: URL.createObjectURL(file),
    }));

    setNewImages(prev => [...prev, ...newPreviews]);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if ((existingImages.length + newImages.length) === 0) {
      alert("Please upload at least one image.");
      return;
    }
    const formData = new FormData();

    formData.append("Id", id)
    formData.append("Make", baseData.make);
    formData.append("Model", baseData.model);
    formData.append("Color", baseData.color);
    formData.append("PricePerDay", baseData.pricePerDay);
    formData.append("FuelType", baseData.fuelType);
    formData.append("DateOfProduction", new Date(baseData.dateOfProduction).toISOString());
    formData.append("CurbWeightInKg", baseData.curbWeight);
    formData.append("Description", baseData.description);
    formData.append("VehicleTypeId", selectedTypeId);
    formData.append("VehicleTypeCategoryId", selectedCategoryTypeId);

    // formData.append("ExistingImageUrls", JSON.stringify(existingImages.map(img => img.url)));

    newImages.forEach(img => {
      formData.append("NewImages", img.file);
    });


    if (baseData.vehicleTypePropertyValues) {
      baseData.vehicleTypePropertyValues.forEach((prop, index) => {
        formData.append(`VehicleTypePropertyInputValues[${index}].PropertyId`, prop.propertyId);
        formData.append(`VehicleTypePropertyInputValues[${index}].Value`, String(prop.vehicleTypePropertyValue));
      });
    }

    try {
      for (let [key, value] of formData.entries()) {
        console.log(key, value);
      }
      const response = await fetch(`${backEndURL}/api/vehicle/edit/${id}`, {
        method: "Put",
        body: formData,
      });
      
      if (!response.ok) {
        throw new Error("Failed to save vehicle");
      }

      navigate('/listing');
    } catch (error) {
      alert(error.message);
      console.error("Error uploading vehicle:", error);
    }
  };

   const handleCancel = () => {
    navigate(`/api/vehicle/${id}`)
  };

  return (
    <form onSubmit={handleSubmit} className="create-vehicle-form">
      <label>
        Make:
        <input
          type="text"
          name="make"
          value={baseData.make}
          onChange={handleBaseChange}
          required
        />
      </label>

      <label>
        Model:
        <input
          type="text"
          name="model"
          value={baseData.model}
          onChange={handleBaseChange}
          required
        />
      </label>

      <label>
        Color:
        <input
          type="text"
          name="color"
          value={baseData.color}
          onChange={handleBaseChange}
          required
        />
      </label>

      <label>
        Price Per Day:
        <input
          type="number"
          name="pricePerDay"
          value={baseData.pricePerDay}
          onChange={handleBaseChange}
          required
          min={1}
        />
      </label>

      <label>
        Fuel Type:
        <select
          name="fuelType"
          value={baseData.fuelType}
          onChange={handleBaseChange}
          required
        >
          <option value="" disabled>
            -- Select Fuel Type --
          </option>
          {fuelTypeEnum?.map((type) => (
            <option key={type.id} value={type.name}>
              {type.name}
            </option>
          ))}
        </select>
      </label>


      <label>
        Date Of Production:
        <input
          type="date"
          name="dateOfProduction"
          value={baseData.dateOfProduction}
          onChange={handleBaseChange}
          required
        />
      </label>

      <label>
        Curb Weight (kg):
        <input
          type="number"
          name="curbWeight"
          value={baseData.curbWeight}
          onChange={handleBaseChange}
          required
          min={1}
        />
      </label>

      <label>
        Description:
        <textarea
          name="description"
          value={baseData.description}
          onChange={handleBaseChange}
          required
        />
      </label>

      <div className="vehicle-type-section">
        <label className="vehicle-type-label">
          Vehicle Type:
          <input
            type="text"
            className="form-input"
            value={baseData.vehicleType ?? ""}
            readOnly
            disabled
          />
        </label>

        <div className="vehicle-type-properties">

          <label className="vehicle-type-label">
            Vehicle Category:
            <input
              type="text"
              className="form-input"
              value={baseData.vehicleTypeCategory ?? ""}
              readOnly
              disabled
            />
          </label>

          {baseData.vehicleTypePropertyValues.map((prop) => {
            let inputType = "text";
            const valueType = prop.valueType;

            if (valueType === "Int" || valueType === "Double") {
              inputType = "number";
            } else if (valueType === "Boolean") {
              inputType = "checkbox";
            }

            const value = prop.vehicleTypePropertyValue;

            return (
              <label
                key={prop.propertyId}
                className={`vehicle-type-label ${inputType === "checkbox" ? "checkbox-label" : ""}`}
              >
                <span>
                  {prop.vehicleTypePropertyName}
                  {prop.unitOfMeasurement && prop.unitOfMeasurement !== "None"
                    ? ` (${prop.unitOfMeasurement})`
                    : ""}
                </span>

                <input
                  type={inputType}
                  className={inputType === "checkbox" ? "styled-checkbox" : "form-input"}
                  value={inputType !== "checkbox" ? value : undefined}
                  checked={inputType === "checkbox" ? value === "true" : undefined}
                  required={inputType !== "checkbox"}
                  min={inputType === "number" ? 1 : undefined}
                  onChange={(e) => {
                    const newValue =
                      inputType === "checkbox" ? e.target.checked.toString() : e.target.value;

                    setBaseData((prev) => ({
                      ...prev,
                      vehicleTypePropertyValues: prev.vehicleTypePropertyValues.map((p) =>
                        p.propertyId === prop.propertyId
                          ? { ...p, vehicleTypePropertyValue: newValue }
                          : p
                      )
                    }));
                  }}
                />
              </label>
            );
          })}
        </div>
      </div>



      <div className="upload-section">
        <div className="image-preview-wrapper">
          {(existingImages.length + newImages.length) > 0 ? (
            <>
              <img
                src={existingImages[0]?.url || newImages[0]?.url}
                alt="First preview"
                className="preview-img"
              />
              {(existingImages.length + newImages.length) > 1 && (
                <div className="image-count-overlay">
                  {existingImages.length + newImages.length} images
                </div>
              )}
            </>
          ) : (
            <div className="no-image-placeholder">No Images</div>
          )}
        </div>

        <label className="upload-button" htmlFor="imageUpload">
          Upload Images
        </label>

        <input
          id="imageUpload"
          type="file"
          accept="image/*"
          multiple
          onChange={handleImageChange}
          style={{ display: "none" }}
        />
      </div>

      {selectedTypeId && (
        <div className="form-buttons">
          <button type="submit">Update Vehicle</button>
          <button type="button" onClick={handleCancel}>Cancel</button>
        </div>
      )}
    </form>
  );
}
