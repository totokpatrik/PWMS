# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PWMS.Presentation.Starter/PWMS.Presentation.Starter.csproj", "src/PWMS.Presentation.Starter/"]
COPY ["src/PWMS.Presentation.Rest/PWMS.Presentation.Rest.csproj", "src/PWMS.Presentation.Rest/"]
COPY ["src/PWMS.Application/PWMS.Application.csproj", "src/PWMS.Application/"]
COPY ["src/PWMS.Common/PWMS.Common.csproj", "src/PWMS.Common/"]
COPY ["src/PWMS.Domain/PWMS.Domain.csproj", "src/PWMS.Domain/"]
COPY ["src/PWMS.Infrastructure.Core/PWMS.Infrastructure.Core.csproj", "src/PWMS.Infrastructure.Core/"]
COPY ["src/PWMS.Persistence.PortgreSQL/PWMS.Persistence.PortgreSQL.csproj", "src/PWMS.Persistence.PortgreSQL/"]
RUN dotnet restore "./src/PWMS.Presentation.Starter/PWMS.Presentation.Starter.csproj"
COPY . .
WORKDIR "/src/src/PWMS.Presentation.Starter"
RUN dotnet build "./PWMS.Presentation.Starter.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PWMS.Presentation.Starter.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PWMS.Presentation.Starter.dll"]