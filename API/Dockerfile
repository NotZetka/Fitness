FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY API/API.csproj ./API/

RUN dotnet restore API/API.csproj

COPY API/ ./API/

RUN dotnet publish API/API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /App
EXPOSE 8080
COPY --from=build-env /App/out ./
ENTRYPOINT ["dotnet", "API.dll"]