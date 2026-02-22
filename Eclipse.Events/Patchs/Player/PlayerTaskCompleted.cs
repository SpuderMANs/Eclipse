namespace Eclipse.Events.Patchs.Player
{
    using HarmonyLib;
    using System;
    using System.Collections.Generic;
    using Eclipse.API.Features;
    using Eclipse.Events.Handlers;
    using Eclipse.API.Extensions;
    using System.Reflection;

    [HarmonyPatch(typeof(PlayerTaskManager), "OnTaskCompletionCallback")]
    internal class TaskCompleted
    {
        private static void Postfix(PlayerTaskManager __instance, int completeCount, PlayerTaskBase taskReference)
        {
            try
            {
                var allTasks = __instance.GetFieldValue<List<PlayerTaskBase>>("allTasks",BindingFlags.NonPublic | BindingFlags.Instance);

                int taskIndex = allTasks.IndexOf(taskReference);
                if (taskIndex < 0)
                    return;

                var taskInstances = __instance.GetFieldValue<System.Collections.IList>("n_taskInstances", BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var obj in taskInstances)
                {
                    var taskIndexField = obj.GetType().GetField("taskIndex", BindingFlags.NonPublic | BindingFlags.Instance);
                    var currentCompletionCountField = obj.GetType().GetField("currentCompletionCount", BindingFlags.NonPublic | BindingFlags.Instance);
                    var maxCompletionCountField = obj.GetType().GetField("maxCompletionCount", BindingFlags.NonPublic | BindingFlags.Instance);
                    var wasCompletedField = obj.GetType().GetField("wasCompleted", BindingFlags.NonPublic | BindingFlags.Instance);

                    int taskIndexValue = (int)taskIndexField.GetValue(obj);
                    int currentCompletionCount = (int)currentCompletionCountField.GetValue(obj);
                    int maxCompletionCount = (int)maxCompletionCountField.GetValue(obj);
                    bool wasCompleted = (bool)wasCompletedField.GetValue(obj);

                    if (taskIndexValue != taskIndex)
                        continue;

                    if (!wasCompleted || currentCompletionCount != maxCompletionCount)
                        return;

                    var networking = __instance.GetComponent<PlayerNetworking>();
                    if (networking == null)
                        return;

                    var player = API.Features.Player.GetByNetworking(networking);

                    Handlers.Player.InvokeTaskCompleted(player, taskReference);
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed to invoke TaskCompleted event: {e}");
            }
        }
    }
}