# Use uma imagem base do .NET 7.0 (ou a versão que você está utilizando)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Utilize uma imagem base para build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["consumoAPI/consumoAPI.csproj", "consumoAPI/"]
RUN dotnet restore "consumoAPI/consumoAPI.csproj"
COPY . .
WORKDIR "/src/consumoAPI"
RUN dotnet build "consumoAPI.csproj" -c Release -o /app/build

# Publicar o resultado da build
FROM build AS publish
RUN dotnet publish "consumoAPI.csproj" -c Release -o /app/publish

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "consumoAPI.dll"]