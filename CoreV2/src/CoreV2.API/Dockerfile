FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CoreV2.API/Core.API.csproj", "src/CoreV2.API/"]
COPY ["src/Core.Domain/Core.Domain.csproj", "src/Core.Domain/"]
COPY ["src/Core.Application/Core.Application.csproj", "src/Core.Application/"]
COPY ["src/Intrastructure/Core.DataAccess/Core.DataAccess.csproj", "src/Intrastructure/Core.DataAccess/"]
RUN dotnet restore "src/CoreV2.API/Core.API.csproj"
COPY . .
WORKDIR "/src/src/CoreV2.API"
RUN dotnet build "Core.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Core.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core.API.dll"]
