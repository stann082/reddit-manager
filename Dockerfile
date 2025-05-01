# Use the official ASP.NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files first (for better Docker layer caching)
COPY src/web/*.csproj ./web/
COPY src/lib/*.csproj ./lib/

# Restore dependencies
RUN dotnet restore ./web/web.csproj

# Now copy the full source (only what’s needed)
COPY src/web/ ./web/
COPY src/lib/ ./lib/

# Build
WORKDIR /src/web
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY data /app/data
ENTRYPOINT ["dotnet", "web.dll"]

