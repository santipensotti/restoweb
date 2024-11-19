# Etapa base: para correr la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de build: para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PruebaDoble.Server/PruebaDoble.Server.csproj", "PruebaDoble.Server/"]
COPY ["PruebaDoble.Shared/PruebaDoble.Shared.csproj", "PruebaDoble.Shared/"]
COPY ["PruebaDoble.Client/PruebaDoble.Client.csproj", "PruebaDoble.Client/"]
RUN dotnet restore "PruebaDoble.Server/PruebaDoble.Server.csproj"
COPY . .
WORKDIR "/src/PruebaDoble.Server"
RUN dotnet publish -c Release -o /app/publish

# Etapa final: para ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PruebaDoble.Server.dll"]
