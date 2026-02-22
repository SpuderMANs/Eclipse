/*namespace Eclipse.Events.Patchs.Player
{
    using Eclipse.API.Features;
    using HarmonyLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [HarmonyPatch(typeof(InventoryBase), nameof(InventoryBase.TryRemoveItem), new Type[] { typeof(ItemData.InstanceData), typeof(ItemInstance) })]
    internal class ItemRemoved
    {
        private static void Postfix(InventoryBase __instance, ItemData.InstanceData instanceData, ItemInstance instance, bool __result)
        {
            if (!__result)
                return;

            var playerNetworking = __instance.GetComponent<PlayerNetworking>();
            if (playerNetworking == null)
                return;

            var player = API.Features.Player.GetByNetworking(playerNetworking);

            var itemData = __instance.GetItemData(instanceData.itemIndex);
            Item item = new Item(itemData);

            Handlers.Player.InvokeItemRemoved(player, item);
        }
    }
}*/
