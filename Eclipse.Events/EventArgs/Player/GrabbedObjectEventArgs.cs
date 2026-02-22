namespace Eclipse.Events.EventArgs.Player
{
    using Eclipse.Events.EventArgs.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class GrabbedObjectEventArgs : IPlayerEvent
    {
        public GrabbedObjectEventArgs(API.Features.Player player, IPlayerPushable objects)
        {
            Player = player;
            Object = objects;
        }

        public API.Features.Player Player { get; }
        public IPlayerPushable Object { get; }
    }
}
