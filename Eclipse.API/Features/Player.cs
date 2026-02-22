namespace Eclipse.API.Features
{
    using Eclipse.API.Enums;
    using Eclipse.API.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Unity.Netcode;
    using UnityEngine;
    using Lightbug.CharacterControllerPro.Core;

    public class Player : PlayerNetworking
    {
        private static Dictionary<PlayerNetworking, string> playerNames = new Dictionary<PlayerNetworking, string>();
        private static readonly Dictionary<PlayerNetworking, Player> Players = new();


        public PlayerNetworking Base { get; }
        public CharacterActor CharacterActor => Base.Actor;

        public Player(PlayerNetworking player)
        {
            Base = player ?? throw new ArgumentNullException(nameof(player));
        }

        public static PlayerNetworking LocalPlayer => ServerManager.GetLocalPlayer();
        public PlayerHandController PlayerHandController => Base.GetComponent<PlayerHandController>();

        public static PlayerNetworking GetByID(ulong id) => ServerManager.GetPlayer(id);

        public static IReadOnlyList<Player> List =>
    UnityEngine.Object
        .FindObjectsByType<PlayerNetworking>(FindObjectsSortMode.None)
        .Where(p =>
            p != null &&
            p.NetworkObject != null &&
            p.NetworkObject.IsSpawned)
        .Select(GetByNetworking)
        .ToList();

        public static IReadOnlyList<InventoryBase.InventoryItem> GetInventoryItems(ulong playerId) => GetByID(playerId).Inventory.GetInventoryItems().ToList();

        public static InventoryBase.InventoryItem GetInventoryItem(ulong playerId, int slot) => GetByID(playerId).Inventory.GetItemInSlot(slot);

        public void Show(string message, float duration = 5f, ScreenLocationType screenLocationType = ScreenLocationType.Middle)
        {
            Hint.Show(this.Base, message, screenLocationType, duration);
        }

        public static string GetPlayerName(PlayerNetworking player)
        {
            if (player == null)
                return "Null";

            if (playerNames.TryGetValue(player, out string cached))
                return cached;

            if (player.SteamPlayer != null)
            {
                if (player.SteamPlayer.TryGetName(out string steamName) &&
                    !string.IsNullOrWhiteSpace(steamName))
                {
                    playerNames[player] = steamName;
                    return steamName;
                }
            }

            return "Unknown";
        }

        public static PlayerNetworking GetByName(string name)
        {
            foreach (var player in List)
            {
                if (GetPlayerName(player).Equals(name, StringComparison.OrdinalIgnoreCase))
                    return player;
            }
            return null;
        }

        public static Player GetByNetworking(PlayerNetworking networking)
        {
            if (networking == null)
                return null;
            if (Players.TryGetValue(networking, out var player))
                return player;
            player = new Player(networking);
            Players.Add(networking, player);

            return player;
        }

        public string DisplayedNickname => GetPlayerName(Base);
        public float Stamina
        {
            get => CharacterActor.CurrentStamina;
            set => ReflectionExtension.SetFieldSafe(CharacterActor, "currentStamina", value, BindingFlags.Instance | BindingFlags.NonPublic);      
        }
        public float MaxStamina
        {
            get => CharacterActor.maxStamina;
            set => CharacterActor.maxStamina = value;
        }
        public float MaxHandStretchDistance
        {
            get => PlayerHandController?.GetFieldSafe("maxHandStretchDistance", BindingFlags.Instance | BindingFlags.NonPublic) ?? 0f;
            set => PlayerHandController?.SetFieldSafe("maxHandStretchDistance", value, BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public float HandReachDistance
        {
            get => PlayerHandController?.GetFieldSafe("handReachDistance", BindingFlags.Instance | BindingFlags.NonPublic) ?? 0f;
            set => PlayerHandController?.SetFieldSafe("handReachDistance", value, BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public float HandBreakDistance
        {
            get => PlayerHandController?.GetFieldSafe("handBreakDistance", BindingFlags.Instance | BindingFlags.NonPublic) ?? 0f;
            set => PlayerHandController?.SetFieldSafe("handBreakDistance", value, BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public float OverStretchDuration
        {
            get => PlayerHandController?.GetFieldSafe("overStretchDuration", BindingFlags.Instance | BindingFlags.NonPublic) ?? 0f;
            set => PlayerHandController?.SetFieldSafe("overStretchDuration", value, BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public float HandStretchSpeed
        {
            get => PlayerHandController?.GetFieldSafe("handStretchSpeed", BindingFlags.Instance | BindingFlags.NonPublic) ?? 0f;
            set => PlayerHandController?.SetFieldSafe("handStretchSpeed", value, BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}