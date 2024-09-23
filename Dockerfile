# Use a imagem base do SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie o csproj e restaure dependências
COPY consumoAPI/consumoAPI.csproj ./consumoAPI/
WORKDIR /app/consumoAPI
RUN dotnet restore

# Copie o restante do código e compile
COPY consumoAPI/. ./consumoAPI/
RUN dotnet publish -c Release -o out

# Use a imagem do runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

# Exponha a porta que seu app utiliza
EXPOSE 3000

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "consumoAPI.dll"]
