:: Setting up Entity Core
dotnet ef database update --project MoviesProject.Infrastructure --startup-project MoviesProject.Api

:: Build & Restore
dotnet restore MoviesProject.sln
dotnet build MoviesProject.sln

:: Publish & Run
cd MoviesProject.Api
dotnet publish -c Release -o publish
cd publish
set ASPNETCORE_URLS=http://localhost:5050
dotnet MoviesProject.Api.dll
