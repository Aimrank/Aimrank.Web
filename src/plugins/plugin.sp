#include <sourcemod>
#include <system2>

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
    PrintToServer("[Stats] Plugin started: v.1.0.0");
    
    HookEvent("round_end", Event_RoundEnd);
    HookEvent("round_start", Event_RoundStart);
    HookEvent("player_death", Event_PlayerDeath);
    HookEvent("player_hurt", Event_PlayerHurt);
}

public void PublishEvent(char[] content, any ...)
{
  char[] event = new char[512];
  char[] command = new char[512];

  VFormat(event, 512, content, 2);
  Format(command, 512, "cat << EVENTDATA | /home/app/Aimrank.EventBus.Client\n%s\nEVENTDATA", event);

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