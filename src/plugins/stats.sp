#include <sourcemod>

public Plugin myinfo =
{
    name = "Stats",
    author = "",
    description = "Stats",
    version = "1.0",
    url = ""
};

public void OnPluginStart()
{
    PrintToServer("[Stats] Plugin started.");
    
    HookEvent("round_end", Event_RoundEnd);
    HookEvent("player_death", Event_PlayerDeath);
}

public Action Event_RoundEnd(Event event, const char[] name, bool dontBroadcast)
{
    PrintToChatAll("Hehe Round End");
    int winner = event.GetInt("winner");
    int reason = event.GetInt("reason");
    PrintToChatAll("Winner %d, Reason %d", winner, reason);
}

public Action Event_PlayerDeath(Event event, const char[] name, bool dontBroadcast)
{
    int playerId = event.GetInt("userid");
    int attackerId = event.GetInt("attackerid");
    bool headshot = event.GetBool("headshot");

    PrintToChatAll("Player: %d", playerId)
    PrintToChatAll("Attacker: %d", attackerId)

    PrintToChatAll("Player '%d' killed '%d'%s",
        playerId,
        attackerId,
        headshot ? " with headshot" : "");

    File file = OpenFile("/table.txt", "w");

    int data_p1[2];
    data_p1[0] = attackerId;
    data_p1[1] = 1;

    int data_p2[2];
    data_p2[0] = playerId;
    data_p2[1] = -1;

    file.Write(data_p1, 2, 4);
    file.Write(data_p2, 2, 4);

    file.Close();
}
