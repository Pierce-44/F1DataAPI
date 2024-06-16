# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY F1DataAPI/*.csproj ./F1DataAPI/
RUN dotnet restore

# copy everything else and build app
COPY F1DataAPI/. ./F1DataAPI/
WORKDIR /source/F1DataAPI
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "F1DataAPI.dll"]