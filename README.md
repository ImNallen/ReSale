# ReSale

ReSale is a comprehensive Enterprise Resource Planning (ERP) system designed to manage various aspects of any business efficiently. It is still in development and not all features are implemented. It is mostly used for learning purposes and to try out new technologies.

## Key Features

- Robust Authentication and Authorization
- Advanced Logging with Serilog
- High-performance Caching using Redis
- Containerization with Docker and Docker Compose
- Database Management with Entity Framework Core (Code First Approach, PostgreSQL)
- CQRS pattern implementation using MediatR
- Efficient object mapping with Mapster
- Input validation using FluentValidation
- Clean Architecture for maintainable and scalable code
- RESTful API with Minimal API Endpoints
- Detailed API documentation using Swagger
- Email functionality with FluentEmail (Papercut for development)
- Comprehensive User Management
- Customer Relationship Management
- Product Catalog and Inventory Management
- Order Processing and Management
- Integrated Messaging System

## Prerequisites

- .NET 8 SDK
- Node.js and npm (for Tailwind CSS)
- Docker and Docker Compose (recommended for streamlined setup)

## Getting Started

### Setting up the API

1. Navigate to the `ReSale.Api` directory
2. Run `docker compose up --build` to start the API and related services
3. Database migrations are automatically applied during startup in the Development environment
4. Access Swagger documentation at `http://localhost:5000/swagger/index.html` or `https://localhost:5001/swagger/index.html`

### Running the Web Application

1. Navigate to the `ReSale.Web` directory
2. For real-time Tailwind CSS compilation, run:
   ```
   npx tailwindcss -i tailwind.css -o wwwroot/css/tailwind.css --watch
   ```
3. To launch the Web App, execute `dotnet run` or `dotnet watch run` for hot reloading
4. Access the Web App at `http://localhost:5002/` or `https://localhost:5003/`

### Development Tools

- **Papercut**: Email testing tool accessible at `http://localhost:8080/`
- **SEQ**: Centralized logging platform available at `http://localhost:8081/`

## Project Structure

- `ReSale.Api`: Core backend API built with ASP.NET Core
- `ReSale.Api.Contracts`: Shared contracts for API communication
- `ReSale.Web`: Frontend Blazor WebAssembly application
- `ReSale.Domain`: Domain entities and core business logic
- `ReSale.Application`: Application layer implementing CQRS pattern
- `ReSale.Infrastructure`: Data access and external service integrations

## Technology Stack

- [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0): Modern, cross-platform framework for building cloud-based applications
- [Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-8.0): Client-side web UI framework
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/): Modern object-database mapper for .NET
- [Tailwind CSS](https://tailwindcss.com/): Utility-first CSS framework for rapid UI development
- [MediatR](https://github.com/jbogard/MediatR): Simple mediator implementation in .NET
- [Mapster](https://github.com/MapsterMapper/Mapster): Fast, flexible object-object mapper
- [FluentValidation](https://github.com/FluentValidation/FluentValidation): .NET validation library for building strongly-typed validation rules
- [FluentEmail](https://github.com/JeremySkinner/FluentEmail): Email sending library for .NET
- [Papercut](https://github.com/ChangemakerStudios/Papercut): Simplified SMTP server for testing email functionality
- [SEQ](https://datalust.co/seq): Centralized logging server with powerful querying capabilities
- [Serilog](https://serilog.net/): Flexible, structured logging for .NET applications
- [Redis](https://redis.io/): In-memory data structure store used for caching
- [PostgreSQL](https://www.postgresql.org/): Advanced, open-source relational database

## License

This project is open-source and available for use without restrictions. If you find it helpful, learn from it, or use it in any interesting projects, we'd love to hear about it! Your feedback and experiences are valuable to us.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

If you encounter any issues or have questions, please open an issue on the GitHub repository.
