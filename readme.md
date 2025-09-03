// Initial project
dotnet new webapi --name movies_api
dotnet run --project movies_api/movies_api.csproj


// DB
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet tool install --global dotnet-ef
dotnet ef migrations add MovieMigration
dotnet ef database update