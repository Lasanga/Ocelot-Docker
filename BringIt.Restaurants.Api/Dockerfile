FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 3002 3022

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["BringIt.Restaurants.Api/BringIt.Restaurants.Api.csproj", "BringIt.Restaurants.Api/"]
COPY ["BringIt.Restaurant.Services/BringIt.Restaurant.Services.csproj", "BringIt.Restaurant.Services/"]
COPY ["BringIt.Restaurant.Infrastrucutre/BringIt.Restaurant.Infrastrucutre.csproj", "BringIt.Restaurant.Infrastrucutre/"]
COPY ["BringIt.Restaurants.Core/BringIt.Restaurants.Core.csproj", "BringIt.Restaurants.Core/"]
RUN dotnet restore "BringIt.Restaurants.Api/BringIt.Restaurants.Api.csproj"
COPY . .
WORKDIR "/src/BringIt.Restaurants.Api"
RUN dotnet build "BringIt.Restaurants.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BringIt.Restaurants.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BringIt.Restaurants.Api.dll"]