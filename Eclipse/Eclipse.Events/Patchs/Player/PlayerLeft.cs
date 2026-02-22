namespace Eclipse.Events.Patchs.Player
{
    using HarmonyLib;
    using Eclipse.Events.Handlers;
    using Steamworks.Data;
    using System;
    using Eclipse.API.Features;
    using Round = Eclipse.Events.Handlers.Round;
    using Player = API.Features.Player;

    [HarmonyPatch(typeof(PlayerNetworking), nameof(PlayerNetworking.OnNetworkDespawn))]
    internal class Left
    {
        private static void Postfix(PlayerNetworking __instance)
        {
            try
            {
                var player = Player.GetByNetworking(__instance);
                Handlers.Player.InvokeLeft(player);
            }
            catch (Exception e)
            {
                Log.Error("Failed to invoke Left event: " + e);
            }
        }
    }
}