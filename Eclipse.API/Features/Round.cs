namespace Eclipse.API.Features
{
    using RootMotion.Dynamics;
    using Steamworks;
    using Steamworks.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Text;
    using System.Threading.Tasks;

    public class Round 
    {
        public static float ElapsedTime => GameProgressionManager.Instance.RoundTimeInSeconds;
        public static bool IsStarted => GameProgressionManager.Instance.didStart;
        public static Bootstrap.GameState GameState => GameProgressionManager.Instance.CurrentGameState;
        public static bool IsTutorialMode => GameProgressionManager.Instance.TutorialMode;
        public static int CurrentDay => GameProgressionManager.Instance.CurrentDay;
        public static int MaxPlayers
        {
            get => Bootstrap.c_maxPlayers;
            set => Bootstrap.c_maxPlayers = value;          
        }
        public static string LobbyID => LobbyManager.instance.Id.ToString();

        public static void Stop() => GameProgressionManager.Instance.EndGameRpc();

    }
}
