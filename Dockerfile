FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build-backend
WORKDIR /app

COPY *.sln .
COPY src/Aimrank.Web.App/*.csproj ./src/Aimrank.Web.App/
COPY src/Common/Aimrank.Web.Common.Domain/*.csproj ./src/Common/Aimrank.Web.Common.Domain/
COPY src/Common/Aimrank.Web.Common.Application/*.csproj ./src/Common/Aimrank.Web.Common.Application/
COPY src/Common/Aimrank.Web.Common.Infrastructure/*.csproj ./src/Common/Aimrank.Web.Common.Infrastructure/
COPY src/Database/Aimrank.Web.Database.Migrator/*.csproj ./src/Database/Aimrank.Web.Database.Migrator/
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

COPY . .

RUN dotnet publish -c Release -o /app/out

FROM node:12 AS build-frontend
WORKDIR /app

COPY src/Aimrank.Web.App/Frontend ./src/Aimrank.Web.App/Frontend

WORKDIR /app/src/Aimrank.Web.App/Frontend

RUN npm install && npm run build-prod && mv ../wwwroot ../../../wwwroot

FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app

COPY --from=build-backend /app/out/ .
COPY --from=build-frontend /app/wwwroot/. ./wwwroot/

RUN apt-get update && apt-get install -y curl

HEALTHCHECK --interval=30s --timeout=30s --start-period=30s --retries=5 \
  CMD curl -f http://localhost/ || exit 1
  
ENV ASPNETCORE_ENVIRONMENT=Production

CMD ["dotnet", "Aimrank.Web.App.dll"]
