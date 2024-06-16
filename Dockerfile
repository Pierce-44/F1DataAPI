FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["F1DataAPI.csproj", "./"]
RUN dotnet restore "./F1DataAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "F1DataAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "F1DataAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "F1DataAPI.dll"]
