#include <sourcemod>
#include <cstrike>

#define CLIENT_WHITELIST_ENTRY_LENGTH 20    // {steam id 64}:{team index}

#define MAX_CLIENTS 4

ConVar g_aimrankWhitelist;
ConVar g_mpMaxRounds;

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
    g_mpMaxRounds = FindConVar("mp_maxrounds");

    AddCommandListener(Command_JoinTeam, "jointeam");
    
    HookUserMessage(GetUserMessageId("VGUIMenu"), OnTeamMenu, true);
}

public void OnClientPutInServer(int client)
{
    if (IsFakeClient(client))
    {
        return;
    }

    char clientId[CLIENT_WHITELIST_ENTRY_LENGTH];
    char whitelist[CLIENT_WHITELIST_ENTRY_LENGTH * MAX_CLIENTS];

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

    return Plugin_Handled;
}

public int GetClientTeamFromCVar(int client)
{
    char clientId[CLIENT_WHITELIST_ENTRY_LENGTH];
    char whitelist[CLIENT_WHITELIST_ENTRY_LENGTH * MAX_CLIENTS];

    GetClientSteamId(client, clientId, sizeof(clientId));
    GetConVarString(g_aimrankWhitelist, whitelist, sizeof(whitelist));

    int index = StrContains(whitelist, clientId);
    if (index == -1)
    {
        return CS_TEAM_CT;
    }

    int team = StringToInt(whitelist[index + 18]);
    
    int score1 = CS_GetTeamScore(CS_TEAM_T);
    int score2 = CS_GetTeamScore(CS_TEAM_CT);

    int maxRounds = GetConVarInt(g_mpMaxRounds);

    if (score1 + score2 >= maxRounds / 2)
    {
        team = team == CS_TEAM_T ? CS_TEAM_CT : CS_TEAM_T;
    }

    return team;
}

public void GetClientSteamId(int client, char[] output, int maxlen)
{
    GetClientAuthId(client, AuthId_SteamID64, output, maxlen);
}