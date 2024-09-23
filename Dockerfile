

# Imagem base para o ambiente de execução do ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagem para o SDK do .NET Core para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["consumoAPI/consumoAPI.csproj", "consumoAPI/"]
RUN dotnet restore "consumoAPI/consumoAPI.csproj"
COPY . .
WORKDIR "/src/consumoAPI"
RUN dotnet build "consumoAPI.csproj" -c Release -o /app/build

# Publicação da aplicação em modo Release
FROM build AS publish
RUN dotnet publish "consumoAPI.csproj" -c Release -o /app/publish

# Finalização da imagem com o runtime necessário para rodar a aplicação
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "consumoAPI.dll"]