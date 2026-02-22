namespace Eclipse.Events.Patchs.Player
{
    using Eclipse.API.Extensions;
    using Eclipse.API.Features;
    using HarmonyLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using UnityEngine;

    [HarmonyPatch(typeof(PlayerHandController), "HandleGrabbingAndSticking")]
    internal class GrabbedObject
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            try
            {
                for (int i = 0; i < codes.Count; i++)
                {
                    if (codes[i].opcode == OpCodes.Call && codes[i].operand is MethodInfo method && method.Name == "ResolvePushedTarget")
                    {
                        codes.InsertRange(i + 1, new[]
                         {
                            new CodeInstruction(OpCodes.Dup),          // IPlayerPushable
                            new CodeInstruction(OpCodes.Ldarg_0),     // this
                            new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(GrabbedObject), nameof(InvokeGrabbed))) // Call InvokeGrabbed
                        });
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to invoke GrabbedObject event: {ex}");
            }

            return codes.AsEnumerable();
        }

        private static void InvokeGrabbed(IPlayerPushable pushable, PlayerHandController hand)
        {
            if (pushable == null) return;

            var playerNetworking = hand.GetFieldValue<PlayerNetworking>("networking", BindingFlags.Instance | BindingFlags.NonPublic);
            if (playerNetworking == null) return;

            var player = API.Features.Player.GetByNetworking(playerNetworking);
            Log.Info(player.DisplayedNickname);
            Handlers.Player.InvokeGrabbedObject(player, pushable);

        }
    }
}