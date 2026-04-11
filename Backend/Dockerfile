# --- Stage 1: Build (with caching optimizations) ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution + project files first for layer caching
COPY ["*.sln", "./"]
COPY ["HuaSect_AMS_DBTCwebapi/*.csproj", "HuaSect_AMS_DBTCwebapi/"]
COPY ["HuaSect_AMS_DBTCclasslib/*.csproj", "HuaSect_AMS_DBTCclasslib/"]

# Restore dependencies (cached unless .csproj changes)
RUN dotnet restore

# Copy source code
COPY . .

# Publish WebAPI in Release mode (smaller + faster, but keep symbols for debugging)
WORKDIR "/src/HuaSect_AMS_DBTCwebapi"
RUN dotnet publish "HuaSect_AMS_DBTCwebapi.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false \
    /p:CopyDebugSymbolFilesFromPackages=true

# --- Stage 2: Runtime (staging-friendly) ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Keep root user in staging for easier debugging (remove in prod)
# RUN useradd -m -u 1001 appuser && chown -R appuser:appuser /app
# USER appuser

COPY --from=build /app/publish .

# Staging environment config
ENV ASPNETCORE_ENVIRONMENT=Staging \
    ASPNETCORE_URLS=http://+:8080 \
    DOTNET_EnableDiagnostics=1 \
    DOTNET_NoSymbolUpdate=1

EXPOSE 8080

# Lightweight health check (wget is in aspnet:8.0 image)
# HEALTHCHECK --interval=20s --timeout=3s --start-period=15s --retries=3 \
#   CMD wget --no-verbose --tries=1 --spider http://localhost:8080/health || exit 1

# Enable detailed exception pages in staging (via env var + appsettings)
ENTRYPOINT ["dotnet", "HuaSect_AMS_DBTCwebapi.dll"]