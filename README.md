# AuthenticationAPI

This project is a clean, secure **Authentication Microservice** built using **.NET 8 Web API**, following **Onion Architecture** and the **Repository Pattern**. It supports user registration, login with JWT token issuance, and middleware-based token validation.

---

## Features

- **User Registration** with secure password hashing (via ASP.NET Identity)
- **User Login** with JWT token issuance
- **Token Validation Middleware** to protect APIs
- **Onion Architecture** for clean separation of concerns
- **Repository Pattern** for testable and abstracted data access
- **Dependency Injection** throughout the application
- Built with **.NET 8** and **ASP.NET Core Web API**

---
## Onion Architecture Overview
Onion Architecture in .NET 8 offers a thoughtful way to organize your code by putting what matters most—your core business rules—right at the heart of the application. Around that core, you build clean, logical layers like Application, Infrastructure, and Presentation, each one depending only on the layer beneath it. This setup makes your code easier to test, easier to maintain, and far more flexible when changes come. With .NET 8's modern tools like minimal APIs, built-in dependency injection, and streamlined service registration, Onion Architecture fits naturally, helping you build robust and scalable microservices without tying your core logic to any specific database, UI, or external tool.
This project contains the following layers: 
- **Presentation Layer (Auth.API)** : ASP.NET Web API (controllers)
- **Application Layer (Auth.Application)** : Services, DTOs, Interfaces
- **Domain Layer (Auth.Domain)**:Entities, Interfaces (pure business logic)
- **Infrastructure Layer(Auth.Infrastructure)**: EF Core, ASP.NET Identity, JWT logic
---
## API Endpoints
| Method | Route              | Description                    |
| ------ | ------------------ | ------------------------------ |
| POST   | /api/v1/userauth/register | Register new user              |
| POST   | /api/v1/userauth/login    | Login and receive JWT          |
| |

---
## Security
- **Passwords are securely hashed using BCrypt Algorithm to overide the default ASP.NET Identity Password Harsher**
- **JWT tokens are signed with HMAC-SHA256 and have expiration**
- **Endpoints are protected using [Authorize] middleware**

## Testing
- **Use Swagger UI or Postman to register/login users**

---
## Technologies Used
- **.NET 8**
- **ASP.NET Core Web API**
- **ASP.NET Identity**
- **JWT Authentication**
- **Entity Framework Core**
- **Onion Architecture**
- **Dependency Injection**
- **BCypt Hash Algorithm**
- **Repository Pattern**
