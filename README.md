# CLEAN

### Exercise: To-Do List API

An application with the following layers:  
- Entities (Domain Layer) → Pure business logic, independent of frameworks.   
- Use Cases (Application Layer) → Orchestrates business logic and defines application behavior.   
- Adapters (Interface Layer) → Controllers, presenters, and UI elements.   
- Infrastructure (Data Layer) → External services (database, APIs, frameworks).  

```
./
│   ├── LICENSE
│   ├── README.md
│   ├── .gitignore
│   ├── TodoApp
│   │   ├── TodoApp.Domain                           # Entities and Interfaces
│   │   │   ├── TodoApp.Domain.csproj
│   │   │   ├── Entities
│   │   │   │   ├── TodoItem.cs
│   │   │   ├── Interfaces
│   │   │   │   ├── TodoRepository.cs
│   │   ├── TodoApp.Infrastructure                   # Data Access, External Services
│   │   │   ├── Repositories
│   │   │   │   ├── TodoRepository.cs
│   │   │   ├── TodoApp.Infrastructure.csproj
│   │   ├── TodoApp.Application                      # Business Logic (Use Cases)
│   │   │   ├── TodoApp.Application.csproj
│   │   │   ├── Services
│   │   │   │   ├── TodoService.cs
│   │   ├── TodoApp.Api                              # Presentation Layer (Controllers)
│   │   │   ├── appsettings.json
│   │   │   ├── TodoApp.Api.csproj
│   │   │   ├── Properties
│   │   │   │   ├── launchSettings.json
│   │   │   ├── appsettings.Development.json
│   │   │   ├── Controllers
│   │   │   │   ├── TodoController.cs
│   │   │   ├── Program.cs
│   │   ├── TodoApp.Tests                           # Unit & Integration Tests
│   │   │   ├── TodoApp.Tests.csproj
│   │   │   ├── TodoRepositoryTests.cs
│   │   │   ├── TodoServiceTests.cs
```

### Test

```bash
cd TodoApp/TodoApp.Tests
dotnet test
```

### Run    

```bash
cd TodoApp/TodoApp.Api
dotnet run
```

### View

`http://localhost:5205/swagger`