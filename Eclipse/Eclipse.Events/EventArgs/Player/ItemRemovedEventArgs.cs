namespace Eclipse.Events.EventArgs.Player
{
    using Eclipse.Events.EventArgs.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class ItemRemovedEventArgs : IPlayerEvent
    {
        public ItemRemovedEventArgs(API.Features.Player player, API.Features.Item item)
        {
            Player = player;
            Item = item;
        }

        public API.Features.Player Player { get; }
        public API.Features.Item Item { get; }

    }
}
