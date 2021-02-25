#define CLIENT_WHITELIST_ENTRY_LENGTH 20  // {steam_id}:{team}
#define CLIENT_ID_LENGTH 18

#define STATS_SIZE 4
#define STATS_INDEX_KILLS 0
#define STATS_INDEX_ASSISTS 1
#define STATS_INDEX_DEATHS 2
#define STATS_INDEX_HS 3

int g_maxClients;

bool g_gamePaused;
bool g_gameStarted;

StringMap g_scoreboard;
StringMap g_clients;

ConVar g_serverId;
ConVar g_whitelist;
ConVar g_maxRounds;

public void InitializeGlobals()
{
    g_serverId = CreateConVar("aimrank_server_id", "00000000-0000-0000-0000-000000000000", "Aimrank server identifier");
    g_whitelist = CreateConVar("aimrank_whitelist", "0", "SteamID list of whitelisted players.");
    g_maxRounds = FindConVar("mp_maxrounds");

    g_maxClients = GetClientsLimit();
    g_scoreboard = new StringMap();
    g_clients = new StringMap();
}

public void GetClientSteamId(int client, char[] output)
{
    GetClientAuthId(client, AuthId_SteamID64, output, CLIENT_ID_LENGTH);
}

int GetClientsLimit()
{
    int count = 0;
    int length = CLIENT_WHITELIST_ENTRY_LENGTH * 10;
    char[] whitelist = new char[length];

    GetConVarString(g_whitelist, whitelist, length);

    for (int i = 0; i < length; i++)
    {
        if (whitelist[i] == 0) break;
        if (whitelist[i] == ',')
        {
            count++;
        }
    }

    return count + 1;
}