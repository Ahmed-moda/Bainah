Bainah_full_fixed

This package contains a scaffolded solution for Clean Architecture with Identity + JWT, GenericRepository, DataResponse wrapper, AdminBFF, PublicBFF and WebPage/PageSection example.

Steps to run:
1. Open the solution in Visual Studio or use dotnet CLI.
2. Update src/CoreApi/appsettings.Development.json connection string if needed (default uses local SQL Express).
3. From src/CoreApi run EF migrations to create Identity tables and domain tables, or run the InitialCreate.sql in src/Infrastructure/Persistence/Migrations to create demo domain tables.
4. Run CoreApi, then AdminBFF and PublicBFF.

Seeded admin credentials: admin@bainah.local / Admin123!
