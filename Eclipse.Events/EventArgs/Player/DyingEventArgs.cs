namespace Eclipse.Events.EventArgs.Player
{
    using Eclipse.API.Enums;
    using Eclipse.Events.EventArgs.Interfaces;
    using Steamworks;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Player = Eclipse.API.Features.Player;

    public class DyingEventArgs : IPlayerEvent
    {

        public DyingEventArgs(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
        public bool IsAllowed { get; set; } = true;
        // public DamageType DamageType { get; set; }

    }
}
