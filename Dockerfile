FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV ASPNETCORE_URLS=http://localhost:8080

WORKDIR /app

COPY . ./

RUN dotnet tool install --tool-path /usr/local/bin dotnet-ef
RUN dotnet restore
RUN dotnet build
RUN dotnet ef database update --project MoviesProject.Infrastructure --startup-project MoviesProject.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "MoviesProject.Api.dll"]
