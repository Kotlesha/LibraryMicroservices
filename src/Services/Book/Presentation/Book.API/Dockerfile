# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5073
EXPOSE 7120


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Book/Presentation/Book.API/Book.API.csproj", "src/Services/Book/Presentation/Book.API/"]
COPY ["src/Services/Book/Core/Book.Application/Book.Application.csproj", "src/Services/Book/Core/Book.Application/"]
COPY ["src/Services/Book/Core/Book.Domain/Book.Domain.csproj", "src/Services/Book/Core/Book.Domain/"]
COPY ["src/Shared/Shared.CleanArchitecture/Shared.CleanArchitecture.csproj", "src/Shared/Shared.CleanArchitecture/"]
COPY ["src/Shared/Shared.Components/Shared.Components.csproj", "src/Shared/Shared.Components/"]
COPY ["src/Services/Book/Infrastructure/Book.Infrastructure/Book.Infrastructure.csproj", "src/Services/Book/Infrastructure/Book.Infrastructure/"]
RUN dotnet restore "./src/Services/Book/Presentation/Book.API/Book.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Book/Presentation/Book.API"
RUN dotnet build "./Book.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

WORKDIR "/src/src/Services/Book/Infrastructure/Book.Infrastructure"
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef database update --project "./Book.Infrastructure.csproj"

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Book.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Book.API.dll"]