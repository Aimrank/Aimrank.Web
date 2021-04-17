FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production

ENV NODE_VERSION=12.6.0
ENV NODE_ENV=Production

COPY *.sln .
COPY src/Aimrank.Web.App/*.csproj ./src/Aimrank.Web.App/
COPY src/Common/Aimrank.Web.Common.Domain/*.csproj ./src/Common/Aimrank.Web.Common.Domain/
COPY src/Common/Aimrank.Web.Common.Application/*.csproj ./src/Common/Aimrank.Web.Common.Application/
COPY src/Common/Aimrank.Web.Common.Infrastructure/*.csproj ./src/Common/Aimrank.Web.Common.Infrastructure/
COPY src/Database/Aimrank.Web.Database.Migrator/*.csproj ./src/Database/Aimrank.Web.Database.Migrator/
COPY src/Modules/CSGO/Aimrank.Web.Modules.CSGO.Application/*.csproj ./src/Modules/CSGO/Aimrank.Web.Modules.CSGO.Application/
COPY src/Modules/CSGO/Aimrank.Web.Modules.CSGO.Infrastructure/*.csproj ./src/Modules/CSGO/Aimrank.Web.Modules.CSGO.Infrastructure/
COPY src/Modules/CSGO/Aimrank.Web.Modules.CSGO.IntegrationEvents/*.csproj ./src/Modules/CSGO/Aimrank.Web.Modules.CSGO.IntegrationEvents/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Domain/*.csproj ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Domain/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Application/*.csproj ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Application/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Infrastructure/*.csproj ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Infrastructure/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.IntegrationEvents/*.csproj ./src/Modules/Matches/Aimrank.Web.Modules.Matches.IntegrationEvents/
COPY src/Modules/Matches/Tests/Aimrank.Web.Modules.Matches.ArchTests/*.csproj ./src/Modules/Matches/Tests/Aimrank.Web.Modules.Matches.ArchTests/
COPY src/Modules/Matches/Tests/Aimrank.Web.Modules.Matches.UnitTests/*.csproj ./src/Modules/Matches/Tests/Aimrank.Web.Modules.Matches.UnitTests/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Domain/*.csproj ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Domain/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Application/*.csproj ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Application/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Infrastructure/*.csproj ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Infrastructure/
COPY src/Modules/UserAccess/Tests/Aimrank.Web.Modules.UserAccess.UnitTests/*.csproj ./src/Modules/UserAccess/Tests/Aimrank.Web.Modules.UserAccess.UnitTests/

RUN dotnet restore

COPY src/Aimrank.Web.App/. ./src/Aimrank.Web.App/
COPY src/Common/Aimrank.Web.Common.Domain/. ./src/Common/Aimrank.Web.Common.Domain/
COPY src/Common/Aimrank.Web.Common.Application/. ./src/Common/Aimrank.Web.Common.Application/
COPY src/Common/Aimrank.Web.Common.Infrastructure/. ./src/Common/Aimrank.Web.Common.Infrastructure/
COPY src/Database/Aimrank.Web.Database.Migrator/. ./src/Database/Aimrank.Web.Database.Migrator/
COPY src/Modules/CSGO/Aimrank.Web.Modules.CSGO.Application/. ./src/Modules/CSGO/Aimrank.Web.Modules.CSGO.Application/
COPY src/Modules/CSGO/Aimrank.Web.Modules.CSGO.Infrastructure/. ./src/Modules/CSGO/Aimrank.Web.Modules.CSGO.Infrastructure/
COPY src/Modules/CSGO/Aimrank.Web.Modules.CSGO.IntegrationEvents/. ./src/Modules/CSGO/Aimrank.Web.Modules.CSGO.IntegrationEvents/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Domain/. ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Domain/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Application/. ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Application/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Infrastructure/. ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Infrastructure/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.IntegrationEvents/. ./src/Modules/Matches/Aimrank.Web.Modules.Matches.IntegrationEvents/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Domain/. ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Domain/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Application/. ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Application/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Infrastructure/. ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Infrastructure/

WORKDIR /app/src/Aimrank.Web.App/Frontend

RUN apt install -y curl
RUN curl -o- https://raw.githubusercontent.com/creationix/nvm/v0.34.0/install.sh | bash
ENV NVM_DIR=/root/.nvm
RUN . "$NVM_DIR/nvm.sh" && nvm install ${NODE_VERSION}
RUN . "$NVM_DIR/nvm.sh" && nvm use v${NODE_VERSION}
RUN . "$NVM_DIR/nvm.sh" && nvm alias default v${NODE_VERSION}
ENV PATH="/root/.nvm/versions/node/v${NODE_VERSION}/bin/:${PATH}"
RUN node --version
RUN npm --version

RUN npm install && npm run build-prod

WORKDIR /app

RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app

COPY --from=build /app/out/ .

COPY scripts/start.sh start.sh

RUN chmod +x start.sh

HEALTHCHECK --interval=30s --timeout=30s --start-period=30s --retries=5 \
  CMD curl -f http://localhost/ || exit 1

ENTRYPOINT ["start.sh"]
