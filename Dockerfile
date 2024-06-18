FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# ENV ASPNETCORE_URLS=http://+:5041 ------ program file should handle this

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
RUN dotnet publish "F1DataAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "F1DataAPI.dll"]
