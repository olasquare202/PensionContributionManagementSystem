# EPS+ Pension Contribution Management System

## Project Overview
A backend system to manage customer onboarding, contributions, and benefits.

## Tech Stack
- .NET 9
- Entity Framework Core
- SQL Server
- Hangfire (for background jobs)     NOT IMPLEMENTED  
- Clean Architecture & DDD

## How to Run
1. Clone the repo
2. Update the connection string in `appsettings.json`
3. Run database migrations: 'add-migration NAME' then Run the App(auto Update database is configured)
4. Run the API

## API Endpoints for Member Management
- POST `/api/MemberManagement/RegisterMember`
- GET `/api/MemberManagement/GetAllMember`
- GET `/api/MemberManagement/{id}`
- PUT `/api/MemberManagement/UpdateMemberDetails`
- DELETE ``/api/MemberManagement/SoftDelete`

## API Endpoints for Contribution
- POST `/api/Contribution/ProcessContribution`
- GET `/api/Contribution/{id}`
- POST `/api/Contribution/GetMemberStatement`

## Background Jobs
- Hangfire Dashboard available at `/hangfire`    NOTE: (I didn't implement this)
