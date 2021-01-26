# -- Step 1 -- Build dotnet projects

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY *.sln .
COPY src/Aimrank.Web/*.csproj ./src/Aimrank.Web/
COPY src/Aimrank.Domain/*.csproj ./src/Aimrank.Domain/
COPY src/Aimrank.Application/*.csproj ./src/Aimrank.Application/
COPY src/Aimrank.Infrastructure/*.csproj ./src/Aimrank.Infrastructure/
COPY src/Database/Aimrank.Database.Migrator/*.csproj ./src/Database/Aimrank.Database.Migrator/
COPY src/BusPublisher/Aimrank.BusPublisher/*.csproj ./src/BusPublisher/Aimrank.BusPublisher/

RUN dotnet restore

COPY src/Aimrank.Web/. ./src/Aimrank.Web/
COPY src/Aimrank.Domain/. ./src/Aimrank.Domain/
COPY src/Aimrank.Application/. ./src/Aimrank.Application/
COPY src/Aimrank.Infrastructure/. ./src/Aimrank.Infrastructure/
COPY src/Database/Aimrank.Database.Migrator/. ./src/Database/Aimrank.Database.Migrator/
COPY src/BusPublisher/Aimrank.BusPublisher/. ./src/BusPublisher/Aimrank.BusPublisher/

RUN dotnet publish -c Release -o /app/out

# -- Step 2 -- Create image with dotnet runtime and CS:GO server

FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN mkdir -p /home/app

COPY --from=build /app/out/ /home/app/.

ENV ASPNETCORE_ENVIRONMENT=Production

ENV STEAM_DIR /home/steam
ENV STEAM_CMD_DIR /home/steam/steamcmd
ENV CSGO_APP_ID 740
ENV CSGO_DIR /home/steam/csgo


# -- Step 3 -- Install CS:GO server

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
  
COPY --chown=steam:steam container_fs ${STEAM_DIR}/

USER steam
VOLUME ${CSGO_DIR}
WORKDIR /home/app

# CS:GO server ports
EXPOSE 27016-27019/udp
EXPOSE 27016-27019/tcp

ENTRYPOINT ["dotnet", "Aimrank.Web.dll"]
