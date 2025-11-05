FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["Directory.Packages.props", "./"]
COPY ["TeamManager.sln", "./"]

COPY ["TeamManager.API/TeamManager.API.csproj", "TeamManager.API/"]
COPY ["TeamManager.Infrastructure/TeamManager.Infrastructure.csproj", "TeamManager.Infrastructure/"]
COPY ["TeamManager.Application/TeamManager.Application.csproj", "TeamManager.Application/"]
COPY ["TeamManager.Domain/TeamManager.Domain.csproj", "TeamManager.Domain/"]
COPY ["TeamManager.Tests/TeamManager.Tests.csproj", "TeamManager.Tests/"]

# Here we clear the NuGet cache and restore the dependencies
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore TeamManager.sln --verbosity normal

COPY . .

RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet publish "TeamManager.API/TeamManager.API.csproj" \
    -c $BUILD_CONFIGURATION -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
ARG APP_USER=app
ARG APP_UID=1000

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

RUN adduser -S -u ${APP_UID:-1000} -G ${APP_USER} -h /app ${APP_USER} || true

WORKDIR /app

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

EXPOSE 8000

COPY --from=build --chown=${APP_USER}:${APP_USER} /app/publish .

USER ${APP_USER}

ENTRYPOINT ["dotnet", "TeamManager.API.dll"]