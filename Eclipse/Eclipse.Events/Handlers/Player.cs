namespace Eclipse.Events.Handlers
{
    using Eclipse.Events.EventArgs.Player;
    using Eclipse.API.Features;
    using System;
    using Eclipse.Events.Features;
    using Steamworks;
    using Unity.Netcode;
    using Eclipse.Events.Patchs.Player;

    public static class Player
    {
        public static Event<JoinedEventArgs> Joined = new Event<JoinedEventArgs>();
        public static Event<LeftEventArgs> Left = new Event<LeftEventArgs>();
        public static Event<DiedEventArgs> Died = new Event<DiedEventArgs>();
        public static Event<DyingEventArgs> Dying = new Event<DyingEventArgs>();
        public static Event<ItemAddedEventArgs> ItemAdded = new Event<ItemAddedEventArgs>();
        public static Event<ItemRemovedEventArgs> ItemRemoved = new Event<ItemRemovedEventArgs>();
        public static Event<GrabbedObjectEventArgs> GrabbedObject = new Event<GrabbedObjectEventArgs>();
        public static Event<TaskCompletedEventArgs> TaskCompleted = new Event<TaskCompletedEventArgs>();

        internal static void InvokeJoined(API.Features.Player player)
        {
            Joined.Invoke(new JoinedEventArgs(player));
        }

        internal static void InvokeLeft(API.Features.Player player)
        {
            if (player == null) return;

            Left.Invoke(new LeftEventArgs(player));
        }
        internal static void InvokeDied(API.Features.Player player)
        {
            if (player == null) return;

            Died.Invoke(new DiedEventArgs(player));
        }

        internal static void InvokeDying(API.Features.Player player)
        {
            if (player == null) return;

            Dying.Invoke(new DyingEventArgs(player));
        }
        internal static void InvokeItemAdded(API.Features.Player player, API.Features.Item item)
        {
            if (player == null) return;

            ItemAdded.Invoke(new ItemAddedEventArgs(player, item));
        }
        internal static void InvokeItemRemoved(API.Features.Player player, API.Features.Item item)
        {
            if (player == null) return;

            ItemRemoved.Invoke(new ItemRemovedEventArgs(player, item));
        }
        internal static void InvokeGrabbedObject(API.Features.Player player, IPlayerPushable pushable)
        {
            GrabbedObject.Invoke(new GrabbedObjectEventArgs(player, pushable));
        }
        internal static void InvokeTaskCompleted(API.Features.Player player, PlayerTaskBase task)
        {
            TaskCompleted.Invoke(new TaskCompletedEventArgs(player, task));
        }

    }
}