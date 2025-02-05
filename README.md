﻿[![CI Pipeline](https://github.com/JicoDotNet/Full-Inventory-by-Asp-Net-Azure/actions/workflows/build.yml/badge.svg)](https://github.com/JicoDotNet/Full-Inventory-by-Asp-Net-Azure/actions/workflows/build.yml)

![GitHub repo size](https://img.shields.io/github/repo-size/JicoDotNet/Full-Inventory-by-Asp-Net-Azure)
![GitHub stars](https://img.shields.io/github/stars/JicoDotNet/Full-Inventory-by-Asp-Net-Azure?style=social)
![GitHub license](https://img.shields.io/github/license/JicoDotNet/Full-Inventory-by-Asp-Net-Azure)
![Contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen)  

# 🏢 Inventory, Billing & GST System for Indian SMBs

## 📌 Description  
This is a powerful and open-source **business management software** designed specifically for **Indian Small and Medium Businesses (SMBs)**. This web-based application provides a **complete solution for inventory management, invoicing, GST compliance, stock tracking, billing, purchase-sale transactions, and business dues & outstanding calculations**.  

Built using **.NET Framework 4.8**, this software ensures a **scalable, secure, and efficient** way to manage day-to-day business operations. Whether you're a **retail shop, wholesale distributor, trader, or manufacturer**, this system helps **automate workflows, generate reports, and streamline financial transactions**.  

### ✨ Key Features  
✔ **Inventory Management** – Track stock levels, categorize products, and monitor warehouse data.  
✔ **Billing & Invoicing** – Generate professional invoices with GST calculations, discounts, and tax details.  
✔ **GST Compliance** – Auto-calculate CGST, SGST, and IGST for Indian businesses.  
✔ **Purchase & Sales Tracking** – Maintain detailed transaction history for buyers and suppliers.  
✔ **Business Dues Management** – Monitor loan payments, interest, and outstanding balances.   
✔ **Reports & Analytics** – Generate sales, expense, and tax reports for financial insights.  
✔ **Self-Hosted & Open-Source** – Deploy on your own server with full control over data.  

## 🌟 Introduction  
Managing business operations efficiently is a **major challenge for Indian SMBs**, especially with **GST compliance, inventory tracking, and financial transactions**. This **open-source, self-hosted business management solution** helps businesses streamline operations, **reduce manual work, and improve financial transparency**.  

This project is developed with **.NET Framework 4.8**, making it a **stable, reliable, and high-performance** web application. The software is designed for **scalability**, allowing businesses to adapt as they grow.  

✅ **Why Choose This WebApp?**  
- **100% Open-Source** – Free to use, modify, and distribute under **GPL-3.0 Lisence**.  
- **No Vendor Lock-in** – Self-hosted with full control over your data.  
- **Designed for Indian SMBs** – Focused on Indian GST accounting and business needs.  
- **Easy to Use** – Simple UI with a clean, intuitive dashboard.  
- **Active Community** – Open for contributions, bug fixes, and enhancements.  


## ☰ Table of Contents
- [Project Overview](#project-overview)  
  - Brief introduction  
  - Features & benefits
- [Screenshots](#screenshots-optional)  
  - UI previews (Login, Dashboard, Reports, etc.)
- [Getting Started](#getting-started)  
  - Quick setup guide  
  - Prerequisites
  - Technology Stack
  - Running the project locally
  - Deployment Guide
- [User Guide](#user-guide)  
  - How to use different modules
- [Technical Details & Know-How](#technical-details--know-how)  
  - Project architecture overview  
  - Code structure explanation  
  - Key design patterns used  
  - Authentication details
- [Contributing](#contributing)  
- [Versioning & Change log](#versioning--Change-log)  
- [License](#license)
- [Contact & Support](#contact--support)
- [Roadmap & Future Enhancements](#roadmap--future-enhancements)  
  - Project Status
  - Planned features  
  - Upcoming improvements


## 📌 Project Overview  

### 🚀 Brief Introduction  
The **Inventory, Billing & GST Management System for Indian SMBs** is a comprehensive web application designed to **streamline business operations** by managing **inventory, billing, GST compliance, and financial transactions** efficiently.  

This software is built using **.NET Framework 4.8 MVC**, providing a **robust, scalable, and user-friendly solution** for small and medium businesses. Whether you're managing **retail, wholesale, manufacturing, or trading**, this application ensures a **smooth workflow, accurate financial tracking, and compliance with Indian taxation laws**.  

This project is **fully open-source** and licensed under **GPL-3.0**, enabling businesses to self-host and customize it according to their needs **without third-party dependencies**.  

### 🚀 Features & Benefits  

#### 🔹 Business Process Automation  
- ✅ **Automates daily operations** like stock updates, invoice generation, and tax calculations.  
- ✅ Reduces **manual errors** in inventory & billing processes.  

#### 🔹 Inventory & Stock Management  
- ✅ Tracks **real-time stock levels**, preventing shortages or overstocking.  
- ✅ Supports **product categorization**, warehouse management, and bulk stock updates.  

#### 🔹 Billing & GST Compliance  
- ✅ Generates **GST-compliant invoices** with auto-calculated CGST, SGST, and IGST.  
- ✅ Supports **discounts, GST tax slabs, and custom invoice formats**.   
- ✅ **Fully Customized GST Features** – Use the software **even if you are not GST-registered**.  
- ✅ **Optional GST settings** – Enable or disable tax calculations based on business needs.  
- ✅ Supports **both GST & non-GST invoices**, making it flexible for all types of businesses.  


#### 🔹 Quotation Management (Flexible & Trackable)  
- ✅ **Easily generate quotations** for customers with detailed pricing, taxes, and discounts.  
- ✅ **Track quotation status** (Converted to Sales Order - Invoice).  
- ✅ Convert quotations to Sales Order **with a single click**, saving time.  

#### 🔹 Purchase & Sales Tracking  
- ✅ Maintains **detailed transaction history** for **vendors and customers**.  
- ✅ Generates **purchase orders, payment due reports, and expense tracking**.  

#### 🔹 Business Financial Management  
- ✅ Monitors **business dues & outstanding balances** in a structured way.  
- ✅ Helps in tracking **dues payments and business credits**.  

#### 🔹 Advanced Reporting & Analytics  
- ✅ Provides **financial insights** with **sales reports and tax summaries**.  
- ✅ Supports **custom report generation & export options (PDF, Excel, CSV)**.  

## 🖼️ Screenshots  

[Here](/docs/SCREENSHOTS.md) are some UI previews of the **Inventory, Billing & GST System** in details.

## 📌 Getting Started  

### 🔹 Quick Setup Guide    
Follow these steps to set up and run the project on your local machine.  

### 🔹 Prerequisites  
Ensure your system has the following is installed:  
- **Windows OS** (Windows 10/11 or Windows Server)  
- [**.NET Framework 4.8**](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)  
- [**MS SQL Server (2016 or later)**](https://www.microsoft.com/en-in/sql-server/sql-server-downloads)  
- **IIS Express or IIS Server**  
- [**Visual Studio 2019/2022**](https://visualstudio.microsoft.com/) (with ASP.NET development workload) or another compatible IDE   

### 🔹 Technology Stack  
This project uses:  
- **Backend:** ASP.NET MVC (.NET Framework 4.8.1)  
- **Database:** MS SQL Server, Azure Table Storage  
- **File/Blob Storage:** Azure Blob Storage  

### 🔹 Running the Project Locally  

#### 1️⃣ Set Up the Database  
- Locate SQL scripts in [`/src/JicoDotNet.SQLServer/DefaultScript`](/src/JicoDotNet.SQLServer/DefaultScript) folder.  
- Execute [`AllSchemaScript.sql`](/src/JicoDotNet.SQLServer/DefaultScript/AllSchemaScript.sql) to create database schema.  
- Execute [`BasicDefaultData.sql`](/src/JicoDotNet.SQLServer/DefaultScript/BasicDefaultData.sql) to insert default required data.  

#### 2️⃣ Build & Run the Project  
- Open the [solution](/src/JicoDotNet.Inventory.sln) in **Visual Studio**.  
- Set the startup project and build the solution.  
- Run the project – it will be available at **`http://localhost:12345/`**.  
- If port **12345** is occupied, modify the port `<IISUrl>` in the `.csproj` file.  

#### 3️⃣ Configuration File Samples  

You need to change **Configuration details** in in the [`Web.config`](/src/JicoDotNet.Inventory.UI/Web.config) file.

- For **Sql Server Database**, the connection string is stored under the key:  

```xml
<appSettings>
    <add key="SqlServerConnection" value="YOUR_CONNECTION_STRING_GOES_HERE" />
</appSettings>
```

- For **Azure Table & Blob Storage**, the connection string is stored under the key:  

> _Generate the connection string of **Azure Table Storage** from [Azure Portal](https://portal.azure.com)._

> To generate an Azure Table Storage connection string from the Azure Portal, follow these steps:  
> 1. Navigate to your **storage account** in the Azure Portal.  
> 2. In the **Security + networking** section, locate the Access keys setting.  
> 3. Click on the **Show keys** button at the top of the page to display the account keys and associated connection strings.  
> 4. Copy the **Connection String** from Key1 or Key2.  
> 5. Add this to the project's **Web.config**  

```xml
<appSettings>
    <add key="AzureStorageConnection" value="YOUR_CONNECTION_STRING_GOES_HERE" />
</appSettings>
```

### 🔹 Deployment Guide (Windows & Azure)
#### Windows Server Deployment
🔹 Publish the project from Visual Studio.   
🔹 Configure IIS to host the application.   
🔹 Ensure MS SQL Server & storage configurations are set.   

#### Azure Deployment
🔹 Deploy as an Azure Web App (Windows-based).   
🔹 Use Azure Table Storage & Blob Storage configurations.   
🔹 Ensure SQL Server is hosted on Azure or connected via Azure SQL.   

## 📖 User Guide  

For a detailed user manual, refer to [Here](/docs/USERMANUAL.md).  

## 🛠 Technical Details & Know-How  

This section provides an overview of the project's architecture, code structure, key design patterns, and authentication details.  

### 🔹 Project Architecture Overview  
The application follows a **multi-layered architecture**:  
- **Presentation Layer (UI)** – ASP.NET MVC views and controllers.  
- **Business Logic Layer (BLL)** – Service classes for processing business logic. It consists of four different projects.  
- **Data Access Layer (DAL)** – Handles database interactions with MS SQL Server and Azure Storage. Azure Storage access layer is consumed via NuGet packages.  
- **Storage & Persistence** – Uses **MS SQL Server** for structured data, **Azure Table Storage** for NoSQL data, and **Azure Blob Storage** for file storage.  

> ![Project Dependency](/docs/screenshots/JicoDotNet.Inventory.Dependency.svg)  

### 🔹 Code Structure  
- `/src/JicoDotNet.Inventory.UI/` – ASP.NET MVC frontend and controllers.  
- `/src/JicoDotNet.Inventory.BusinessLayer/` – Business logic services.  
- `/src/JicoDotNet.Inventory.Core/` – Business logic models for entities & DTOs.  
- `/src/JicoDotNet.Inventory.Helper/` – Helper classes for business logic.  
- `/src/JicoDotNet.Inventory.Logging/` – Handles audit logs, error logs & activity logs.  
- `/src/DataAccess.Sql/` – Data access layer for SQL Server.  
- `/src/JicoDotNet.SQLServer/` – SQL scripts.  
- `/docs/` – Documentation files.  

### 🔹 Key Design Patterns Used  
The project follows a **monolithic architecture** and implements the following design patterns:  
- **Repository Pattern** – Used throughout the solution in all projects to encapsulate data access logic and maintain a consistent structure.  

### 🔹 Authentication & Security  
- **User Authentication** – Implements cookie-based authentication for secure user sessions.  
- **Data Protection** – Ensures security through SQL Server and Azure Storage access policies.  


