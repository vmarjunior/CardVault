CardVault API (WIP)

CardVault API is a backend service for managing trading card collections and decks. It's currently a work in progress, and I'm mostly using it as a practical playground to implement Domain-Driven Design (DDD) and Clean Architecture patterns in .NET 10.



Tech Stack

Framework: .NET 10 (C# 13)

Database: PostgreSQL via Entity Framework Core

Auth: JWT (JSON Web Tokens)

Testing: xUnit



Current State

It's still under construction, but here is what is working right now:



\[x] Core domain modeling (Users, Decks, Cards) with encapsulated logic.

\[x] User registration, authentication, and profile updates via JWT.

\[x] Automated data seeding for development (loads test users, cards, and decks from JSON files).

\[x] Swagger UI with Bearer token support.

\[x] Unit tests for the User domain rules.



How to run it locally

1\. Prerequisites

You'll need the .NET 10 SDK and a local instance of PostgreSQL running.



2\. Configuration

Clone the repo and update the appsettings.Development.json file inside the API project with your database credentials and a secret key for JWT:



JSON

"ConnectionStrings": {

&#x20; "DefaultConnection": "Host=localhost;Port=5432;Database=CardVaultDB;Username=postgres;Password=yourpassword"

},

"Jwt": {

&#x20; "Key": "your-super-secret-key-with-at-least-32-characters!",

&#x20; "Issuer": "cardvault-dev",

&#x20; "Audience": "cardvault-users"

}



3\. Database Setup

Open your terminal at the root of the project and apply the Entity Framework migrations to create the database:



Bash

dotnet ef database update -p Infrastructure/CardVault.Infrastructure.csproj -s API/CardVault.API.csproj

4\. Run

Navigate to the API folder and start the project:



Bash

cd API

dotnet run

Once it's running, go to https://localhost:xxxx/swagger in your browser. Because the database automatically seeds data in the development environment, you can immediately log in with one of the test accounts to grab a Bearer token and try out the secured endpoints.

