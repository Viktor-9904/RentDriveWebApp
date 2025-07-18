import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import useAllVehicleTypes from "../hooks/useAllVehicleTypes";
import useAllVehicleTypeProperties from "../hooks/useAllVehicleTypeProperties";
import useFuelTypes from "../../../hooks/useFuelTypesEnum";
import useValueTypesEnum from "../../../hooks/useValueTypesEnum";
import useUnitsEnum from "../../../hooks/useUnitsEnum";
import useAllVehicleCategories from "../hooks/useAllVehicleCategories";


export default function CreateVehicleForm() {
  const backEndURL = import.meta.env.VITE_API_URL;
  const navigate = useNavigate();

  const { vehicleTypes, loadingVehicleTypes, errorVehicleTypes } = useAllVehicleTypes();
  const { vehicleTypeProperties, loadingVehicleTypeProperties, errorVehicleTypeProperties } = useAllVehicleTypeProperties();
  const { vehicleCategories, loadingVehicleCategories, errorVehicleCategories } = useAllVehicleCategories();

  const { valueTypeEnum, loadingValueTypeEnum, errorValueTypeEnum } = useValueTypesEnum();
  const { unitsEnum, loadingUnitsEnum, errorUnitsEnum } = useUnitsEnum();
  const { fuelTypeEnum, loadingfuelTypeEnum, errorfuelTypeEnum } = useFuelTypes();

  const [selectedTypeId, setSelectedTypeId] = useState("")
  const [selectedCategoryTypeId, setSelectedCategoryTypeId] = useState("");
  const [filteredProperties, setFilteredProperties] = useState([]);
  const [vehicleTypePropertyValues, setVehicleTypePropertyValues] = useState({});
  const [images, setImages] = useState([]);


  const [baseData, setBaseData] = useState({
    make: "",
    model: "",
    color: "",
    pricePerDay: "",
    dateOfProduction: "",
    curbWeight: "",
    description: "",
    fuelType: fuelTypeEnum[0],
    vehicleTypeId: "",
    vehicleTypeCategoryId: "",
    vehicleTypePropertyValues: {}
  });

  const sortedProperties = [...filteredProperties].sort((a, b) => {
    return a.valueType === "Boolean" ? 1 : b.valueType === "Boolean" ? -1 : 0;
  });

  useEffect(() => {
    if (vehicleTypeProperties && selectedTypeId !== null) {
      const filtered = vehicleTypeProperties.filter(
        p => p.vehicleTypeId === selectedTypeId
      );
      setFilteredProperties(filtered);

      const initialValues = {};
      filtered.forEach((prop) => {
        if (prop.valueType === "Boolean") {
          initialValues[prop.id] = false;
        } else {
          initialValues[prop.id] = "";
        }
      });
      setVehicleTypePropertyValues(initialValues);
    }
  }, [vehicleTypeProperties, selectedTypeId]);

  useEffect(() => {
    if (vehicleTypeProperties && selectedTypeId !== null) {
      const filtered = vehicleTypeProperties.filter(
        p => p.vehicleTypeId === selectedTypeId
      );
      setFilteredProperties(filtered);
    }

    if (selectedTypeId !== null) {
      setBaseData((prev) => ({
        ...prev,
        vehicleTypeId: selectedTypeId,
      }));
      setSelectedCategoryTypeId("")
    }
  }, [vehicleTypeProperties, selectedTypeId]);

  const handleBaseChange = (e) => {
    const { name, value, type, checked } = e.target;
    setBaseData((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handleImageChange = (e) => {
    const files = Array.from(e.target.files);
    const remainingSlots = 15 - images.length;
    const newFiles = files.slice(0, remainingSlots);

    const newPreviews = newFiles.map((file) => ({
      file,
      url: URL.createObjectURL(file),
    }));

    setImages((prev) => [...prev, ...newPreviews]);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (images.length === 0) {
      alert("Please upload at least one image.");
      return;
    }

    const formData = new FormData();

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

    if (vehicleTypePropertyValues) {
      Object.entries(vehicleTypePropertyValues).forEach(([propertyId, value], index) => {
        formData.append(`PropertyValues[${index}].PropertyId`, propertyId);
        formData.append(`PropertyValues[${index}].Value`, String(value));
      });
    }

    images.forEach((img) => {
      formData.append("Images", img.file);
    });

    try {
      const response = await fetch(`${backEndURL}/api/vehicle/create`, {
        method: "POST",
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


  const handleVehicleTypePropertyChange = (propId, value) => {
    setVehicleTypePropertyValues((prev) => ({
      ...prev,
      [propId]: value,
    }));
    setBaseData((prev) => ({
      ...prev,
      vehicleTypePropertyValues: vehicleTypePropertyValues,
    }));
  };

  const handleCancel = () => {
    navigate(`/listing`)
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
          <select
            className="form-select"
            value={selectedTypeId}
            required
            onChange={(e) => setSelectedTypeId(Number(e.target.value))}
          >
            <option value="" disabled hidden>
              -- Select Vehicle Type --
            </option>
            {vehicleTypes.map(type => (
              <option key={type.id} value={type.id}>
                {type.name}
              </option>
            ))}
          </select>
        </label>

        {selectedTypeId && sortedProperties.length > 0 && (
          <div className="vehicle-type-properties">

            {selectedTypeId && vehicleCategories && (
              <label className="vehicle-type-label">
                Vehicle Category:
                <select
                  className="form-select"
                  value={selectedCategoryTypeId}
                  required
                  onChange={(e) => setSelectedCategoryTypeId(Number(e.target.value))}
                >
                  <option value="" disabled hidden>
                    -- Select Vehicle Category --
                  </option>
                  {vehicleCategories
                    .filter(category => category.vehicleTypeId === selectedTypeId)
                    .map(category => (
                      <option key={category.id} value={category.id}>
                        {category.name}
                      </option>
                    ))}
                </select>
              </label>
            )}


            {sortedProperties.map((prop) => {
              let inputType = "text";

              switch (prop.valueType) {
                case "Int":
                case "Double":
                  inputType = "number";
                  break;
                case "Boolean":
                  inputType = "checkbox";
                  break;
                case "String":
                default:
                  inputType = "text";
                  break;
              }

              const value = vehicleTypePropertyValues[prop.id] ?? (inputType === "checkbox" ? false : "");

              return (
                <label key={prop.id} className={inputType === "checkbox" ? "checkbox-label" : ""}>
                  <span>
                    {prop.name}
                    {inputType === "checkbox" && ":"}
                  </span>
                  <input
                    type={inputType}
                    name={prop.name}
                    required={inputType !== "checkbox"}
                    className={inputType === "checkbox" ? "styled-checkbox" : ""}
                    checked={inputType === "checkbox" ? value : undefined}
                    value={inputType !== "checkbox" ? value : undefined}
                    min={inputType === "number" ? 1 : undefined}
                    onChange={(e) =>
                      handleVehicleTypePropertyChange(
                        prop.id,
                        inputType === "checkbox" ? e.target.checked : e.target.value
                      )
                    }
                  />
                </label>
              );
            })}

          </div>
        )}
      </div>

      <div className="upload-section">
        <div className="image-preview-wrapper">
          {images.length > 0 ? (
            <>
              <img
                src={images[0].url}
                alt="First preview"
                className="preview-img"
              />
              {images.length > 1 && (
                <div className="image-count-overlay">
                  {images.length} images
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
          <button type="submit">Create Vehicle</button>
          <button type="button" onClick={handleCancel}>Cancel</button>
        </div>
      )}
    </form>
  );
}
