FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./MultiAtendimento.API.csproj", "MultiAtendimento.API/"]
RUN dotnet restore "./MultiAtendimento.API/./MultiAtendimento.API.csproj"
WORKDIR "/src/MultiAtendimento.API"
COPY . .
RUN dotnet build "./MultiAtendimento.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MultiAtendimento.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MultiAtendimento.API.dll"]