namespace Eclipse.Events.Handlers
{
    using System;
    using System.Security.Cryptography;
    using Eclipse.API.Features;
    using Eclipse.Events.EventArgs.Interfaces;
    using Eclipse.Events.EventArgs.Player;
    using Eclipse.Events.EventArgs.Round;
    using Eclipse.Events.Features;
    using Eclipse.Events.Patchs.Player;
    using Steamworks;
    using Steamworks.Data;
    using UnityEngineInternal;

    public static class Round
    {
        public static Event<RoundStartedEventArgs> RoundStarted = new Event<RoundStartedEventArgs>();
        public static Event<RoundEndedEventArgs> RoundEnded = new Event<RoundEndedEventArgs>();  

        internal static void InvokeRoundStarted()
        {
            RoundStarted.Invoke(new RoundStartedEventArgs());
        }
        internal static void InvokeRoundEnded()
        {
            RoundEnded.Invoke(new RoundEndedEventArgs());
        }
    }
}