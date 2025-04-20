# JobSphere API

JobSphere is a powerful and scalable job board API built using **ASP.NET Core Web API**, designed to streamline the process of posting jobs, browsing listings, and managing applications. It adopts a **clean architecture** pattern to ensure separation of concerns, maintainability, and testability.

## 🚀 Project Overview
JobSphere is intended to serve as the backend for a modern job board platform where employers can post job offers and job seekers can apply, optionally uploading their CVs. The API implements robust user management, secure authentication and authorization, and flexible job tagging.

## ✨ Features
- **User Management**
  - Register & login functionality
  - Role-based accounts: `Employer`, `JobSeeker`
- **Job Management**
  - Employers can post, update, and delete job offers
  - Users can browse all job postings
- **Job Application**
  - Job Seekers can apply to jobs
  - Optional file upload for CVs
- **Tagging System**
  - Categorize job posts by tags (e.g., Engineering, Marketing, Remote)
- **Authentication & Authorization**
  - Secure JWT-based authentication
  - Role-based access control
- **Developer Tools**
  - Swagger UI for API exploration
  - RESTful API structure

## 🛠 Technologies Used
- ASP.NET Core Web API (.NET 8+)
- Entity Framework Core
- SQL Server
- AutoMapper
- Swagger (Swashbuckle)
- JWT Authentication
- FluentValidation
- Clean Architecture Principles

## 🧱 Architecture Overview
```
JobSphere.Api           --> Entry point (Controllers, Swagger, DI setup)
JobSphere.Application   --> Business logic, DTOs, Interfaces
JobSphere.Domain        --> Core entities, enums, interfaces
JobSphere.Infrastructure--> Data access (EF Core), Repository implementations
```

## 🧰 Getting Started
### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- Visual Studio or VS Code

### Installation
1. Clone the repository
```bash
git clone git clone https://github.com/your-username/JobSphere.git
cd JobSphere
```
2. Update `appsettings.json` with your local SQL Server connection string

3. Apply database migrations:
```bash
dotnet ef database update --project JobSphere.Infrastructure
```

4. Run the API
```bash
dotnet run --project JobSphere.Api
```

## 📚 API Documentation
After starting the API, navigate to:
```
https://localhost:{port}/swagger
```
You will see interactive API documentation via Swagger UI where you can test endpoints.

## 📁 Folder Structure
```
JobSphere.Api
JobSphere.Application
JobSphere.Domain
JobSphere.Infrastructure
JobSphere.Tests (optional)
```

## 🔐 Environment Variables
Configure these in `appsettings.Development.json` or in your environment:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=JobSphereDb;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "your-secure-jwt-secret-key",
    "Issuer": "JobSphereApi",
    "Audience": "JobSphereUsers",
    "DurationInMinutes": 60
  }
}
```

## 🧱 Database Migrations
To add a migration:
```bash
dotnet ef migrations add MigrationName --project JobSphere.Infrastructure
```
To apply migrations:
```bash
dotnet ef database update --project JobSphere.Infrastructure
```

## ▶️ Running the API
```bash
dotnet run --project JobSphere.Api
```
Then visit `https://localhost:{port}/swagger` in your browser.

## 🤝 Contribution Guidelines
1. Fork the repo
2. Create a feature branch (`git checkout -b feature/xyz`)
3. Commit your changes with clear messages
4. Push to your branch
5. Open a Pull Request 🚀

## 📄 License
This project is licensed under the [MIT License](LICENSE).

