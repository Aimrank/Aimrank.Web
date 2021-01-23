#include <sourcemod>
#include <sdktools>
#include <cstrike>
#include <system2>
#include <json>

#define M_IFRAGS 4044
#define M_IDEATHS 4052

#define CLIENT_NAME_LENGTH 33       // 32 + 1
#define CLIENT_ID_LENGTH 19         // 18 + 1

#define MAX_CLIENTS 2

ConVar g_aimrankServerId;
ConVar g_aimrankWhitelist;

public Plugin myinfo =
{
    name = "Aimrank",
    author = "Mariusz Baran",
    description = "Aimrank plugin that sends game events to web application.",
    version = "1.0.0",
    url = ""
};

public void OnPluginStart()
{
    g_aimrankServerId = CreateConVar("aimrank_server_id", "00000000-0000-0000-0000-000000000000", "Aimrank server identifier");
    g_aimrankWhitelist = CreateConVar("aimrank_whitelist", "0", "SteamID list of whitelisted players.");
    
    HookEvent("cs_win_panel_match", Event_MatchEnd);
    HookEvent("round_end", Event_PublishScoreboard);
    HookEvent("round_start", Event_PublishScoreboard);
    HookEvent("player_activate", Event_PublishScoreboard);
    HookEvent("player_disconnect", Event_PublishScoreboard);
}

public void PublishEvent(JSON_Object data)
{
    char event[1024];
    char command[1024];
    char serverId[64];

    GetConVarString(g_aimrankServerId, serverId, sizeof(serverId));

    data.Encode(event, sizeof(event));

    Format(command, sizeof(command), "cat << EVENTDATA | /home/app/Aimrank.BusPublisher %s\n%s\nEVENTDATA", serverId, event);
    
    PrintToServer(event);
    PrintToServer(command);

    System2_ExecuteThreaded(PublishEvent_Executed, command);

    data.Cleanup();
}

public void PublishEvent_Executed(bool success, const char[] command, System2ExecuteOutput output)
{
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

            GetClientName(i, clientName, sizeof(clientName));
            GetClientAuthId(i, AuthId_Steam2, clientAuth, sizeof(clientAuth));

            data.SetString("name", clientName);
            data.SetString("steamId", clientAuth);
            data.SetInt("kills", GetEntProp(i, Prop_Data, "m_iFrags"));
            data.SetInt("deaths", GetEntProp(i, Prop_Data, "m_iDeaths"));
            data.SetInt("assists", CS_GetClientAssists(i));
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

    teamT.SetInt("score", GetTeamScore(CS_TEAM_T));
    teamT.SetObject("clients", clientsT);

    teamCt.SetInt("score", GetTeamScore(CS_TEAM_CT));
    teamCt.SetObject("clients", clientsCt);

    JSON_Object scoreboard = new JSON_Object();
    scoreboard.SetObject("teamTerrorists", teamT);
    scoreboard.SetObject("teamCounterTerrorists", teamCt);
    
    return scoreboard;
}

public JSON_Object CreateIntegrationEvent(const char[] name, JSON_Object data)
{
    JSON_Object event = new JSON_Object();
    event.SetString("name", name);
    event.SetObject("data", data);
    return event;
}

public Action Event_PublishScoreboard(Event event, const char[] name, bool dontBroadcast)
{
    PublishEvent(CreateIntegrationEvent("scoreboard_changed", GetScoreboard()));
}

public Action Event_MatchEnd(Event event, const char[] name, bool dontBroadcast)
{
    PublishEvent(CreateIntegrationEvent("match_end", GetScoreboard()));
}

public void OnClientPutInServer(int client)
{
    if (IsFakeClient(client))
    {
        return;
    }

    char clientId[CLIENT_ID_LENGTH];
    char whitelist[CLIENT_ID_LENGTH * MAX_CLIENTS];

    GetClientAuthId(client, AuthId_Steam2, clientId, sizeof(clientId));
    GetConVarString(g_aimrankWhitelist, whitelist, sizeof(whitelist));

    if (StrContains(whitelist, clientId) == -1)
    {
        KickClient(client, "You are not whitelisted");
    }
}

// When warmup ends and not all players are connected - cancel the game