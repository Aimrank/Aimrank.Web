FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /app

COPY src/Aimrank.Web.App/*.csproj ./src/Aimrank.Web.App/
COPY src/Common/Aimrank.Web.Common.Domain/*.csproj ./src/Common/Aimrank.Web.Common.Domain/
COPY src/Common/Aimrank.Web.Common.Application/*.csproj ./src/Common/Aimrank.Web.Common.Application/
COPY src/Common/Aimrank.Web.Common.Infrastructure/*.csproj ./src/Common/Aimrank.Web.Common.Infrastructure/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Domain/*.csproj ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Domain/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Application/*.csproj ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Application/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Infrastructure/*.csproj ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Infrastructure/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.IntegrationEvents/*.csproj ./src/Modules/Matches/Aimrank.Web.Modules.Matches.IntegrationEvents/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Domain/*.csproj ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Domain/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Application/*.csproj ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Application/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Infrastructure/*.csproj ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Infrastructure/

RUN dotnet restore src/Aimrank.Web.App

COPY src/Aimrank.Web.App/. ./src/Aimrank.Web.App/
COPY src/Common/Aimrank.Web.Common.Domain/. ./src/Common/Aimrank.Web.Common.Domain/
COPY src/Common/Aimrank.Web.Common.Application/. ./src/Common/Aimrank.Web.Common.Application/
COPY src/Common/Aimrank.Web.Common.Infrastructure/. ./src/Common/Aimrank.Web.Common.Infrastructure/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Domain/. ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Domain/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Application/. ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Application/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.Infrastructure/. ./src/Modules/Matches/Aimrank.Web.Modules.Matches.Infrastructure/
COPY src/Modules/Matches/Aimrank.Web.Modules.Matches.IntegrationEvents/. ./src/Modules/Matches/Aimrank.Web.Modules.Matches.IntegrationEvents/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Domain/. ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Domain/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Application/. ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Application/
COPY src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Infrastructure/. ./src/Modules/UserAccess/Aimrank.Web.Modules.UserAccess.Infrastructure/

WORKDIR /app/src/Aimrank.Web.App/Frontend

ENV NODE_VERSION=12.6.0
ENV NODE_ENV=Production

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

RUN dotnet publish src/Aimrank.Web.App -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app

COPY --from=build /app/out/ .

RUN apt-get update && apt-get install -y curl

HEALTHCHECK --interval=30s --timeout=30s --start-period=30s --retries=5 \
  CMD curl -f http://localhost/ || exit 1
  
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Aimrank.Web.App.dll"]
