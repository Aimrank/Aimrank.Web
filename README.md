# Aimrank

CS:GO server with Docker configuration. Comes with Metamod and Sourcemod preinstalled and configured.

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
4. Copy initial configuration files
5. Start server

## How to run

```bash
docker-compose up
```

## References

1. Documentation

    - [Metamod](https://wiki.alliedmods.net/Category:Metamod:Source_Documentation)
    - [Sourcemod](https://wiki.alliedmods.net/Category:SourceMod_Documentation)
      - [Events](https://wiki.alliedmods.net/Counter-Strike:_Global_Offensive_Events#round_end)

2. Sourcepawn

    - [Online compiler](https://spider.limetech.io/)