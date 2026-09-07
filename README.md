# 🚀 E-commerce Web Application for Online Clothing Shopping

A clean, modular full-stack application built with **.NET / C#**, featuring an **ASP.NET Core Web API** backend and a **Blazor WebAssembly** client. 
The project utilizes a dedicated Consumer service pattern for client-server communication and shared Data Transfer Objects (DTOs).

---

## 📑 Table of Contents
- [Architecture & Flow](#-architecture--flow)
- [Project Structure](#-project-structure)
- [Technologies & Libraries](#-technologies--libraries)
- [Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation & Setup](#installation--setup)
- [Port & Localhost Configuration](#-port--localhost-configuration)
- [Database Migrations](#-database-migrations)
- [Running the Application](#-running-the-application)
- [API Endpoints](#-api-endpoints)
- [License](#-license)

---

## 🏛 Architecture & Flow

The application enforces a clear end-to-end flow with decoupled responsibilities:

```
┌─────────────────────────────────────────────────────────────┐
│                 Frontend UI (.razor Pages)                  │
└──────────────────────────────┬──────────────────────────────┘
                               │ Calls Consumer Methods
                               ▼
┌─────────────────────────────────────────────────────────────┐
│              Consumer Layer (FrontEnd.Consumer)             │
│      *HttpClient wrappers calling API & handling DTOs*      │
└──────────────────────────────┬──────────────────────────────┘
                               │ HTTP Requests / JSON Payloads
                               ▼
┌─────────────────────────────────────────────────────────────┐
│                API Controllers (API Layer)                  │
│       *Extracts User Claims, Validates, Calls Service*      │
└──────────────────────────────┬──────────────────────────────┘
                               │ Passes DTOs & Parameters
                               ▼
┌─────────────────────────────────────────────────────────────┐
│             Business Services (Infrastructure)              │
│  *Applies logic (Discounts, Stock, Sync), Maps Entity <-> DTO*
└──────────────────────────────┬──────────────────────────────┘
                               │ Queries / Persists Entities
                               ▼
┌─────────────────────────────────────────────────────────────┐
│             Repositories (Infrastructure)                   │
│         *Direct DbContext Access working on Entities*       │
└──────────────────────────────┬──────────────────────────────┘
                               │ EF Core
                               ▼
┌─────────────────────────────────────────────────────────────┐
│              Database & Domain (SQL Server)                 │
└─────────────────────────────────────────────────────────────┘
```

### 🔄 Layer Responsibilities:
- **Domain:** Pure business models and entities (`Cart`, `CartItem`, `Product`, `ProductVariant`).
- **Shared:** Shared request/response contracts (`CartDto`, `CartItemDto`, `UpdateCartItemDto`, `LocalCartItemDto`) referenced by both client and server.
- **Infrastructure:**
  - **Repositories (`IRepository` / `Repository`):** Directly query the `DbContext` and operate strictly with Domain Entities.
  - **Services (`IService` / `Service`):** Orchestrate business workflows (e.g., dynamic discount checks, stock verification, local-to-remote cart syncing), communicate with Repositories, and map Entities to/from Shared DTOs.
- **API:** RESTful endpoints (`CartController`) that authenticate users via JWT claims, delegate logic to Services, and return HTTP status responses.
- **Application (FrontEnd):**
  - **Consumer Layer (`CartConsumer`):** Encapsulates `HttpClient` logic, handles serialization/deserialization, and isolates UI components from raw HTTP requests.
  - **Pages & Components:** Razor components consume the Consumer layer to render data.

---

## 📁 Project Structure

```bash
SolutionName/
├── src/
│   ├── FrontEnd/ (or SolutionName.Application)
│   │   ├── Consumer/                    # API Client consumers (e.g., CartConsumer)
│   │   ├── Pages/                       # Blazor Razor views (.razor)
│   │   ├── Shared/                      # Layouts and reusable UI components
│   │   ├── wwwroot/                     # Static assets (CSS, JS, images)
│   │   └── Program.cs                   # Blazor WASM startup & Consumer DI registration
│   │
│   ├── API/
│   │   ├── Controllers/                 # REST Controllers (e.g., CartController)
│   │   ├── Properties/
│   │   │   └── launchSettings.json      # Port and local URL configurations
│   │   ├── appsettings.json             # Connection strings & JWT configurations
│   │   └── Program.cs                   # API pipeline, CORS, and Service registrations
│   │
│   ├── Infrastructure/
│   │   ├── Data/                        # EF Core DbContext & configurations
│   │   ├── Migrations/                  # EF Core database migrations
│   │   ├── IRepository/ & Repository/   # Entity-level repository contracts and queries
│   │   └── IService/ & Service/         # Business services (e.g., CartService)
│   │
│   ├── Domain/
│   │   ├── Entities/                    # Database models (Cart, CartItem, Product, etc.)
│   │   └── Enums/                       # Domain enums
│   │
│   └── Shared/
│       ├── DTOs/                        # Shared contracts (CartDto, UpdateCartItemDto, etc.)
│       └── Constants/                   # Global constants and role definitions
│
└── SolutionName.sln
```

---

## 🛠 Technologies & Libraries

- **Frontend:** Blazor WebAssembly (.NET), `System.Net.Http.Json`
- **Backend API:** ASP.NET Core Web API (.NET)
- **Database & ORM:** SQL Server & Entity Framework Core
- **Shared Layer:** .NET Class Library for DTO sharing
- **Authentication:** JWT Bearer Authentication (`User` role claims)
- **API Documentation:** Swagger / OpenAPI

---

## ⚙️ Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (matching your project version)
- [SQL Server](https://www.microsoft.com/sql-server) / LocalDB
- [Visual Studio 2022](https://visualstudio.microsoft.com/)

---

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/your-repo-name.git
   cd your-repo-name
   ```

2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

3. **Configure Database Connection:**
   Open `API/appsettings.json` and adjust the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=YourDbName;Trusted_Connection=True;TrustServerCertificate=True;"
     },
     "JwtSettings": {
       "Key": "YourSecureKeyHereMakeSureItIsLongEnough",
       "Issuer": "YourApp",
       "Audience": "YourAppAudience"
     },
   }
   ```

---

## 🔌 Port & Localhost Configuration

To allow the Blazor Consumer layer to reach the API endpoints without network or CORS issues:

### 1. Verify API Port (`API`)
Check the assigned HTTPS port in `API/Properties/launchSettings.json`:
```json
"https": {
  "commandName": "Project",
  "applicationUrl": "https://localhost:7123;http://localhost:5123"
}
```

### 2. Configure Consumer `HttpClient` in Blazor (`FrontEnd`)
In `FrontEnd/Program.cs`, ensure the `HttpClient` base address matches the API port, and register the Consumer services:
```csharp
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("https://localhost:7123/") // Must match API HTTPS URL
});

// Register Consumers
builder.Services.AddScoped<CartConsumer>();
```

### 3. Check CORS Policy (`API`)
In `API/Program.cs`, ensure requests from the Blazor client are permitted:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7001", "http://localhost:5001") // Blazor client port
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

app.UseCors("AllowBlazorClient");
```

---

## 🗄️ Database Migrations

Apply database schema changes:

### Package Manager Console (Visual Studio):
```powershell
# Default project: Infrastructure, Startup project: API
Update-Database
```

### .NET CLI:
```bash
dotnet ef database update --project src/Infrastructure --startup-project src/API
```

---

## ▶️ Running the Application

### Visual Studio (Multiple Startup Projects):
1. Right-click the **Solution** -> Select **Configure Startup Projects...**.
2. Select **Multiple startup projects**:
   - Set `API` to **Start**.
   - Set `FrontEnd` to **Start**.
3. Press `F5` to run both services.

---

## 📑 Core Cart Endpoints

| Method | Endpoint | Description | Auth Required |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/cart` | Get current user cart with calculated discounts | Yes (`User`) |
| `POST` | `/api/cart?productVariantId={id}&quantity={qty}` | Add an item variant to cart | Yes (`User`) |
| `PUT` | `/api/cart/item` | Update item quantity in cart | Yes (`User`) |
| `DELETE` | `/api/cart` | Clear entire cart | Yes (`User`) |
| `DELETE` | `/api/cart/item/{cartItemId}` | Remove single item from cart | Yes (`User`) |
| `POST` | `/api/cart/sync` | Sync local guest items to user cart on login | Yes (`User`) |

---
