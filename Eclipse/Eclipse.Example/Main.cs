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
    using Item = Eclipse.API.Features.Item;
    using UnityEngine.Assertions.Must;
    using Lightbug.CharacterControllerPro.Core;

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
            UnSubscribeEvent();
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
            Events.Handlers.Player.GrabbedObject += OnGrabbedObject;
            Events.Handlers.Player.TaskCompleted += OnTaskCompleted;
            // Item Events
            Events.Handlers.Player.ItemAdded += OnItemAdded;
        }
        public void UnSubscribeEvent()
        {
            // Round Events
            Events.Handlers.Round.RoundStarted -= OnRoundStarted;
            Events.Handlers.Round.RoundEnded -= OnRoundEnded;
            // Player Events
            Events.Handlers.Player.Joined -= OnPlayerJoined;
            Events.Handlers.Player.Left -= OnPlayerLeft;
            Events.Handlers.Player.Died -= OnPlayerDied;
            Events.Handlers.Player.Dying -= OnPlayerDying;
            Events.Handlers.Player.GrabbedObject -= OnGrabbedObject;
            Events.Handlers.Player.TaskCompleted -= OnTaskCompleted;
            // Item Events
            Events.Handlers.Player.ItemAdded -= OnItemAdded;
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
            Coroutine.CallDelayed(1f, () =>
            {
                foreach (Player player in Player.List)
                {

                    player.Show("Round has started!", 15f);
                    player.MaxStamina = float.MaxValue;
                    player.Stamina = float.MaxValue;
                }
            });
            
        }
        private void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Log.Info("Round has ended!");
            foreach (Player player in Player.List)
            {
                player.Show("Round has ended!", 15F);
                
            }
        }
        private void OnItemAdded(ItemAddedEventArgs ev)
        {
            var type = ev.Item.Type;
            Log.Info(type.ToString());
            Log.Info(ev.Item.MaxDurability.ToString());
            Log.Info(ev.Item.ToString());
        }
        private void OnGrabbedObject(GrabbedObjectEventArgs ev)
        {
            Log.Info($"Player {ev.Player.DisplayedNickname} has grabbed an object: {ev.Object.NetworkObject.name}!");
            Log.Info($"{ev.Object.transform.position}");
            Coroutine.CallDelayed(0.5f, () =>
            {
                ev.Player.Show($"You have grabbed an object: {ev.Object.NetworkObject.name}!", 5F, screenLocationType: API.Enums.ScreenLocationType.Corner);
            });
        }
        private void OnTaskCompleted(TaskCompletedEventArgs ev)
        {
            Log.Info($"{ev.Player.DisplayedNickname} completed {ev.Task.name}, difficulty {ev.Task.Difficulty.ToString()}");
        }
    }
}
