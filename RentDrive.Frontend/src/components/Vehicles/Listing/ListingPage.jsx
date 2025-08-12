import React, { useEffect, useState } from 'react';
import { Range, getTrackBackground } from "react-range";
import ListingPageItem from './ListingPageItem';
import useAllVehicles from '../hooks/useAllVehicles';
import useAllVehicleTypes from '../hooks/useAllVehicleTypes';
import useAllVehicleCategories from '../hooks/useAllVehicleCategories';
import useFilterVehiclePropertiesByTypeId from '../hooks/useFilterVehiclePropertiesByTypeId';

export default function ListingPage() {
    const backEndURL = import.meta.env.VITE_API_URL;

    const [selectedTypeId, setSelectedTypeId] = useState("");
    const [selectedCategoryId, setSelectedCategoryId] = useState("");

    const { vehicles, loading, error } = useAllVehicles();
    const { vehicleTypes, loadingVehicleTypes, errorVehicleTypes } = useAllVehicleTypes();
    const { vehicleCategories, loadingVehicleCategories, errorVehicleCategories } = useAllVehicleCategories();
    const { filterVehicleProperties, filterVehiclePropertiesLoading, filterVehiclePropertiesError } = useFilterVehiclePropertiesByTypeId(selectedTypeId);

    const [selectedFilters, setSelectedFilters] = useState({});
    const [localVehicles, setLocalVehicles] = useState([]);
    const [localVehicleTypes, setLocalVehicleTypes] = useState([]);
    const [localVehicleTypeCategories, setLocalVehicleTypeCategories] = useState([]);
    const [localFilterdVehicleTypeProperties, setLocalFilteredVehicleTypeProperties] = useState([]);

    const [baseFilters, setBaseFilters] = useState({
        makes: [],
        colors: [],
        fuelType: "",
        pricePerDay: [50, 5000],
        yearOfProduction: [1999, new Date().getFullYear()],
    });

    const priceRange = { min: 0, max: 10000 };
    const yearRange = { min: 1990, max: new Date().getFullYear() };

    const availableMakes = ["Toyota", "Ford", "BMW", "Tesla"];
    const availableColors = ["Red", "Blue", "Black", "White"];
    const availableFuelTypes = ["Petrol", "Diesel"]

    const [openProperties, setOpenProperties] = useState({});
    const [makeOpen, setMakeOpen] = useState(false);
    const [colorOpen, setColorOpen] = useState(false);

    const togglePropertyOpen = (propertyId) => {
        setOpenProperties(prev => ({
            ...prev,
            [propertyId]: !prev[propertyId]
        }));
    };

    const renderSlider = (label, min, max, values, onChange, unit = "") => (
        <div className="slider-container mb-4">
            <label className="slider-label fw-bold">
                {label}: {values[0]} {unit} - {values[1]} {unit}</label>
            <Range
                step={1}
                min={min}
                max={max}
                values={values}
                onChange={onChange}
                renderTrack={({ props, children }) => {
                    const percentStart = ((values[0] - min) / (max - min)) * 100;
                    const percentEnd = ((values[1] - min) / (max - min)) * 100;

                    return (
                        <div
                            onMouseDown={props.onMouseDown}
                            onTouchStart={props.onTouchStart}
                            className="slider-track-wrapper"
                        >
                            <div
                                ref={props.ref}
                                className="slider-track"
                                style={{
                                    '--percent-start': `${percentStart}%`,
                                    '--percent-end': `${percentEnd}%`,
                                }}
                            >
                                {children}
                            </div>
                        </div>
                    );
                }}
                renderThumb={({ props, index }) => {
                    const { key, ...rest } = props;
                    return (
                        <div
                            key={key}
                            {...rest}
                            className="slider-thumb"
                            data-index={index}
                        >
                            <div className="slider-thumb-indicator" />
                        </div>
                    );
                }}
            />
        </div>
    );

    useEffect(() => {
        if (!selectedTypeId) return;

        const fetchFilteredVehicles = async () => {
            try {
                const payload = Object.entries(selectedFilters)
                    .map(([propertyId, values]) => ({ propertyId, values }));

                const res = await fetch(`${backEndURL}/api/vehicles/filter`, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify(payload)
                });

                if (!res.ok) throw new Error("Failed to fetch vehicles");
                const data = await res.json();
                setLocalVehicles(data);
            } catch (err) {
                console.error(err);
            }
        };

        fetchFilteredVehicles();
    }, [selectedFilters, selectedTypeId]);

    useEffect(() => {
        setLocalVehicleTypes(vehicleTypes);
    }, [vehicleTypes])

    useEffect(() => {
        setLocalVehicleTypeCategories(vehicleCategories);
    }, [vehicleCategories])

    useEffect(() => {
        setLocalVehicles(vehicles)
    }, [vehicles])

    useEffect(() => {
        setLocalFilteredVehicleTypeProperties(filterVehicleProperties)
    }, [filterVehicleProperties]);

    return (
        <div className="listing-page">
            <div className="container">
                <div className="row">
                    <div className="col-lg-3 mb-4">
                        <div className="sidebar-filters">

                            <h5 className="mb-3">Vehicle Types</h5>
                            <select
                                className="form-select"
                                value={selectedTypeId}
                                onChange={e => {
                                    setSelectedTypeId(e.target.value);
                                    setSelectedCategoryId("");
                                }}
                            >
                                <option value="">Select vehicle type</option>
                                {localVehicleTypes.map(type => (
                                    <option key={type.id} value={type.id}>
                                        {type.name}
                                    </option>
                                ))}
                            </select>

                            {selectedTypeId && (
                                <div className="mt-4">
                                    <h6 className="mb-2">
                                        {localVehicleTypes.find(t => t.id === selectedTypeId)?.name} Categories
                                    </h6>
                                    <select
                                        className="form-select"
                                        value={selectedCategoryId}
                                        onChange={e => setSelectedCategoryId(e.target.value)}
                                    >
                                        <option value="">Select category</option>
                                        {localVehicleTypeCategories
                                            .filter(cat => cat.vehicleTypeId === selectedTypeId)
                                            .map(cat => (
                                                <option key={cat.id} value={cat.id}>
                                                    {cat.name}
                                                </option>
                                            ))}
                                    </select>
                                </div>
                            )}

                            <div className="base-properties mt-4">
                                <h6>Base Properties</h6>
                                <hr />

                                <div className="mt-3">
                                    <div className="collapsible-header" onClick={() => setMakeOpen(open => !open)}>
                                        Make
                                        <span>{makeOpen ? "▲" : "▼"}</span>
                                    </div>

                                    {makeOpen && (
                                        <ul className="list-unstyled ms-3">
                                            {availableMakes.map(make => (
                                                <li key={make}>
                                                    <label>
                                                        <input
                                                            type="checkbox"
                                                            className="me-2"
                                                            checked={baseFilters.makes.includes(make)}
                                                            onChange={() => {
                                                                const isChecked = baseFilters.makes.includes(make);
                                                                setBaseFilters(prev => {
                                                                    const newMakes = isChecked
                                                                        ? prev.makes.filter(m => m !== make)
                                                                        : [...prev.makes, make];
                                                                    return { ...prev, makes: newMakes };
                                                                });
                                                            }}
                                                        />
                                                        {make}
                                                    </label>
                                                </li>
                                            ))}
                                        </ul>
                                    )}
                                </div>

                                <div className="mt-3">
                                    <div className="collapsible-header" onClick={() => setColorOpen(open => !open)}>
                                        Color
                                        <span>{colorOpen ? "▲" : "▼"}</span>
                                    </div>
                                    {colorOpen && (
                                        <ul className="list-unstyled ms-3">
                                            {availableColors.map(color => (
                                                <li key={color}>
                                                    <label>
                                                        <input
                                                            type="checkbox"
                                                            className="me-2"
                                                            checked={baseFilters.colors.includes(color)}
                                                            onChange={() => {
                                                                const isChecked = baseFilters.colors.includes(color);
                                                                setBaseFilters(prev => {
                                                                    const newColors = isChecked
                                                                        ? prev.colors.filter(c => c !== color)
                                                                        : [...prev.colors, color];
                                                                    return { ...prev, colors: newColors };
                                                                });
                                                            }}
                                                        />
                                                        {color}
                                                    </label>
                                                </li>
                                            ))}
                                        </ul>
                                    )}
                                </div>

                                <div className="mt-3">
                                    <label htmlFor="fuelType" className="form-label fw-bold">
                                        Fuel Type
                                    </label>
                                    <select
                                        id="fuelType"
                                        className="form-select"
                                        value={baseFilters.fuelType || ""}
                                        onChange={e =>
                                            setBaseFilters(prev => ({ ...prev, fuelType: e.target.value }))
                                        }
                                    >
                                        <option value="">Select fuel type</option>
                                        {availableFuelTypes.map(fuel => (
                                            <option key={fuel} value={fuel}>
                                                {fuel}
                                            </option>
                                        ))}
                                    </select>
                                </div>

                                <div className="mt-3">
                                    <div className="slider-container">
                                        {renderSlider(
                                            "Price per Day",
                                            priceRange.min,
                                            priceRange.max,
                                            baseFilters.pricePerDay,
                                            values => setBaseFilters(prev => ({ ...prev, pricePerDay: values })),
                                            "€"
                                        )}

                                        {renderSlider(
                                            "Year of Production",
                                            yearRange.min,
                                            yearRange.max,
                                            baseFilters.yearOfProduction,
                                            values => setBaseFilters(prev => ({ ...prev, yearOfProduction: values }))
                                        )}
                                    </div>
                                </div>
                            </div>

                            {selectedTypeId && (
                                <div className="type-properties">
                                    <h6 className="mb-2">
                                        {localVehicleTypes.find(v => v.id === selectedTypeId)?.name} Properties
                                    </h6>
                                    <hr />
                                    <div>
                                        {localFilterdVehicleTypeProperties?.properties?.map(prop => (
                                            <div key={prop.propertyId} className="mb-3">
                                                <div
                                                    className="collapsible-header"
                                                    onClick={() => togglePropertyOpen(prop.propertyId)}
                                                >
                                                    {prop.propertyName}{" "}
                                                    <span>{openProperties[prop.propertyId] ? "▲" : "▼"}</span>
                                                </div>

                                                {openProperties[prop.propertyId] && (
                                                    <ul className="list-unstyled ms-3 mt-2">
                                                        {prop.propertyValues.map(val => {
                                                            const isChecked =
                                                                selectedFilters[prop.propertyId]?.includes(val.propertyValue) ||
                                                                false;

                                                            return (
                                                                <li key={val.propertyValue}>
                                                                    <label>
                                                                        <input
                                                                            type="checkbox"
                                                                            value={val.propertyValue}
                                                                            className="me-2"
                                                                            checked={isChecked}
                                                                            onChange={() => {
                                                                                setSelectedFilters(prev => {
                                                                                    const currentValues = prev[prop.propertyId] || [];
                                                                                    const newValues = isChecked
                                                                                        ? currentValues.filter(v => v !== val.propertyValue)
                                                                                        : [...currentValues, val.propertyValue];

                                                                                    return { ...prev, [prop.propertyId]: newValues };
                                                                                });
                                                                            }}
                                                                        />
                                                                        {val.propertyValue}{" "}
                                                                        {val.unitOfMeasurement !== "None" ? val.unitOfMeasurement : ""}{" "}
                                                                        ({val.count})
                                                                    </label>
                                                                </li>
                                                            );
                                                        })}
                                                    </ul>
                                                )}
                                            </div>
                                        ))}
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>

                    <div className="col-lg-9">
                        <div className="row">
                            {localVehicles?.map(vehicle => (
                                <ListingPageItem
                                    key={vehicle.id}
                                    id={vehicle.id}
                                    make={vehicle.make}
                                    model={vehicle.model}
                                    vehicleType={vehicle.vehicleType}
                                    vehicleTypeCategory={vehicle.vehicleTypeCategory}
                                    yearOfProduction={vehicle.yearOfProduction}
                                    pricePerDay={vehicle.pricePerDay}
                                    fuelType={vehicle.fuelType}
                                    imageURL={vehicle.imageURL}
                                    ownerName={vehicle.ownerName}
                                    starsRating={vehicle.starsRating}
                                    reviewCount={vehicle.reviewCount}
                                />
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
