# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 5218
EXPOSE 7270

COPY certificates /usr/local/share/ca-certificates/
RUN update-ca-certificates

USER app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/User/Presentation/User.API/User.API.csproj", "src/Services/User/Presentation/User.API/"]
COPY ["src/Shared/Shared.Components/Shared.Components.csproj", "src/Shared/Shared.Components/"]
COPY ["src/Services/User/Core/User.Application/User.Application.csproj", "src/Services/User/Core/User.Application/"]
COPY ["src/Services/User/Core/User.Domain/User.Domain.csproj", "src/Services/User/Core/User.Domain/"]
COPY ["src/Shared/Shared.CleanArchitecture/Shared.CleanArchitecture.csproj", "src/Shared/Shared.CleanArchitecture/"]
COPY ["src/Services/User/Infrastructure/User.Infrastructure/User.Infrastructure.csproj", "src/Services/User/Infrastructure/User.Infrastructure/"]
RUN dotnet restore "./src/Services/User/Presentation/User.API/User.API.csproj"
COPY . . 
WORKDIR "/src/src/Services/User/Presentation/User.API"
RUN dotnet build "./User.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./User.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.API.dll"]

ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet User.API.dll"]