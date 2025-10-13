import React, { useEffect, useState } from 'react';
import { Range, getTrackBackground } from "react-range";
import { useSearchParams } from "react-router-dom";

import { useBackendURL } from '../../../hooks/useBackendURL';
import ListingPageItem from './ListingPageItem';
import useAllVehicles from '../hooks/useAllVehicles';
import useAllVehicleTypes from '../hooks/useAllVehicleTypes';
import useAllVehicleCategories from '../hooks/useAllVehicleCategories';
import useFilterVehiclePropertiesByTypeId from '../hooks/useFilterVehiclePropertiesByTypeId';
import useBaseFilterProperties from '../hooks/useBaseFilterProperties';

export default function ListingPage() {
    const backEndURL = useBackendURL();
    const [searchQuery, setSearchQuery] = useState("");
    const [triggeredSearchQuery, setTriggeredSearchQuery] = useState("");

    const handleSearch = () => {
        setTriggeredSearchQuery(searchQuery);
    };

    const [selectedTypeId, setSelectedTypeId] = useState("");
    const [selectedCategoryId, setSelectedCategoryId] = useState("");

    const { baseFilterProperties, baseFilterPropertiesLoading, baseFilterPropertiesError } = useBaseFilterProperties(selectedTypeId, selectedCategoryId);

    const { vehicles, loading, error } = useAllVehicles();
    const { vehicleTypes, loadingVehicleTypes, errorVehicleTypes } = useAllVehicleTypes();
    const { vehicleCategories, loadingVehicleCategories, errorVehicleCategories } = useAllVehicleCategories();
    const { filterVehicleProperties, filterVehiclePropertiesLoading, filterVehiclePropertiesError } = useFilterVehiclePropertiesByTypeId(selectedTypeId);

    const [selectedFilters, setSelectedFilters] = useState({});

    const [localVehicles, setLocalVehicles] = useState([]);
    const [localVehicleTypes, setLocalVehicleTypes] = useState([]);
    const [localVehicleTypeCategories, setLocalVehicleTypeCategories] = useState([]);

    const [localBaseFilterdProperties, setLocalBaseFilteredProperties] = useState([]);
    const [localFilterdVehicleTypeProperties, setLocalFilteredVehicleTypeProperties] = useState([]);

    const [searchParams] = useSearchParams();

    const [openProperties, setOpenProperties] = useState({});
    const [makeOpen, setMakeOpen] = useState(false);
    const [colorOpen, setColorOpen] = useState(false);

    const [baseFilters, setBaseFilters] = useState({
        makes: [],
        colors: [],
        fuelType: "",
        pricePerDay: [0, 0],
        yearOfProduction: [1970, 1970],
    });

        const togglePropertyOpen = (propertyId) => {
        setOpenProperties(prev => ({
            ...prev,
            [propertyId]: !prev[propertyId]
        }));
    };

    const getSliderBounds = (min, max) => {
        if (min === max) {
            return { min: Math.floor(min - 1), max: Math.ceil(max + 1) };
        }
        return { min: Math.floor(min), max: Math.ceil(max) };
    };

    const priceBounds = getSliderBounds(baseFilterProperties?.minPrice || 0, baseFilterProperties?.maxPrice || 0);
    const yearBounds = getSliderBounds(baseFilterProperties?.minYearOfProduction || 1970, baseFilterProperties?.maxYearOfProduction || 1970);

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

    // Search params
    useEffect(() => {
        const typeName = searchParams.get("type");
        const categoryName = searchParams.get("category");
        const fuel = searchParams.get("fuel");

        if (fuel) {
            const normalizedFuel = fuel.charAt(0).toUpperCase() + fuel.slice(1).toLowerCase();
            setBaseFilters(prev => ({
                ...prev,
                fuelType: normalizedFuel,
            }));
        } else {
            setBaseFilters(prev => ({
                ...prev,
                fuelType: "",
            }));

        }

        if (localVehicleTypes.length > 0) {
            if (typeName) {
                const matchedType = localVehicleTypes.find(
                    t => t.name.toLowerCase() === typeName.toLowerCase()
                );
                setSelectedTypeId(matchedType ? matchedType.id : 0);
            } else {
                setSelectedTypeId(0);
            }
        }

        if (localVehicleTypeCategories.length > 0) {
            if (categoryName) {
                const matchedCategory = localVehicleTypeCategories.find(
                    c => c.name.toLowerCase() === categoryName.toLowerCase()
                );
                setSelectedCategoryId(matchedCategory ? matchedCategory.id : "");
            } else {
                setSelectedCategoryId("");
            }
        }
    }, [searchParams, localVehicleTypes, localVehicleTypeCategories]);

    // Load Base Filters
    useEffect(() => {
        if (localBaseFilterdProperties?.minPrice !== undefined && localBaseFilterdProperties?.maxPrice !== undefined) {
            setBaseFilters(prev => ({
                ...prev,
                pricePerDay: [localBaseFilterdProperties.minPrice, localBaseFilterdProperties.maxPrice],
                yearOfProduction: [localBaseFilterdProperties.minYearOfProduction, localBaseFilterdProperties.maxYearOfProduction],
            }));
        }
    }, [localBaseFilterdProperties]);

    useEffect(() => {
        if (!vehicles || vehicles.length === 0) return; 

        const debounceTimer = setTimeout(() => {
            const fetchFilteredVehicles = async  () => {
                try {
                    const payload = {
                        vehicleTypeId: selectedTypeId || null,
                        vehicleTypeCategoryId: selectedCategoryId || null,
                        makes: baseFilters.makes.map(m => m.name),
                        colors: baseFilters.colors.map(c => c.name),
                        fuelType: baseFilters.fuelType,
                        minPrice: baseFilters.pricePerDay[0],
                        maxPrice: baseFilters.pricePerDay[1],
                        minYear: baseFilters.yearOfProduction[0],
                        maxYear: baseFilters.yearOfProduction[1],
                        searchQuery: triggeredSearchQuery,
                        properties: Object.entries(selectedFilters).map(([propertyId, values]) => ({ propertyId, values }))
                    };

                    const res = await fetch(`${backEndURL}/api/vehicle/filter`, {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify(payload),
                    });

                    if (!res.ok) {
                        throw new Error("Failed to fetch vehicles");
                    }
                    const filteredIds = await res.json();
                    setLocalVehicles(vehicles.filter(v => filteredIds.includes(v.id)));

                } catch (err) {
                    console.error(err);
                }
            }
            fetchFilteredVehicles();
        }, 250);

        return () => {
            clearTimeout(debounceTimer)
        }

    }, [vehicles, selectedTypeId, selectedCategoryId, baseFilters, selectedFilters, triggeredSearchQuery]);
    
    useEffect(() => {
        setLocalVehicleTypes(vehicleTypes);
    }, [vehicleTypes])

    useEffect(() => {
        setLocalVehicleTypeCategories(vehicleCategories);
    }, [vehicleCategories])

    useEffect(() => {
        setLocalFilteredVehicleTypeProperties(filterVehicleProperties)
    }, [filterVehicleProperties]);

    useEffect(() => {
        setLocalBaseFilteredProperties(baseFilterProperties)
    }, [baseFilterProperties])

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
                                    setSelectedTypeId(e.target.value === "" ? null : e.target.value);
                                    setSelectedCategoryId("");
                                }}
                            >
                                <option value="">All</option>
                                {localVehicleTypes.map(type => (
                                    <option key={type.id} value={type.id}>
                                        {type.name}
                                    </option>
                                ))}
                            </select>

                            {selectedTypeId !== null && selectedTypeId !== undefined && selectedTypeId !== 0 && (
                                <div className="mt-4">
                                    <h6 className="mb-2">
                                        {localVehicleTypes.find(t => String(t.id) === String(selectedTypeId))?.name} Categories
                                    </h6>
                                    <select
                                        className="form-select"
                                        value={selectedCategoryId}
                                        onChange={e => setSelectedCategoryId(e.target.value)}
                                    >
                                        <option value="">Select category</option>
                                        {localVehicleTypeCategories
                                            .filter(cat => String(cat.vehicleTypeId) === String(selectedTypeId))
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
                                            {localBaseFilterdProperties?.makes?.map(make => (
                                                <li key={make.name}>
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
                                                        {make.name} <span className="text-muted">({make.count})</span>
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
                                            {localBaseFilterdProperties?.colors.map(color => (
                                                <li key={color.name}>
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
                                                        {color.name} <span className="text-muted">({color.count})</span>
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
                                        <option value="">All</option>
                                        {localBaseFilterdProperties?.fuelTypes?.map(ft => ft.name).map(fuel => (
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
                                            priceBounds.min,
                                            priceBounds.max,
                                            baseFilters.pricePerDay,
                                            values => setBaseFilters(prev => ({ ...prev, pricePerDay: values })),
                                            "€"
                                        )}

                                        {renderSlider(
                                            "Year of Production",
                                            yearBounds.min,
                                            yearBounds.max,
                                            baseFilters.yearOfProduction,
                                            values => setBaseFilters(prev => ({ ...prev, yearOfProduction: values }))
                                        )}
                                    </div>
                                </div>
                            </div>

                            {selectedTypeId !== null && selectedTypeId !== undefined && selectedTypeId !== 0 && (
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
                                                                        {val.propertyValue === "true"
                                                                            ? "Yes"
                                                                            : val.propertyValue === "false"
                                                                                ? "No"
                                                                                : val.propertyValue}{" "}
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
                        <div className="col-lg-5 ms-auto">
                            <div className="mb-4">
                                <div className="input-group">
                                    <input
                                        type="text"
                                        className="form-control"
                                        placeholder="Search by make, model, type..."
                                        value={searchQuery}
                                        onChange={(e) => setSearchQuery(e.target.value)}
                                        onKeyDown={(e) => e.key === "Enter" && handleSearch()}
                                    />
                                    <button className="btn btn-primary" type="button" onClick={handleSearch}>
                                        Search
                                    </button>
                                </div>
                            </div>
                        </div>

                        <div className="col-lg-12">
                            <div className="row">
                                {localVehicles?.length > 0 ? (
                                    localVehicles.map(vehicle => (
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
                                    ))
                                ) : (
                                    <div className="col-12 text-center p-5">
                                        <p>No vehicles found matching your filters.</p>
                                    </div>
                                )}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
