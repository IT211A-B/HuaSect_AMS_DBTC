FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["*.sln", "./"]
COPY ["HuaSect_AMS_DBTCwebapi/*.csproj", "HuaSect_AMS_DBTCwebapi/"]
COPY ["HuaSect_AMS_DBTCtests/*.csproj", "HuaSect_AMS_DBTCtests/"]
COPY ["HuaSect_AMS_DBTCclasslib/*.csproj", "HuaSect_AMS_DBTCclasslib/"]

RUN dotnet restore

COPY . .

WORKDIR "/src/HuaSect_AMS_DBTCwebapi"
RUN dotnet publish "HuaSect_AMS_DBTCwebapi.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false \
    /p:CopyDebugSymbolFilesFromPackages=true

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# RUN useradd -m -u 1001 appuser && chown -R appuser:appuser /app
# USER appuser

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Staging \
    ASPNETCORE_URLS=http://+:8080 \
    DOTNET_EnableDiagnostics=1 \
    DOTNET_NoSymbolUpdate=1

EXPOSE 8080

# HEALTHCHECK --interval=20s --timeout=3s --start-period=15s --retries=3 \
#   CMD wget --no-verbose --tries=1 --spider http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "HuaSect_AMS_DBTCwebapi.dll"]