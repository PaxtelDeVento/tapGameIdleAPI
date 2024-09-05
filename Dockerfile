FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TapGameAPI/TapGameAPI.csproj", "TapGameAPI/"]
RUN dotnet restore "TapGameAPI/TapGameAPI.csproj"
COPY . .
WORKDIR "/src/TapGameAPI"
RUN dotnet build "TapGameAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TapGameAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "TapGameAPI.dll"]
