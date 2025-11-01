<p align="center">
<img style="width: 10%" src="https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff" alt=".NET 8">
<img style="width: 15%" src="https://img.shields.io/badge/Postgres-%23316192.svg?logo=postgresql&logoColor=white" alt="PostgreSQL">
<img style="width: 11%" src="https://img.shields.io/badge/Redis-%23DD0031.svg?logo=redis&logoColor=white" alt="Redis">
<img style="width: 12%" src="https://img.shields.io/badge/Strapi-%232E7EEA.svg?logo=strapi&logoColor=white" alt="Strapi">
<img style="width: 15%" src="https://img.shields.io/badge/-rabbitmq-%23FF6600?style=flat&logo=rabbitmq&logoColor=white" alt="RabbitMQ">
</p>

<h1 align="center">TeamManager API</h1>

<p align="center" style="font-weight: bold;">
A robust, scalable, and secure API for team management, built on modern .NET 8 with Clean Architecture and Domain-Driven Design principles.
</p>

## 🚀 About This Project

This project is the backend API for TeamManager, a platform designed to facilitate team organization, member management, and role assignments. It is built to be highly performant, testable, and maintainable, serving as a reference implementation of Clean Architecture in .NET.

A key feature of this application is its Hybrid Role-Based Access Control (RBAC), which allows for both global roles (like SystemAdmin) and team-specific roles (like TeamLeader), providing granular security down to the team level.

The application also integrates with Strapi as a headless CMS for managing external content, such as email templates.

## 🛠️ Tech Stack & Architecture

Core Technologies

.NET 8: The core runtime and framework.

ASP.NET Core: For Minimal APIs (Endpoints).

Entity Framework Core 8: For data access and persistence (PostgreSQL).

ASP.NET Identity: For user authentication.

PostgreSQL: The primary relational database.

Redis: For distributed caching and refresh token storage.

RabbitMQ: For asynchronous messaging (e.g., sending email notifications).

Strapi: (External dependency) Used as a headless CMS to fetch email templates.

Docker & Docker Compose: For containerized development and deployment.

Architectural Principles

The solution is strictly divided into four main layers, following the Clean Architecture dependency rule (all dependencies point inwards to the Domain).

### <u>TeamManager.Domain:</u>

Contains the core business logic, entities, and domain errors.

Defines the abstractions (interfaces) for repositories and providers (IUnitOfWork, IMemberRepository, ITokenProvider).

This layer is "pure"—it has zero dependencies on other layers or infrastructure.

### <u>TeamManager.Application:</u>

Contains the application logic and use cases (following a Vertical Slice pattern).

Defines the IUseCase handlers (similar to CQRS) that orchestrate the domain logic.

Depends only on the Domain layer.

### <u>TeamManager.Infrastructure:</u>

Contains the concrete implementations of the domain abstractions.

Includes Persistence (EF Core DbContext, Repositories), Providers (TokenProvider, ServiceBus), and external service clients.

Depends on Application (to implement interfaces) and Domain.

### <u>TeamManager.API:</u>

The presentation layer, built with Minimal APIs.

Handles HTTP requests, validation, and authorization.

Orchestrates calls to the Application layer (Use Cases).

Depends only on Application and Infrastructure (for DI setup).

## 🏁 Getting Started (Running with Docker)

This is the fastest and recommended way to run the entire application stack (API, PostgreSQL, Redis, RabbitMQ) locally.

### Prerequisites

<b>Docker Desktop installed and running.</b>

1. Create your Environment File

The application requires specific secrets (like the Admin password and JWT secret) to run. These are loaded from a .env file that is not committed to the repository.

Create a new file named .env in the root of the solution (at the same level as docker-compose.yaml) by copying the example file.

In the root solution directory
```bash
cp .env.example .env
```

Now, open the new .env file and fill in the values. The database seeder will use these to create the first <strong>SystemAdmin</strong> user on startup.

2. Build and Run the Containers

Run the following command from the root of the solution:

docker-compose up --build


This will:

Build the TeamManager.API Docker image.

Start containers for the API, PostgreSQL, Redis, and RabbitMQ.

Run the database migrations.

Run the DatabaseSeeder, which automatically creates the SystemAdmin user and roles using the values from your .env file.

The API will be available at http://localhost:8080.
You can access the HealthChecks UI at http://localhost:8080/api/healthchecks-ui.

## 🔐 Security Model

The API uses a sophisticated hybrid RBAC model:

Authentication: Handled by ASP.NET Identity with JWTs (Access Tokens) and Refresh Tokens (stored in the database via UserManager).

Authorization: Managed by a custom Policy-based system.

Global Roles: (e.g., SystemAdmin) Stored in AspNetRoles and provide universal permissions.

Team Roles: (e.g., TeamLeader, TeamMember) Stored in the UserTeams table and are converted into custom team_role claims (e.g., "1234-abcd:TeamLeader").

Custom Policies: Resource-based policies like CanManageTeam are used to verify if a user is either a SystemAdmin OR has the correct role for a specific team.