FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 4001 4002

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY BringIt.Users.Api/BringIt.Users.Api.csproj BringIt.Users.Api/
COPY BringIt.Users.Infrastructure/BringIt.Users.Infrastructure.csproj BringIt.Users.Infrastructure/
COPY BringIt.Users.Core/BringIt.Users.Core.csproj BringIt.Users.Core/
RUN dotnet restore BringIt.Users.Api/BringIt.Users.Api.csproj
COPY . .
WORKDIR /src/BringIt.Users.Api
RUN dotnet build BringIt.Users.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BringIt.Users.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BringIt.Users.Api.dll"]
