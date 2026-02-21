# Eclipse
Eclipse is a plugin framework for Burglin Gnomes Servers, All Eclipse events are coded with Harmony. The framework has not been released yet, but will be released in a couple of days.

## For Developers
The advantage of using Eclipse is that it is simpler. Here is an example of a plugin:
```cs
public class Main : IPlugin
{
    public string Name => "Eclipse.Events";
    public string Author { get; set; } = "SpuderMANs";
    public Version Version { get; set; } = new Version(1, 0, 0);

    public void OnEnabled()
    {
        Log.Info("Eclipse.Events has been enabled!");
        Events.Handlers.Player.Joined += OnPlayerJoined;  
    }

    public void OnDisabled()
    {
        Events.Handlers.Player.Joined -= OnPlayerJoined;
    }

    private void OnPlayerJoined(JoinedEventArgs ev)
    {
        Coroutine.CallDelayed(0.5f, () =>
        {
            Log.Info($"Player {ev.Player.DisplayedNickname} has joined the game!");
            ev.Player.Show("Welcome to the game!", 5f);
        });
    }
}
```
