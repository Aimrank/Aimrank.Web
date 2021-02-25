public bool IsClientWhitelisted(int client)
{
    char clientId[CLIENT_ID_LENGTH];
    char[] whitelist = new char[CLIENT_WHITELIST_ENTRY_LENGTH * g_maxClients];

    GetClientSteamId(client, clientId);
    GetConVarString(g_whitelist, whitelist, CLIENT_WHITELIST_ENTRY_LENGTH * g_maxClients);

    return StrContains(whitelist, clientId) != -1;
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

Action OnTeamMenuTimer(Handle timer, any client)
{
    CS_SwitchTeam(client, GetClientTeamFromCVar(client));
}

int GetClientTeamFromCVar(int client)
{
    int length = CLIENT_WHITELIST_ENTRY_LENGTH * g_maxClients;

    char clientId[CLIENT_ID_LENGTH];
    char[] whitelist = new char[length];

    GetClientSteamId(client, clientId);
    GetConVarString(g_whitelist, whitelist, length);

    int index = StrContains(whitelist, clientId);
    if (index == -1)
    {
        return CS_TEAM_CT;
    }

    int team = StringToInt(whitelist[index + 18]);
    
    int score1 = CS_GetTeamScore(CS_TEAM_T);
    int score2 = CS_GetTeamScore(CS_TEAM_CT);

    int maxRounds = GetConVarInt(g_maxRounds);

    if (score1 + score2 >= maxRounds / 2)
    {
        team = team == CS_TEAM_T ? CS_TEAM_CT : CS_TEAM_T;
    }

    return team;
}

public void ClientConnected(int client)
{
    char steamId[CLIENT_ID_LENGTH];
    GetClientSteamId(client, steamId);

    g_clients.SetValue(steamId, true);

    if (g_gamePaused)
    {
        int clients = 0;

        for (int i = 1; i <= MaxClients; i++)
        {
            if (IsClientOnList(i))
            {
                clients++;
            }
        }

        if (clients == g_maxClients)
        {
            PrintToChatAll("All clients connected. Unpausing game.");
            Unpause();
        }
    }
}

public void ClientDisconnected(int client)
{
    char steamId[CLIENT_ID_LENGTH];
    GetClientSteamId(client, steamId);

    g_clients.SetValue(steamId, false);

    if (!g_gamePaused)
    {
        Pause();
    }
}

bool IsClientOnList(int client)
{
    char steamId[CLIENT_ID_LENGTH];
    GetClientSteamId(client, steamId);

    bool isClientOnList;

    g_clients.GetValue(steamId, isClientOnList);

    return isClientOnList;
}