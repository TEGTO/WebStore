#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RabbitMQReceiver/RabbitMQReceiver.csproj", "RabbitMQReceiver/"]
COPY ["RabbitMQ/RabbitMQ.csproj", "RabbitMQ/"]
COPY ["WebStoreApi/WebStoreApi.csproj", "WebStoreApi/"]
COPY ["AuthenticationManager/AuthenticationManager.csproj", "AuthenticationManager/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "./RabbitMQReceiver/RabbitMQReceiver.csproj"
COPY . .
WORKDIR "/src/RabbitMQReceiver"
RUN dotnet build "./RabbitMQReceiver.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RabbitMQReceiver.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitMQReceiver.dll"]