FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# ENV ASPNETCORE_URLS=http://+:5041 ------ program file should handle this
# Download the SQLite database file from Firebase Storage

ADD https://firebasestorage.googleapis.com/v0/b/f1-data-storage.appspot.com/o/F1DataAPI.db?alt=media&token=dacae2c3-04ea-4d6c-8f40-0956e63c183a /app/F1DataAPI.db

ADD https://firebasestorage.googleapis.com/v0/b/f1-data-storage.appspot.com/o/F1DataAPI.db-shm?alt=media&token=024999cc-c14d-4549-9e9a-8a6eb16830a9 /app/F1DataAPI.db-shm

ADD https://firebasestorage.googleapis.com/v0/b/f1-data-storage.appspot.com/o/F1DataAPI.db-wal?alt=media&token=0d703aba-8cc9-49dd-8904-4ca17d49830e /app/F1DataAPI.db-wal

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