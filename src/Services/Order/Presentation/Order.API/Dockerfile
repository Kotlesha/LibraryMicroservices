# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 5128
EXPOSE 7018

COPY certificates /usr/local/share/ca-certificates/
RUN update-ca-certificates

USER app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Order/Presentation/Order.API/Order.API.csproj", "src/Services/Order/Presentation/Order.API/"]
COPY ["src/Services/Order/Core/Order.Application/Order.Application.csproj", "src/Services/Order/Core/Order.Application/"]
COPY ["src/Shared/Shared.CleanArchitecture/Shared.CleanArchitecture.csproj", "src/Shared/Shared.CleanArchitecture/"]
COPY ["src/Shared/Shared.Components/Shared.Components.csproj", "src/Shared/Shared.Components/"]
COPY ["src/Services/Order/Core/Order.Domain/Order.Domain.csproj", "src/Services/Order/Core/Order.Domain/"]
COPY ["src/Services/Order/Infrastructure/Order.Infrastructure/Order.Infrastructure.csproj", "src/Services/Order/Infrastructure/Order.Infrastructure/"]
RUN dotnet restore "./src/Services/Order/Presentation/Order.API/Order.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Order/Presentation/Order.API"
RUN dotnet build "./Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

WORKDIR "/src/src/Services/Order/Infrastructure/Order.Infrastructure"
RUN dotnet ef database update --project "./Order.Infrastructure.csproj"

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Order.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]