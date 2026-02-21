namespace Eclipse.Example
{
    using Eclipse.API.Features;
    using Eclipse.API.Interfaces;
    using Eclipse.Events.Handlers;
    using Eclipse.Events.EventArgs.Player;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Eclipse.Events.EventArgs.Round;
    using Player = Eclipse.API.Features.Player;

    public class Main : IPlugin
    {
        public string Name => "Eclipse.Events";
        public string Author { get; set; } = "SpuderMANs";
        public Version Version { get; set; } = new Version(1, 0, 0);

        public void OnEnabled()
        {
            Log.Info("Eclipse.Events has been enabled!");
            SubscribeEvent();
        }
        public void OnDisable()
        {

        }

        public void SubscribeEvent()
        {
            // Round Events
            Events.Handlers.Round.RoundStarted += OnRoundStarted;
            Events.Handlers.Round.RoundEnded += OnRoundEnded;
            // Player Events
            Events.Handlers.Player.Joined += OnPlayerJoined;    
            Events.Handlers.Player.Left += OnPlayerLeft;
            Events.Handlers.Player.Died += OnPlayerDied;
            Events.Handlers.Player.Dying += OnPlayerDying;
        }

        private void OnPlayerJoined(JoinedEventArgs ev)
        {
            Coroutine.CallDelayed(0.5f, () =>
            {
                Log.Info($"Player {ev.Player.DisplayedNickname} has joined the game!");
                ev.Player.Show("Welcome to the game!", 5F);
            });
        }
        private void OnPlayerLeft(LeftEventArgs ev)
        {
            Coroutine.CallDelayed(0.5f, () =>
            {
                Log.Info($"Player {ev.Player.DisplayedNickname} has left the game!");
            });
        }
        private void OnPlayerDied(DiedEventArgs ev)
        {
            Coroutine.CallDelayed(0.5f, () =>
            {
                Log.Info($"Player {ev.Player.DisplayedNickname} has died!");
                ev.Player.Show("You have died!", 5F);
            });
        }
        private void OnPlayerDying(DyingEventArgs ev)
        {
            Coroutine.CallDelayed(0.5f, () =>
            {
                 Log.Info($"Player {ev.Player.DisplayedNickname} is dying!");
                 ev.Player.Show("You are dying!", 5F);
             });
        }

        private void OnRoundStarted(RoundStartedEventArgs ev)
        {
            Log.Info("Round has started!");
            foreach (Player player in Player.List)
            {
                player.Show("Round has started!", 5F);
            }
        }
        private void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Log.Info("Round has ended!");
            foreach (Player player in Player.List)
            {
                player.Show("Round has ended!", 5F);
            }
        }
    }
}
