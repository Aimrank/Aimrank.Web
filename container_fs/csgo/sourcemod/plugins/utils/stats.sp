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