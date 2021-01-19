# Aimrank

CS:GO server with Docker configuration. Comes with Metamod and Sourcemod preinstalled and configured.

Additionally container exposes web application on port 80 which is used to present live server status (e.g. automatically updated scoreboard).

## Purpose of this repository

Main purpose of this repository is to create POC setup of CS:GO server + web application and establish communication between them. The web server
should be able to listen on in-game events (player killed, round finished) and execute various CS:GO server commands (restart game, execute config file).

## Environment variables

|Name                 |Default value|
|---------------------|-------------|
|SERVER_HOSTNAME      |Counter-Strike: Global Offensive Dedicated Server|
|SERVER_PASSWORD      ||
|SERVER_ADMIN_STEAMID ||
|RCON_PASSWORD        |changeme|
|STEAM_ACCOUNT        |changeme|

## What it does

1. Install CS:GO server on Linux
2. Install Metamod
3. Install Sourcemod
4. Copy server configuration files
5. Start web server that is used to manage CS:GO server

## How to run

1. Start container with web application and CS:GO server
   ```bash
   docker-compose up
   ```
   
2. Plugins from `/src` are not built automatically yet. You have to compile them manually and copy to
   `container_data/csgo/addons/sourcemod/plugins`. They are necessary for application to work at all.
   
3. When starting CS:GO server for the first time it has to download all necessary data (~28GB). This might take a while depending on
   your connection. It's saved under /home/steam/csgo and it's persisted inside `container_data` directory.
   

## References

1. Documentation

    - [Metamod](https://wiki.alliedmods.net/Category:Metamod:Source_Documentation)
    - [Sourcemod](https://wiki.alliedmods.net/Category:SourceMod_Documentation)
      - [Events](https://wiki.alliedmods.net/Counter-Strike:_Global_Offensive_Events#round_end)

2. Sourcepawn

    - [Online compiler](https://spider.limetech.io/)
   
3. Some repositories used as reference:

   - [csgobash](https://github.com/jpcanoso/csgobash)
   
## Others

Processes spawned when starting cs go server:

   1. Bash
      1.1. (Child process) Screen with attached console to CS:GO server
         1.2. (Child process) CS:GO server instance