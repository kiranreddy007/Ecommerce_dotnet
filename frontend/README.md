# Ecommerce Application

This project is a full-stack ecommerce application built with a **.NET backend** and a **React frontend**.

## Prerequisites

Before starting, ensure you have **Node.js** (v16 or above) and **npm**, **.NET SDK** (v7.0 or above), and **SQLite** for database management.

## Backend Instructions

To build and run the backend, navigate to the backend folder using `cd EcommerceBackend`. Restore NuGet packages using `dotnet restore`. Apply database migrations with `dotnet ef database update`. Finally, run the backend server using `dotnet run`. The server will start and listen at `http://localhost:5126`.

## Frontend Instructions

To build and start the frontend, navigate to the frontend folder using `cd frontend`. Install dependencies with `npm install`. Start the development server using `npm run dev`. The frontend application will be available at `http://localhost:5173`.

## Application Login Details

To test the application, use the following login details:
For the Admin User: Email: `testadmin@example.com` and Password: `TestPassword123`.
For a Regular User: Email: `testuser2@example.com` and Password: `TestPassword123`.

## Deployment

the backend is hosted in azure 
http://ecommerce-app.eastus2.cloudapp.azure.com/