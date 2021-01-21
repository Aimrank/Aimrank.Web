#include <sourcemod>
#include <system2>

ConVar g_aimrankServerId;

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

    PrintToServer("[Stats] Plugin started: v.1.0.0");
    
    HookEvent("round_end", Event_RoundEnd);
    HookEvent("round_start", Event_RoundStart);
    HookEvent("player_death", Event_PlayerDeath);
    HookEvent("player_hurt", Event_PlayerHurt);
    HookEvent("player_connect", Event_PlayerConnect);
}

public void PublishEvent(char[] content, any ...)
{
    decl String:value[64]
    GetConVarString(g_aimrankServerId, value, sizeof(value));

    char[] event = new char[512];
    char[] command = new char[512];

    VFormat(event, 512, content, 2);
    Format(command, 512, "cat << EVENTDATA | /home/app/Aimrank.BusPublisher %s\n%s\nEVENTDATA", value, event);

    System2_ExecuteThreaded(PublishEvent_Executed, command);
}

public void PublishEvent_Executed(bool success, const char[] command, System2ExecuteOutput output)
{
}

public Action Event_RoundStart(Event event, const char[] name, bool dontBroadcast)
{
    int timelimit = event.GetInt("timelimit");
    int fraglimit = event.GetInt("fraglimit");

    PublishEvent("{\"name\": \"round_start\", \"timelimit\": %d, \"fraglimit\": %d}", timelimit, fraglimit);
}

public Action Event_RoundEnd(Event event, const char[] name, bool dontBroadcast)
{
    int reason = event.GetInt("reason");
    int winner = event.GetInt("winner");

    PublishEvent("{\"name\": \"round_end\", \"winner\": %d, \"reason\": %d}", winner, reason);
}

public Action Event_PlayerDeath(Event event, const char[] name, bool dontBroadcast)
{
    int player = event.GetInt("userid");
    int attacker = event.GetInt("attackerid");

    PublishEvent("{\"name\": \"player_death\", \"playerId\": %d, \"attackerId\": %d}", player, attacker);
}

public Action Event_PlayerHurt(Event event, const char[] name, bool dontBroadcast)
{
    int player = event.GetInt("userid");
    int attacker = event.GetInt("attackerid");
    int dmgHealth = event.GetInt("dmg_health");

    PublishEvent("{\"name\": \"player_hurt\", \"playerId\": %d, \"attackerId\": %d, \"dmgHealth\": %d}", player, attacker, dmgHealth);
}

public Action Event_PlayerConnect(Event event, const char[] name, bool dontBroadcast)
{
    decl String:username[128];

    event.GetString("name", username, sizeof(username));

    PublishEvent("{\"name\": \"player_connect\", \"name\": \"%s\"}", username);
}