FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["BringIt.Auth.Api/BringIt.Auth.Api.csproj", "BringIt.Auth.Api/"]
RUN dotnet restore "BringIt.Auth.Api/BringIt.Auth.Api.csproj"
COPY . .
WORKDIR "/src/BringIt.Auth.Api"
RUN dotnet build "BringIt.Auth.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BringIt.Auth.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BringIt.Auth.Api.dll"]