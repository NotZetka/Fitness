FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App/API

COPY API.csproj .

RUN dotnet restore API.csproj

COPY . .

RUN dotnet publish API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

EXPOSE 8080
COPY --from=build-env /App/out ./
ENTRYPOINT ["dotnet", "API.dll"]