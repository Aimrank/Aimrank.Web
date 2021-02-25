public void PublishEvent(JSON_Object data)
{
    char event[1024];
    char serverId[64];

    GetConVarString(g_serverId, serverId, sizeof(serverId));

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

// 0 - get winner from scores
// 1 - draw
// 2 - t
// 3 - ct
public JSON_Object GetScoreboard(int winner = 0)
{
    JSON_Array clientsT = new JSON_Array();
    JSON_Array clientsCt = new JSON_Array();

    for (int i = 1; i <= MaxClients; i++)
    {
        if (IsClientConnected(i) && IsClientInGame(i))
        {
            JSON_Object data = new JSON_Object();

            char steamId[18];
            int stats[STATS_SIZE];

            GetClientStats(i, stats);
            GetClientSteamId(i, steamId);

            data.SetString("steamId", steamId);
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

    int scoreT = GetTeamScore(CS_TEAM_CT);
    int scoreCT = GetTeamScore(CS_TEAM_T);

    teamT.SetInt("score", scoreT);
    teamT.SetObject("clients", clientsT);

    teamCt.SetInt("score", scoreCT);
    teamCt.SetObject("clients", clientsCt);

    JSON_Object scores = new JSON_Object();
    scores.SetObject("teamTerrorists", teamT);
    scores.SetObject("teamCounterTerrorists", teamCt);
    scores.SetInt("winner", winner > 0 ? winner : (scoreT > scoreCT ? CS_TEAM_T : (scoreCT > scoreT ? CS_TEAM_CT : 1)));
    
    return scores;
}