FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /src/FakeP2P

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /src/FakeP2P
COPY --from=build-env /app/out .
CMD dotnet AspNetCoreHerokuDocker.dll