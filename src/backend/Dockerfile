FROM mcr.microsoft.com/dotnet/core/sdk:3.1
ARG BUILD_CONFIGURATION
ARG DOTNET_USE_POLLING_FILE_WATCHER

EXPOSE 5000

WORKDIR /src
COPY ["Shb.Domain/Shb.Domain.csproj", "./Shb.Domain/"]
COPY ["Shb.Infrastructure/Shb.Infrastructure.csproj", "Shb.Infrastructure/Superheroes.json", "./Shb.Infrastructure/"]
COPY ["Shb.Application/Shb.Application.csproj", "./Shb.Application/"]
COPY ["Shb.Api/Shb.Api.csproj", "./Shb.Api/"]
COPY ["SuperHeroBattles.sln", "."]

RUN dotnet restore SuperHeroBattles.sln

COPY . .

RUN dotnet build SuperHeroBattles.sln --no-restore -c $BUILD_CONFIGURATION

WORKDIR /src/Shb.Api
ENTRYPOINT ["dotnet", "watch", "run", "--project=Shb.Api.csproj", "--no-launch-profile", "--urls", "http://*:5000"]