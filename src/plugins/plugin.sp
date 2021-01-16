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
    HookEvent("player_death", Event_PlayerDeath);
}

public void PublishEvent(char[] content, any ...)
{
  char[] event = new char[512];
  char[] command = new char[512];

  VFormat(event, 512, content, 2);
  Format(command, 512, "cat << EVENTDATA | /home/steam/csgo/DBus\n%s\nEVENTDATA", event);

  System2_ExecuteThreaded(PublishEvent_Executed, command);
}

public void PublishEvent_Executed(bool success, const char[] command, System2ExecuteOutput output)
{
}

public Action Event_RoundEnd(Event event, const char[] name, bool dontBroadcast)
{
    int reason = event.GetInt("reason");
    int winner = event.GetInt("winner");

    PublishEvent("{\"event\": \"round_end\", \"winner\": \"%d\", \"reason\": \"%d\"}", winner, reason);
}

public Action Event_PlayerDeath(Event event, const char[] name, bool dontBroadcast)
{
    int player = event.GetInt("userid");
    int attacker = event.GetInt("attackerid");

    PublishEvent("{\"event\": \"player_death\", \"playerId\": \"%d\", \"attackerId\": \"%d\"}", player, attacker);
}
