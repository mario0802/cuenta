#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Cuenta/Account.API/Account.API.csproj", "Services/Cuenta/Account.API/"]
COPY ["Services/Cuenta/Account.Application/Account.Application.csproj", "Services/Cuenta/Account.Application/"]
COPY ["Services/Cuenta/Account.Domain/Account.Domain.csproj", "Services/Cuenta/Account.Domain/"]
COPY ["BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/BuildingBlocks/"]
COPY ["Services/Cuenta/Account.Infrastructure/Account.Infrastructure.csproj", "Services/Cuenta/Account.Infrastructure/"]
RUN dotnet restore "./Services/Cuenta/Account.API/Account.API.csproj"
COPY . .
WORKDIR "/src/Services/Cuenta/Account.API"
RUN dotnet build "./Account.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Account.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.API.dll"]