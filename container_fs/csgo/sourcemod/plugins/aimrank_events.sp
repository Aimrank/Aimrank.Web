#include <sourcemod>
#include <sdktools>
#include <cstrike>
#include <system2>
#include <json>

#define CLIENT_NAME_LENGTH 33
#define CLIENT_ID_LENGTH 18

#define STATS_SIZE 4
#define STATS_INDEX_KILLS 0
#define STATS_INDEX_ASSISTS 1
#define STATS_INDEX_DEATHS 2
#define STATS_INDEX_HS 3

StringMap g_scoreboard;
ConVar g_aimrankServerId;

public Plugin myinfo =
{
    name = "Aimrank Events",
    author = "Aimrank",
    description = "Sending game events as json to web application.",
    version = "1.0.0",
    url = ""
};

public void OnPluginStart()
{
    g_scoreboard = new StringMap();
    g_aimrankServerId = CreateConVar("aimrank_server_id", "00000000-0000-0000-0000-000000000000", "Aimrank server identifier");
    
    HookEvent("cs_win_panel_match", Event_MatchEnd);
    HookEvent("player_death", Event_PlayerDeath);
}

public void OnMapStart()
{
    PublishEvent(CreateIntegrationEvent("map_start", null));
}

public Action Event_MatchEnd(Event event, const char[] name, bool dontBroadcast)
{
    PublishEvent(CreateIntegrationEvent("match_end", GetScoreboard()));
}

public Action Event_PlayerDeath(Event event, const char[] name, bool dontBroadcast)
{
    if (GameRules_GetProp("m_bWarmupPeriod") == 1)
    {
        // Don't record player stats when warmup is active

        return Plugin_Continue;
    }

    int attacker = event.GetInt("attacker");
    bool headshot = event.GetBool("headshot");
    int clientId = GetClientOfUserId(attacker);

    for (int i = 1; i <= MaxClients; i++)
    {
        if (IsClientConnected(i) && IsClientInGame(i))
        {
            int stats[STATS_SIZE];

            GetClientStats(i, stats)

            stats[STATS_INDEX_KILLS] = GetEntProp(i, Prop_Data, "m_iFrags");
            stats[STATS_INDEX_ASSISTS] = CS_GetClientAssists(i);
            stats[STATS_INDEX_DEATHS] = GetEntProp(i, Prop_Data, "m_iDeaths");
            stats[STATS_INDEX_HS] = stats[STATS_INDEX_HS] + (i == clientId && headshot ? 1 : 0);

            SetClientStats(i, stats);
        }
    }
}

public void PublishEvent(JSON_Object data)
{
    char event[1024];
    char serverId[64];

    GetConVarString(g_aimrankServerId, serverId, sizeof(serverId));

    data.SetString("matchId", serverId);
    data.Encode(event, sizeof(event));
    
    System2HTTPRequest httpRequest = new System2HTTPRequest(HttpResponseCallback, "http://localhost/api/server");
    httpRequest.SetHeader("Content-Type", "application/json");
    httpRequest.SetData(event);
    httpRequest.POST();
    
    delete httpRequest;

    data.Cleanup();
}

public void HttpResponseCallback(bool success, const char[] error, System2HTTPRequest request, System2HTTPResponse response, HTTPRequestMethod method)
{
}

public JSON_Object CreateIntegrationEvent(const char[] name, JSON_Object data)
{
    JSON_Object event = new JSON_Object();
    event.SetString("name", name);
    event.SetObject("data", data);
    return event;
}

public JSON_Object GetScoreboard()
{
    JSON_Array clientsT = new JSON_Array();
    JSON_Array clientsCt = new JSON_Array();

    for (int i = 1; i <= MaxClients; i++)
    {
        if (IsClientConnected(i) && IsClientInGame(i))
        {
            JSON_Object data = new JSON_Object();

            char clientName[CLIENT_NAME_LENGTH];
            char clientAuth[CLIENT_ID_LENGTH];
            int stats[STATS_SIZE];

            GetClientName(i, clientName, sizeof(clientName));
            GetClientSteamId(i, clientAuth, sizeof(clientAuth));
            GetClientStats(i, stats);

            data.SetString("name", clientName);
            data.SetString("steamId", clientAuth);
            data.SetInt("kills", stats[STATS_INDEX_KILLS]);
            data.SetInt("assists", stats[STATS_INDEX_ASSISTS]);
            data.SetInt("deaths", stats[STATS_INDEX_DEATHS]);
            data.SetInt("hs", stats[STATS_INDEX_HS]);
            data.SetInt("score", CS_GetClientContributionScore(i));

            int team = GetClientTeam(i);
            if (team == CS_TEAM_T)
            {
                clientsT.PushObject(data);
            }
            else if (team == CS_TEAM_CT)
            {
                clientsCt.PushObject(data);
            }
        }
    }

    JSON_Object teamT = new JSON_Object();
    JSON_Object teamCt = new JSON_Object();

    // Teams score has to be swapped here
    
    teamT.SetInt("score", GetTeamScore(CS_TEAM_CT));
    teamT.SetObject("clients", clientsT);

    teamCt.SetInt("score", GetTeamScore(CS_TEAM_T));
    teamCt.SetObject("clients", clientsCt);

    JSON_Object scores = new JSON_Object();
    scores.SetObject("teamTerrorists", teamT);
    scores.SetObject("teamCounterTerrorists", teamCt);
    
    return scores;
}

public void GetClientSteamId(int client, char[] output, int maxlen)
{
    GetClientAuthId(client, AuthId_SteamID64, output, maxlen);
}

public void GetClientStats(int client, int[] stats)
{
    char name[3];
    IntToString(client, name, sizeof(name));

    if (!g_scoreboard.GetArray(name, stats, STATS_SIZE))
    {
        ClearClientStats(client);
        GetClientStats(client, stats);
    }
}

public void SetClientStats(int client, int[] stats)
{
    char name[3];
    IntToString(client, name, sizeof(name));

    g_scoreboard.SetArray(name, stats, STATS_SIZE, true);
}

public void ClearClientStats(int client)
{
    char name[3];
    IntToString(client, name, sizeof(name));

    int stats[STATS_SIZE];
    g_scoreboard.SetArray(name, stats, STATS_SIZE)
}
