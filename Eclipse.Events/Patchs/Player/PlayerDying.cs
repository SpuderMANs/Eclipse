namespace Eclipse.Events.Patchs.Player
{
    using HarmonyLib;
    using Eclipse.Events.Handlers;
    using Steamworks.Data;
    using System;
    using Eclipse.API.Features;
    using Round = Eclipse.Events.Handlers.Round;
    using Player = API.Features.Player;

    [HarmonyPatch(typeof(HealthBase), "OnDied")]
    internal class Died
    {
        private static void Postfix(HealthBase __instance)
        {
            try
            {
                var networking = __instance.GetComponent<PlayerNetworking>();
                if (networking == null)
                    return;
                var player = Player.GetByNetworking(networking);

                Handlers.Player.InvokeDied(player);
            }
            catch (Exception e)
            {
                Log.Error($"Failed to invoke Dying event: {e}");
            }
        }
    }
}