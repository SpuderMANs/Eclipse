# Eclipse
Eclipse is a plugin framework for Burglin Gnomes Servers, All Eclipse events are coded with Harmony.

## How To Install
Download `Assembly-CSharp.dll` and place it in the folder `C:\Program Files (x86)\Steam\steamapps\common\Burglin Gnomes Demo\Gnomium_Data\Managed`. Do the same with `Eclipse.dll` and `Eclipse.API.dll`.

When you start the game, an “Eclipse” folder will be created in `C:\Users\YourUser\AppData\Roaming\Eclipse`. Open the folder and place `Eclipse.Events.dll` and any plugins you want that support the Eclipse Framework in the Plugins subfolder.

## For Developers
The advantage of using Eclipse is that it is simpler. Here is an example of a plugin:
```cs
public class Main : IPlugin
{
    public string Name => "Good Plugin";
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
