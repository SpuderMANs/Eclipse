namespace Eclipse.API.Features
{
    using Eclipse.API.Enums;
    using Eclipse.API.Extensions;
    using NOTLonely_Door;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;

    public static class Hint
    {
        public static void Show(PlayerNetworking player, string message, ScreenLocationType screenLocation = ScreenLocationType.Middle,float duration = 5f)
        {
            try
            {
                if (player == null) return;
                var playerController = player.GetComponent<PlayerController>();
                if (playerController == null) return;
                Type keyActionType = AppDomain.CurrentDomain.GetAssemblies()
                    .Select(a => a.GetType("KeyActionPopupInformation"))
                    .FirstOrDefault(t => t != null);
                if (keyActionType == null) return;
                object hintObj = Activator.CreateInstance(keyActionType);
                keyActionType.GetField("text")?.SetValue(hintObj, message);

                var screenLocationEnum = keyActionType.GetNestedType("ScreenLocation");
                keyActionType.GetField("screenLocation")?.SetValue(hintObj, Enum.Parse(screenLocationEnum, screenLocation.ToString()));

                MethodInfo setHint = playerController.GetType().GetMethod(
                    "SetInteractHint",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                );
                setHint?.Invoke(playerController, new object[] { hintObj });
                playerController.StartCoroutine(HideHintCoroutine(playerController, duration));
                
            }
            catch (Exception e)
            {
                Log.Error("Hint Show failed: " + e);
            }
        }

        private static System.Collections.IEnumerator HideHintCoroutine(object playerController, float duration)
        {
            yield return new WaitForSeconds(duration);

            MethodInfo clearHint = playerController.GetType().GetMethod(
                "ClearInteractHint",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            );

            Type screenLocationEnum = Type.GetType("KeyActionPopupInformation+ScreenLocation");
            clearHint?.Invoke(playerController, new object[] { Enum.Parse(screenLocationEnum, "Middle") });
        }
    }
}
