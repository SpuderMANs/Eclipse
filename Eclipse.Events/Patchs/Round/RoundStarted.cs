namespace Eclipse.Events.Patchs.Player
{
    using HarmonyLib;
    using Eclipse.Events.Handlers;
    using Steamworks.Data;
    using System;
    using Eclipse.API.Features;
    using Round = Eclipse.Events.Handlers.Round;

    [HarmonyPatch(typeof(Bootstrap), nameof(Bootstrap.StartGame))]
    internal class RoundStartedPatch
    {
        private static void Postfix(string sceneName)
        {
            try
            {
                API.Features.Round round = new API.Features.Round();
                Handlers.Round.InvokeRoundStarted();
            }
            catch (Exception e)
            {
                Log.Error("Failed to invoke RoundStarted event: " + e);
            }
        }
    }
}