# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
WORKDIR /src
COPY LiteWebApi.sln ./
COPY WebApi/*.csproj ./WebApi/
COPY Data/*.csproj ./Data/
RUN dotnet restore

# copy everything else and build app
COPY . .

WORKDIR /src/Data
RUN dotnet publish -o /app

WORKDIR /src/WebApi
RUN dotnet publish -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WebApi.dll", "--environment=Development"]