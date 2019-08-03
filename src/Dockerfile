FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Provausio.SecureLink.Api/Provausio.SecureLink.Api.csproj", "Provausio.SecureLink.Api/"]
COPY ["Provausio.SecureLink.Application/Provausio.SecureLink.Application.csproj", "Provausio.SecureLink.Application/"]
COPY ["Provausio.SecureLink.MongoDb/Provausio.SecureLink.MongoDb.csproj", "Provausio.SecureLink.MongoDb/"]
RUN dotnet restore "Provausio.SecureLink.Api/Provausio.SecureLink.Api.csproj"
COPY . .
WORKDIR "/src/Provausio.SecureLink.Api"
RUN dotnet build "Provausio.SecureLink.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Provausio.SecureLink.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Provausio.SecureLink.Api.dll"]