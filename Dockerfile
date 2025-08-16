# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore dependencies
COPY TestProject.sln ./
COPY TestProject.Application/TestProject.Application.csproj TestProject.Application/
COPY TestProject.Core/TestProject.Core.csproj TestProject.Core/
COPY TestProject.API/TestProject.API.csproj TestProject.API/
COPY TestProject.Infrastructure/TestProject.Infrastructure.csproj TestProject.Infrastructure/
RUN dotnet restore

# Copy the rest of the source code and build
COPY . ./
WORKDIR /src/TestProject.API
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "TestProject.API.dll"]