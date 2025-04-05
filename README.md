JobSphere is a RESTful API for a job board platform where users can register as **Admins**, **Employers**, or **Jobseekers**. The API supports features like user authentication, role-based access control (RBAC), job post management, ownership policies, and secure data updates.

---

## 🚀 Technologies Used

- **C#** / **ASP.NET Core 8**
- **Entity Framework Core**
- **SQL Server** / **SQLite**
- **JWT Authentication**
- **Role-Based Authorization + Policies**
- **AutoMapper**
- **Swagger**
- **Postman** (for API testing)

---

## 🔐 Features

- ✅ User Registration & Login with JWT
- ✅ Roles: Admin | Employer | Jobseeker
- ✅ Secure endpoints using `Authorize` + Role policies
- ✅ Employers can create, update, and delete their job posts
- ✅ Ownership Policy: Users can only update their own data
- ✅ Admins can manage all users and jobs
- ✅ Seeding default tags (e.g., Technology, Design, Marketing)
- ✅ Global error handling + validation messages

---

## 📦 Project Structure


JobSphereAPI/
│
├── Controllers/            # API Endpoints
├── Models/                 # Entity Models
├── DTOs/                   # Data Transfer Objects
├── Services/               # Business Logic
├── Repositories/           # Data Access Layer
├── Policies/               # Custom Authorization Policies
├── Data/                   # DbContext and Seed Data
├── Mappings/               # AutoMapper Profiles
└── Program.cs              # Application Configuration
🔧 Setup Instructions
Clone the Repository


`git clone https://github.com/yourusername/jobsphere-api.git` 
`cd jobsphere-api`
Update DB Connection String

In appsettings.json:

`"ConnectionStrings": {
  "DefaultConnection": "Your_SQL_Server_or_SQLite_Connection_String"
}` 
Apply Migrations


`dotnet ef database update` 
Run the API

`dotnet run`
Visit https://localhost:{PORT}/swagger to explore the API via Swagger UI.

🔐 Authorization & Policies
JWT is used for authentication.

Use the Authorization header in your requests:


`Authorization: Bearer your_token_here` 
Custom Policies

UserOwnershipPolicy: Only the owner can update their data.

Role-based routes for Admin, Employer, and Jobseeker.

📘 Sample API Routes
Method	Endpoint	Description	Roles
POST	/api/auth/register	Register a new user	Public
POST	/api/auth/login	Login and get token	Public
GET	/api/jobs	Get all jobs	All Users
POST	/api/jobs	Create job	Employer
PUT	/api/user/{id}	Update user info (Ownership only)	Logged Users
DELETE	/api/jobs/{id}	Delete job (Owner or Admin)	Employer
🏷️ Seeded Tags
The API seeds these commonly used job tags:

Technology

Programming

Marketing

Design

Finance

Healthcare

Remote

Full-Time

Part-Time

Internship

👤 Author
Hamza Ait Aissa
Aspiring .NET Backend Developer
🇲🇦 Based in Morocco
🌐 [Portfolio Coming Soon]

🌟 Want to Contribute?
If you're a recruiter or dev who wants to collaborate or provide feedback, feel free to open issues or reach out!

📌 TODO
 File Upload for Company Logos

 Add Pagination and Filtering

 Add Comments/Applications Feature

 Frontend in Next.js

📄 License
This project is open-source and available under the MIT License.


---

Would you like me to:
- Help you write the README for your **Blog API** too?
- Polish this with badges (build passing, license, etc.)?
- Push this directly to your GitHub repo (I’ll guide you how)?

Let me know!
