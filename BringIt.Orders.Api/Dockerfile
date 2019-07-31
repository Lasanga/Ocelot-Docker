FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 3001 3011

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["BringIt.Orders.Api/BringIt.Orders.Api.csproj", "BringIt.Orders.Api/"]
COPY ["BringIt.Orders.Core/BringIt.Orders.Core.csproj", "BringIt.Orders.Core/"]
COPY ["BringIt.Orders.Services/BringIt.Orders.Services.csproj", "BringIt.Orders.Services/"]
RUN dotnet restore "BringIt.Orders.Api/BringIt.Orders.Api.csproj"
COPY . .
WORKDIR "/src/BringIt.Orders.Api"
RUN dotnet build "BringIt.Orders.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BringIt.Orders.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BringIt.Orders.Api.dll"]