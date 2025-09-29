import React, { useEffect, useState } from 'react';
import { Link, Navigate, useNavigate } from 'react-router-dom'

import useAllVehicleTypes from '../Vehicles/hooks/useAllVehicleTypes';
import useAllVehicleCategories from '../Vehicles/hooks/useAllVehicleCategories';
import useFuelTypesEnum from '../../hooks/useFuelTypesEnum';

export default function MainBanner() {

    const navigate = useNavigate();

    const { vehicleTypes, loadingVehicleTypes, errorVehicleTypes } = useAllVehicleTypes();
    const { vehicleCategories, loadingVehicleCategories, errorVehicleCategories } = useAllVehicleCategories();
    const { fuelTypeEnum, loadingfuelTypeEnum, errorfuelTypeEnum } = useFuelTypesEnum();

    const [selectedTypeId, setSelectedTypeId] = useState("");
    const [selectedCategoryId, setSelectedCategoryId] = useState("");
    const [selectedFuelTypeId, setSelectedFuelTypeId] = useState("");

    const [localVehicleTypes, setLocalVehicleTypes] = useState([]);
    const [localVehicleCategories, setLocalVehicleCategories] = useState([]);
    const [localFuelTypes, setLocalFuelTypes] = useState([]);

    useEffect(() => {
        setLocalVehicleTypes(vehicleTypes);
    }, [vehicleTypes])

    useEffect(() => {
        setLocalVehicleCategories(vehicleCategories);
    }, [vehicleCategories])

    useEffect(() => {
        setLocalFuelTypes(fuelTypeEnum)
    },[fuelTypeEnum])

    const handleSubmit = (e) => {
        e.preventDefault();

        const selectedType = localVehicleTypes.find(type => type.id === Number(selectedTypeId))?.name;
        const selectedCategory = localVehicleCategories.find(category => category.id === Number(selectedCategoryId))?.name;
        const selectedFuelType = localFuelTypes.find(fuel => fuel.id === Number(selectedFuelTypeId))?.name;

        console.log(selectedType);
        console.log(selectedCategory);
        console.log(selectedFuelType);

        const params = new URLSearchParams();

        if (selectedType) params.append("type", selectedType);
        if (selectedCategory) params.append("category", selectedCategory);
        if (selectedFuelType && selectedFuelType !== "None") params.append("fuel", selectedFuelType);

        navigate(`/listing?${params.toString()}`);
    }

    return (
        <>
            <div className="main-banner">
                <div className="container">
                    <div className="row">
                        <div className="col-lg-12">
                            <div className="top-text header-text">
                                <h6>Over 100+ Active Listings</h6>
                                <h2>Find Your Next Ride</h2>
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <form onSubmit={handleSubmit} id="search-form" name="gs" method="submit" role="search" action="#">
                                <div className="row">
                                    <div className="col-lg-3 align-self-center">
                                        <fieldset className="flex-grow-1">
                                            <select
                                                id="vehicleType"
                                                name="vehicleType"
                                                className="form-select"
                                                defaultValue=""
                                                onChange={e => {
                                                    setSelectedTypeId(e.target.value);
                                                    selectedCategoryId !== "0" ? setSelectedCategoryId("") : "";
                                                }}
                                            >
                                                <option value="" disabled>
                                                    Select Vehicle Type
                                                </option>
                                                <option value={0}>All Vehicle Types</option>
                                                {localVehicleTypes.map(type => (
                                                    <option key={type.id} value={type.id}>
                                                        {type.name}
                                                    </option>
                                                ))}
                                            </select>
                                        </fieldset>
                                    </div>
                                    <div className="col-lg-3 align-self-center">
                                        <fieldset className="flex-grow-1">
                                            <select
                                                id="vehicleCategory"
                                                name="vehicleCategory"
                                                className="form-select searchSelect"
                                                value={selectedCategoryId}
                                                onChange={e => setSelectedCategoryId(e.target.value)}
                                            >
                                                <option value="" disabled>
                                                    Select Vehicle Category
                                                </option>
                                                <option value="All">All Vehicle Categories</option>
                                                {localVehicleCategories
                                                    .filter(category =>
                                                        Number(selectedTypeId) === 0 && selectedTypeId !== ""
                                                            ? true
                                                            : category.vehicleTypeId === Number(selectedTypeId)
                                                    )
                                                    .map(category => (
                                                        <option key={category.id} value={category.id}>
                                                            {category.name}
                                                        </option>
                                                    ))}
                                            </select>
                                        </fieldset>
                                    </div>
                                    <div className="col-lg-3 align-self-center">
                                        <fieldset className="flex-grow-1">
                                            <select
                                                id="fuelType"
                                                name="fuelType"
                                                className="form-select"
                                                onChange={e => setSelectedFuelTypeId(e.target.value)}
                                                value={selectedFuelTypeId} 
                                            >
                                                <option value="" disabled>
                                                    Select Fuel Type
                                                </option>
                                                <option value="All">All Fuel Types</option>
                                                {localFuelTypes.map(fuel => (
                                                    <option key={fuel.id} value={fuel.id}>
                                                        {fuel.name}
                                                    </option>
                                                ))}
                                            </select>
                                        </fieldset>
                                    </div>
                                    <div className="col-lg-3">
                                        <fieldset>
                                            <button className="main-button"><i className="fa fa-search"></i> Search Now</button>
                                        </fieldset>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div className="col-lg-10 offset-lg-1">
                            <ul className="categories">
                                <li><Link to="/categories"><span className="icon"><img src="assets/images/search-icon-01.png" alt="Home" /></span> Apartments</Link></li>
                                <li><Link to="/listing"><span className="icon"><img src="assets/images/search-icon-02.png" alt="Food" /></span> Food &amp; Life</Link></li>
                                <li><Link to="#"><span className="icon"><img src="assets/images/search-icon-03.png" alt="Vehicle" /></span> Cars</Link></li>
                                <li><Link to="#"><span className="icon"><img src="assets/images/search-icon-04.png" alt="Shopping" /></span> Shopping</Link></li>
                                <li><Link to="#"><span className="icon"><img src="assets/images/search-icon-05.png" alt="Travel" /></span> Traveling</Link></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}