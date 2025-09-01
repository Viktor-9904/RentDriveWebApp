# Base image (runtime)
FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM --platform=linux/arm64 mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RentDrive.Backend/RentDrive.Backend.csproj", "RentDrive.Backend/"]
COPY ["RentDrive.Data/RentDrive.Data.csproj", "RentDrive.Data/"]
COPY ["RentDrive.Common/RentDrive.Common.csproj", "RentDrive.Common/"]
COPY ["RentDrive.Data.Models/RentDrive.Data.Models.csproj", "RentDrive.Data.Models/"]
COPY ["RentDrive.Services.Data/RentDrive.Services.Data.csproj", "RentDrive.Services.Data/"]
COPY ["RentDrive.Web.ViewModels/RentDrive.Web.ViewModels.csproj", "RentDrive.Web.ViewModels/"]
RUN dotnet restore "./RentDrive.Backend/RentDrive.Backend.csproj"
COPY . .
WORKDIR "/src/RentDrive.Backend"
RUN dotnet build "./RentDrive.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RentDrive.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentDrive.Backend.dll"]
