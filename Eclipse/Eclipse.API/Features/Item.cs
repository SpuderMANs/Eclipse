namespace Eclipse.API.Features
{
    using Eclipse.API.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Unity.Netcode;
    using UnityEngine;

    public class Item : ItemData
    {
        public ItemData Base { get; }
        public ItemData ItemData;

        private static readonly Dictionary<string, ItemType> TypeMapping = new()
        {
            { "CigaretteBox", ItemType.CigaretteBox },
            { "Knife", ItemType.Knife },
            { "Taser", ItemType.Taser },
            { "Pistol", ItemType.Pistol },
            { "Skillet", ItemType.Skillet },
        };

        public Item(ItemData item)
        {
            Base = item ?? throw new ArgumentNullException(nameof(item));
        }
        public static IReadOnlyList<Item> List =>
            UnityEngine.Object
                .FindObjectsByType<ItemInstance>(FindObjectsSortMode.None)
                .Select(x =>
                {
                    var data = GetItemDataFromIndex(x.InstanceData.itemIndex);
                    return data != null ? new Item(data) : null;
                })
                .Where(x => x != null)
                .ToList();


        public ItemType Type
        {
            get
            {
                var typeName = Base.name;
                if (TypeMapping.TryGetValue(typeName, out var type))
                    return type;

                return ItemType.Unknown;
            }
        }

        public float MaxDurability
        {
            get => Base.maxDurability;
            set => Base.maxDurability = value;
        }

        public static Item GetByInstanceData(ItemData itemData)
        {

            var Item = new Item(itemData);

            return Item;
        }

        private static ItemData GetItemDataFromIndex(byte index)
        {
            var inventory = UnityEngine.Object.FindFirstObjectByType<InventoryBase>();
            if (inventory == null)
                return null;

            return inventory.GetItemData(index);
        }
    }
}