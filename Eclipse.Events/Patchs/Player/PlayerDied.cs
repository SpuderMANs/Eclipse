namespace Eclipse.Events.Patchs.Player
{
    using HarmonyLib;
    using Eclipse.Events.Handlers;
    using Steamworks.Data;
    using System;
    using Eclipse.API.Features;
    using Round = Eclipse.Events.Handlers.Round;
    using Player = API.Features.Player;
    using Eclipse.Events.EventArgs.Player;

    [HarmonyPatch(typeof(HealthBase), nameof(HealthBase.TakeDamage))]
    internal class Dying
    {
        private static void Prefix(HealthBase __instance, float amount)
        {
            try
            {
                if (__instance.Health - amount > 0f)
                    return;

                if (__instance.Dead)
                    return;

                var networking = __instance.GetComponent<PlayerNetworking>();
                if (networking == null)
                    return;

                var player = Player.GetByNetworking(networking);

                Handlers.Player.InvokeDying(player);
            }
            catch (Exception e)
            {
                Log.Error($"Failed to invoke Dying event: {e}");
            }
        }
    }
}