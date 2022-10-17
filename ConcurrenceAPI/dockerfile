FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore
# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
    
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ConcurrenceAPI.dll"]

