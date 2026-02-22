namespace Eclipse.Loader
{
    using System;
    using System.Threading;
    using UnityEngine; 
    using HarmonyLib;
    using System.IO;
    using Steamworks;
    using Eclipse.API.Features;
    using Steamworks.Data;
    using System.Linq;

    public class Server
    {
        private static bool consoleOpened = false;
        private static Thread consoleThread;

        public static void Start()
        {
            if (consoleOpened) return;
            consoleOpened = true;

            AllocConsole();

            var standardOutput = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);
            var standardInput = new StreamReader(Console.OpenStandardInput());
            Console.SetIn(standardInput);

            Console.Title = "[Eclipse] Server Console";
            Console.WriteLine("[Eclipse.Loader] Server console started.");

            consoleThread = new Thread(ConsoleLoop);
            consoleThread.IsBackground = true;
            consoleThread.Start();
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        private static void ConsoleLoop()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                    continue;

                HandleCommand(input.ToLower());
            }
        }

        private static void HandleCommand(string command)
        {
            switch (command)
            {
                case "stop":
                    Log.Special("[Eclipse.Loader] Stopping server...");
                    Application.Quit();

                    break;

                case "players":
                    Log.Special("[Eclipse.Loader] Online Players:");

                    foreach (var p in Player.List)
                    {
                        if (p == null) continue;
                        var name = Player.GetPlayerName(p);
                        Log.Special($"- {name}");
                    }

                    break;

                case "clear":
                    Console.Clear();
                    break;

                case "forcestart":
                    if (Round.GameState == Bootstrap.GameState.InLobby)
                    {
                        try
                        {
                            GameProgressionManager.Instance.StartGameRpc();
                            Log.Special("[Eclipse.Loader] Forced round start.");
                        }
                        catch (Exception ex)
                        {
                            Log.Special($"[Eclipse.Loader] Failed to force start round: {ex}");

                        }
                    }
                    else Log.Special("[Eclipse.Loader] Round is already started.");
                    break;

                case "help":
                    Log.Special("[Eclipse.Loader] Available commands:");
                    Log.Special("- stop: Stop the server");
                    Log.Special("- players: List online players");
                    Log.Special("- clear: Clear the console");
                    Log.Special("- help: Show this help message");
                    break;

                default:
                    Log.Special("[Eclipse.Loader] Unknown command.");
                    break;
            }
        }
    }
}
   
