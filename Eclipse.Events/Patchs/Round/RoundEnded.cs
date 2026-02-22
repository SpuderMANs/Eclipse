namespace Eclipse.Events.Patchs.Player
{
    using HarmonyLib;
    using Eclipse.Events.Handlers;
    using Steamworks.Data;
    using System;
    using Eclipse.API.Features;
    using Round = Eclipse.Events.Handlers.Round;

    [HarmonyPatch(typeof(Bootstrap), nameof(Bootstrap.StopGame))]
    internal class RoundEndedPatch
    {
        private static void Postfix(bool goToLobby)
        {
            try
            {
                API.Features.Round round = new API.Features.Round();
                Handlers.Round.InvokeRoundEnded();
            }
            catch (Exception e)
            {
                Log.Error("Failed to invoke RoundEnded event: " + e);
            }
        }
    }
}