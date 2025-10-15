# RentDrive

<p align="center">
  <img src="https://img.shields.io/github/actions/workflow/status/viktor-9904/RentDriveWebApp/docker-build.yml?branch=master" />
  &nbsp;&nbsp;&nbsp;&nbsp;
  <img src="https://img.shields.io/badge/Docker-Ready-blue?logo=docker" />
  &nbsp;&nbsp;&nbsp;&nbsp;
  <a href="https://rentdrive.eu"><img src="https://img.shields.io/badge/Live%20App-Online-brightgreen" /></a>
  &nbsp;&nbsp;&nbsp;&nbsp;
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet" />
  &nbsp;&nbsp;&nbsp;&nbsp;
  <img src="https://img.shields.io/badge/React-19-61DAFB?logo=react" />
  &nbsp;&nbsp;&nbsp;&nbsp;
  <img src="https://img.shields.io/badge/License-MIT-green" />
</p>

RentDrive is a vehicle rental web application where users can rent vehicles or list their own to earn profit. It supports a wide range of vehicles, with booking management and availability calendars to easily track rentals.  

This project is still under active development. 🚧

🌐 Live App: https://rentdrive.eu

The entire application is fully Dockerized and deployed on a Raspberry Pi 4B using Docker Compose and Watchtower, with a CI/CD pipeline via GitHub Actions for automatic build and deployment of updated images.


---

## ✨ Features  

### 🟢 Implemented
- **User Authentication** – Secure login & registration using ASP.NET Identity (cookie-based).  
- **Vehicle Management** – Add and manage vehicles with base properties (make, model, color, price per day, etc.).  
- **Dynamic Vehicle Fields** – Managers can create, edit, and delete:
  - Vehicle Types (Car, Motorcycle, Truck, etc.)  
  - Vehicle Categories (SUV, Sedan, Naked, Tour, etc.)  
  - Vehicle Properties (Engine Displacement, Door Count, Seat Count, etc.).  
- **Wallet System** – Users can top up wallets with card payments and use balance for rentals.  
- **Availability Calendar** – Check available vs. booked dates for each vehicle, up to 6 months ahead.  
- **Bookings** – Full-day rentals with status tracking (Active, Canceled, Completed).  
- **Search** – Quickly find vehicles by keyword (make, model, etc.).  
- **Chat:** Real-time chat between users with end-to-end message encryption.  

### 🚧 In Progress / Upcoming
- **Pickup & Return Logistics:** Manage rental logistics for real-world usage.  
- **Notifications:** Inform users about booking confirmations, cancellations, etc.  
- **Roles & Permissions:** Differentiate between managers, admins, and normal users.  
- **Live Chat Support:** Users can interact with company representatives in real-time.  
- **Admin Dashboard:** Admins can assign manager roles to users and monitor overall system activity.  
- **Discounts:** Apply promotional or seasonal discounts to rentals.

---

## 🚀 Getting Started  

> Since the project is already deployed at [rentdrive.eu](https://rentdrive.eu), local setup is optional.  
> If you want to run it yourself (for development or contribution):  

### Prerequisites  
- **Docker**  
- Or manually:  
  - Node.js  
  - .NET 9 SDK  
  - PostgreSQL

### Installation & Setup  

Option A: Docker (Recommended)
```bash
git clone https://github.com/Viktor-9904/RentDriveWebApp.git

cd RentDriveWebApp
docker-compose up --build -d
```

Option B: Run Client + Server Locally
```bash
# Run Backend
cd RentDrive.Backend
dotnet restore
dotnet run

# In a separate terminal, run Frontend
cd RentDrive.Frontend
npm install
npm run dev
```

---
## 👤 Test Users

The application comes with preloaded demo accounts you can use to explore the features:

- **Manager**  
  - Username: `jane.smith`  
  - Password: `Asd123`  

- **Regular Users**  
  - Username: `john.doe`  
  - Password: `Asd123`  
  - Username: `alex.miles`  
  - Password: `Asd123`
 
  ---

## 🛠 Technologies Used

- **Frontend:** ReactJS v19 (static CSS styling)  
- **Backend:** ASP.NET Core 9 Web API  
- **Database:** PostgreSQL (migrated from MS SQL Server)  
- **ORM:** Entity Framework Core  
- **Deployment:** Raspberry Pi 4B + Docker Compose + Watchtower  
- **CI/CD:** GitHub Actions (automatic build & deployment of updated Docker images)


---
 ## 📐 Project Structure and Architecture

 #### 📊 Database Schema
<p align="center">
  <img src="README/images/db_schema.png" alt="Full Project Overview" width="800"/>
</p>

---

### 🔧 Back-End Architecture
RentDrive uses a **classic monolithic 3-layer architecture**: Web, Service, and Data layers.

#### Overview of the Structure
<p align="center">
  <img src="README/images/backend_overview.png" alt="Full Project Overview" width="300"/>
</p>

#### Server Structure

**Data Layer (`RentDrive.Data`)**  
- **Models:** Entities such as `Vehicles`, `VehicleTypes`, `VehicleTypeProperties`, `VehicleTypePropertyValues`, `VehicleTypeCategories`, `VehicleImages`, `VehicleReviews`, `Wallets`, `WalletTransactions`, `Rentals`.  
- **DbContext:** `RentDriveDbContext.cs` handles database access, relationships, and migrations.  
<p align="center">
  <img src="README/images/backend_data_layer.png" alt="Data Layer Structure" width="300"/>
</p>

**Services Layer (`RentDrive.Services.Data`)**  
- **Interfaces:** Service contracts for dependency injection.  
- **Implementations:** Business logic for vehicles, rentals, wallets, and bookings.  
<p align="center">
  <img src="README/images/backend_service_layer.png" alt="Services Layer Structure" width="300"/>
</p>

**Web Layer (`RentDrive.Backend`)**  
- **Controllers:** Handles HTTP requests; separated by functionality (e.g., VehicleController, RentalController).  
- **ViewModels:** Input models for validation and mapping to services.  
- **wwwroot:** Stores static files, including vehicle images uploaded by users.  
<p align="center">
  <img src="README/images/backend_web_layer.png" alt="Web Layer Structure" width="300"/>
</p>

**Common (`RentDrive.Common`)**  
- Global constants, enums, and helper utilities shared across layers.  

#### Features
Each feature (Vehicles, Rentals, Wallets, etc.) has its own directory containing:  

**Web:**  
- Controllers: Handles user requests.  
- ViewModels: Input/output data validation.  

**Services:**  
- Interfaces + Implementations: Business logic decoupled from web layer.  

**Data:**  
- Entity models, EF configurations, seeders for initial data.


---


### 🖥 Front-End Architecture
The frontend is built in **React v19** with static CSS. Components and logic are organized by feature and functionality.

#### Overview of the Front-End
<p align="center">
  <img src="README/images/frontend_overview.png" alt="Front-End Overview" width="300"/>
</p>

#### Components
- `components/` – JSX components grouped by feature (Vehicles, Rentals, Booking Calendar, etc.)  
- `contexts/` – Contexts for global state (AccountContext, etc.)  

<p align="center">
  <img src="README/images/frontend_components.png" alt="Components Structure" width="300"/>
</p>

#### Pages
- `pages/` – Top-level page components that assemble multiple components for specific routes  

<p align="center">
  <img src="README/images/frontend_pages.png" alt="Pages Structure" width="300"/>
</p>

#### Hooks
- `hooks/` – Custom hooks that encapsulate feature logic and API calls  

<p align="center">
  <img src="README/images/frontend_hooks.png" alt="Hooks Structure" width="300"/>
</p>

<p align="center">
  <img src="README/images/frontend_hooks_vehicle.png" alt="Hooks Structure" width="300"/>
</p>

---

## 📄 License
This project is licensed under the MIT License.
