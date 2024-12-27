# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 5105
EXPOSE 7073

COPY certificates /usr/local/share/ca-certificates/
RUN update-ca-certificates

USER app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Auth/Auth.PL/Auth.PL.csproj", "src/Services/Auth/Auth.PL/"]
COPY ["src/Shared/Shared.CleanArchitecture/Shared.CleanArchitecture.csproj", "src/Shared/Shared.CleanArchitecture/"]
COPY ["src/Shared/Shared.Components/Shared.Components.csproj", "src/Shared/Shared.Components/"]
COPY ["src/Services/Auth/Auth.BLL/Auth.BLL.csproj", "src/Services/Auth/Auth.BLL/"]
COPY ["src/Services/Auth/Auth.DAL/Auth.DAL.csproj", "src/Services/Auth/Auth.DAL/"]
RUN dotnet restore "./src/Services/Auth/Auth.PL/Auth.PL.csproj"
COPY . . 
WORKDIR "/src/src/Services/Auth/Auth.PL"
RUN dotnet build "./Auth.PL.csproj" -c $BUILD_CONFIGURATION -o /app/build

WORKDIR "/src/src/Services/Auth/Auth.DAL"
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef database update --project "./Auth.DAL.csproj"

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auth.PL.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "Auth.PL.dll"]
