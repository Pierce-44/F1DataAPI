FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# NEEDED FOR LOCAL TESTING
# EXPOSE 5041

# NEEDED FOR LOCAL TESTING
# ENV ASPNETCORE_URLS=http://+:5041 


# Download the SQLite database file from Firebase Storage
ADD https://firebasestorage.googleapis.com/v0/b/f1-data-storage.appspot.com/o/F1DataAPI.db?alt=media&token=eff9b187-1440-40f4-b200-df65670bd28e /app/F1DataAPI.db

ADD https://firebasestorage.googleapis.com/v0/b/f1-data-storage.appspot.com/o/F1DataAPI.db-shm?alt=media&token=d13b7979-d896-404d-bf87-7dda3065d902 /app/F1DataAPI.db-shm

ADD https://firebasestorage.googleapis.com/v0/b/f1-data-storage.appspot.com/o/F1DataAPI.db-wal?alt=media&token=aac7d6e2-b611-4fde-b735-65cb42154631 /app/F1DataAPI.db-wal

# USER app ------ THIS CAUSED iSSUES CREATING THE DB
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["F1DataAPI.csproj", "./"]
RUN dotnet restore "F1DataAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "F1DataAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "F1DataAPI.csproj" -c $configuration -o /app/publish /p=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "F1DataAPI.dll"]