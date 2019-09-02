FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/FakeP2P/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY src/FakeP2P/* ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
CMD dotnet AspNetCoreHerokuDocker.dll