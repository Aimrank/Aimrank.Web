#include <sourcemod>
#include <sdktools>
#include <cstrike>
#include <system2>
#include <json>

ConVar g_aimrankServerId;

#define M_IFRAGS 4044
#define M_IDEATHS 4052

public Plugin myinfo =
{
    name = "Custom",
    author = "",
    description = "Custom",
    version = "1.0.0",
    url = ""
};

public void OnPluginStart()
{
    g_aimrankServerId = CreateConVar("aimrank_server_id", "00000000-0000-0000-0000-000000000000", "Aimrank server identifier");

    PrintToServer("[Stats] Plugin started: v.1.0.1");
    
    HookEvent("round_end", Event_RoundEnd);
    HookEvent("player_connect", Event_PlayerConnect);
    HookEvent("player_disconnect", Event_PlayerDisconnect);
}

public void PublishEvent(JSON_Object data)
{
    decl String:serverId[64];
    GetConVarString(g_aimrankServerId, serverId, sizeof(serverId));

    decl String:event[1024];
    decl String:command[1024];

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

            char clientName[128];

            GetClientName(i, clientName, sizeof(clientName));

            data.SetString("name", clientName);
            data.SetInt("kills", GetEntData(i, M_IFRAGS));
            data.SetInt("deaths", GetEntData(i, M_IDEATHS));

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

public Action Event_RoundEnd(Event event, const char[] name, bool dontBroadcast)
{
    JSON_Object data = new JSON_Object();
    data.SetString("eventName", name);
    data.SetObject("scoreboard", GetScoreboard());
    PublishEvent(data);
}

public Action Event_PlayerConnect(Event event, const char[] name, bool dontBroadcast)
{
    decl String:username[128];
    event.GetString("name", username, sizeof(username));

    JSON_Object data = new JSON_Object();
    data.SetString("event_name", name);
    data.SetString("name", username);
    PublishEvent(data);
}

public Action Event_PlayerDisconnect(Event event, const char[] name, bool dontBroadcast)
{
    decl String:username[128];
    event.GetString("name", username, sizeof(username));

    JSON_Object data = new JSON_Object();
    data.SetString("event_name", name);
    data.SetString("name", username);
    PublishEvent(data);
}