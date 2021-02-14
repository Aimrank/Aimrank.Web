#include <sourcemod>
#include <sdktools>
#include <cstrike>
#include <system2>
#include <json>

#define M_IFRAGS 4044
#define M_IDEATHS 4052

#define CLIENT_NAME_LENGTH 33
#define CLIENT_ID_LENGTH 18

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
    g_aimrankServerId = CreateConVar("aimrank_server_id", "00000000-0000-0000-0000-000000000000", "Aimrank server identifier");
    
    HookEvent("cs_win_panel_match", Event_MatchEnd);
}

public void OnMapStart()
{
    PublishEvent(CreateIntegrationEvent("map_start", null));
}

public Action Event_MatchEnd(Event event, const char[] name, bool dontBroadcast)
{
    PublishEvent(CreateIntegrationEvent("match_end", GetScoreboard()));
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

            GetClientName(i, clientName, sizeof(clientName));
            GetClientSteamId(i, clientAuth, sizeof(clientAuth));

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

public void GetClientSteamId(int client, char[] output, int maxlen)
{
    GetClientAuthId(client, AuthId_SteamID64, output, maxlen);
}
