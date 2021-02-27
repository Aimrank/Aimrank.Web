# -- Step 1 -- Restore and build web application

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production

ENV NODE_VERSION=12.6.0
ENV NODE_ENV=Production

COPY *.sln .
COPY src/Aimrank.Web/*.csproj ./src/Aimrank.Web/
COPY src/Aimrank.Domain/*.csproj ./src/Aimrank.Domain/
COPY src/Aimrank.Application/*.csproj ./src/Aimrank.Application/
COPY src/Aimrank.Infrastructure/*.csproj ./src/Aimrank.Infrastructure/
COPY src/Aimrank.IntegrationEvents/*.csproj ./src/Aimrank.IntegrationEvents/
COPY src/Tests/Aimrank.UnitTests/*.csproj ./src/Tests/Aimrank.UnitTests/
COPY src/Tests/Aimrank.IntegrationTests/*.csproj ./src/Tests/Aimrank.IntegrationTests/
COPY src/Common/Aimrank.Common.Domain/*.csproj ./src/Common/Aimrank.Common.Domain/
COPY src/Common/Aimrank.Common.Application/*.csproj ./src/Common/Aimrank.Common.Application/
COPY src/Common/Aimrank.Common.Infrastructure/*.csproj ./src/Common/Aimrank.Common.Infrastructure/
COPY src/Database/Aimrank.Database.Migrator/*.csproj ./src/Database/Aimrank.Database.Migrator/

RUN dotnet restore

COPY src/Aimrank.Web/. ./src/Aimrank.Web/
COPY src/Aimrank.Domain/. ./src/Aimrank.Domain/
COPY src/Aimrank.Application/. ./src/Aimrank.Application/
COPY src/Aimrank.Infrastructure/. ./src/Aimrank.Infrastructure/
COPY src/Aimrank.IntegrationEvents/. ./src/Aimrank.IntegrationEvents/
COPY src/Tests/Aimrank.IntegrationTests/. ./src/Tests/Aimrank.IntegrationTests/
COPY src/Common/Aimrank.Common.Domain/. ./src/Common/Aimrank.Common.Domain/
COPY src/Common/Aimrank.Common.Application/. ./src/Common/Aimrank.Common.Application/
COPY src/Common/Aimrank.Common.Infrastructure/. ./src/Common/Aimrank.Common.Infrastructure/
COPY src/Database/Aimrank.Database.Migrator/. ./src/Database/Aimrank.Database.Migrator/

WORKDIR /app/src/Aimrank.Web/Frontend

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

# -- Step 2 -- Create image with web application and CS:GO server

FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN mkdir -p /home/app

COPY --from=build /app/out/ /home/app/.

ENV ASPNETCORE_ENVIRONMENT=Production

ENV STEAM_DIR /home/steam
ENV STEAM_CMD_DIR /home/steam/steamcmd
ENV CSGO_APP_ID 740
ENV CSGO_DIR /home/steam/csgo

ARG STEAM_CMD_URL=https://steamcdn-a.akamaihd.net/client/installer/steamcmd_linux.tar.gz

RUN DEBIAN_FRONTEND=noninteractive && apt-get update \
  && apt-get install --no-install-recommends --no-install-suggests -y \
      lib32gcc1 \
      lib32stdc++6 \
      ca-certificates \
      net-tools \
      locales \
      curl \
      wget \
      unzip \
      screen \
  && locale-gen en_US.UTF-8 \
  && adduser --disabled-password --gecos "" steam \
  && mkdir ${STEAM_CMD_DIR} \
  && cd ${STEAM_CMD_DIR} \
  && curl -sSL ${STEAM_CMD_URL} | tar -zx -C ${STEAM_CMD_DIR} \
  && mkdir -p ${STEAM_DIR}/.steam/sdk32 \
  && ln -s ${STEAM_CMD_DIR}/linux32/steamclient.so ${STEAM_DIR}/.steam/sdk32/steamclient.so \
  && { \
    echo '@ShutdownOnFailedCommand 1'; \
    echo '@NoPromptForPassword 1'; \
    echo 'login anonymous'; \
    echo 'force_install_dir ${CSGO_DIR}'; \
    echo 'app_update ${CSGO_APP_ID}'; \
    echo 'quit'; \
  } > ${STEAM_DIR}/autoupdate_script.txt \
  && mkdir ${CSGO_DIR} \
  && chown -R steam:steam ${STEAM_DIR} \
  && rm -rf /var/lib/apt/lists/*
  
COPY --chown=steam:steam container_fs/csgo/ ${STEAM_DIR}/
COPY --chown=steam:steam container_fs/start.sh /home/start.sh

RUN chmod +x /home/start.sh

# -- Step 3 -- Compile sourcemod plugins

WORKDIR ${STEAM_DIR}/sourcemod/plugins

RUN tar -xzf build.tar.gz \
  && chmod +x ./build/sourcemod/scripting/spcomp \
  && ./build/sourcemod/scripting/spcomp aimrank.sp

# -- Step 4 -- Startup

VOLUME ${CSGO_DIR}

WORKDIR /home/app

EXPOSE 27016-27019/udp
EXPOSE 27016-27019/tcp

HEALTHCHECK --interval=30s --timeout=30s --start-period=30s --retries=5 \
  CMD curl -f http://localhost/ || exit 1

ENTRYPOINT ["/home/start.sh"]
