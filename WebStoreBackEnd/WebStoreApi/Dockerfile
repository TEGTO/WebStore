FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebStoreApi/WebStoreApi.csproj", "WebStoreApi/"]
COPY ["AuthenticationManager/AuthenticationManager.csproj", "AuthenticationManager/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "./WebStoreApi/WebStoreApi.csproj"
COPY . .
WORKDIR "/src/WebStoreApi"
RUN dotnet build "./WebStoreApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS migration
WORKDIR /src/WebStoreApi
# Ensure dotnet-ef is installed
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
# Run migrations
RUN dotnet ef database update --project WebStoreApi --startup-project WebStoreApi --configuration $BUILD_CONFIGURATION

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebStoreApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebStoreApi.dll"]