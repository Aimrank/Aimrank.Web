#include <sourcemod>
#include <cstrike>

#define CLIENT_NAME_LENGTH 33
#define CLIENT_ID_LENGTH 18

#define MAX_CLIENTS 4

ConVar g_aimrankWhitelist;

public Plugin myinfo =
{
    name = "Aimrank Teamlock",
    author = "Aimrank",
    description = "Joining palyers to correct teams automatically and prevent team switching.",
    version = "1.0.0",
    url = ""
};

public void OnPluginStart()
{
    g_aimrankWhitelist = CreateConVar("aimrank_whitelist", "0", "SteamID list of whitelisted players.");

    AddCommandListener(Command_JoinTeam, "jointeam");
    
    HookUserMessage(GetUserMessageId("VGUIMenu"), OnTeamMenu, true);
}

public void OnClientPutInServer(int client)
{
    if (IsFakeClient(client))
    {
        return;
    }

    char clientId[CLIENT_ID_LENGTH];
    char whitelist[CLIENT_ID_LENGTH * MAX_CLIENTS];

    GetClientSteamId(client, clientId, sizeof(clientId));
    GetConVarString(g_aimrankWhitelist, whitelist, sizeof(whitelist));

    if (StrContains(whitelist, clientId) == -1)
    {
        KickClient(client, "You are not whitelisted");
    }
}

public Action OnTeamMenu(UserMsg msg_id, Protobuf msg, const int[] players, int playersNum, bool reliable, bool init)
{
    char buffer[64];
    PbReadString(msg, "name", buffer, sizeof(buffer));

    if (StrEqual(buffer, "team", true))
    {
        int client = players[0];

        CreateTimer(0.1, OnTeamMenuTimer, client);
    }

    return Plugin_Continue;
}

public Action OnTeamMenuTimer(Handle timer, any client)
{
    CS_SwitchTeam(client, GetClientTeamFromCVar(client));
}

public Action Command_JoinTeam(int client, const char[] command, int argc)
{
    if (!client || !IsClientInGame(client) || IsFakeClient(client))
    {
        return Plugin_Continue;
    }

    char teamString[4];
    GetCmdArg(1, teamString, sizeof(teamString));

    int team = GetClientTeamFromCVar(client);
    int team_to = StringToInt(teamString);

    if (team_to != team)
    {
        return Plugin_Handled;
    }

    return Plugin_Continue;
}

public int GetClientTeamFromCVar(int client)
{
    return CS_TEAM_CT;
}

public void GetClientSteamId(int client, char[] output, int maxlen)
{
    GetClientAuthId(client, AuthId_SteamID64, output, maxlen);
}