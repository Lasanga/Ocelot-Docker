FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["BringIt.Gateway.Api/BringIt.Gateway.Api.csproj", "BringIt.Gateway.Api/"]
RUN dotnet restore "BringIt.Gateway.Api/BringIt.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/BringIt.Gateway.Api"
RUN dotnet build "BringIt.Gateway.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BringIt.Gateway.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BringIt.Gateway.Api.dll"]