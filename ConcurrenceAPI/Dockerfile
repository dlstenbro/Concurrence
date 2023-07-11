FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
ENV ASPNETCORE_ENVIRONMENT=${ENV_DEPLOYMENT_TYPE}

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore
# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
    
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV ASPNETCORE_ENVIRONMENT=${ENV_DEPLOYMENT_TYPE}

WORKDIR /app
COPY --from=builder /app/out .
ENTRYPOINT ["dotnet", "ConcurrenceAPI.dll"]