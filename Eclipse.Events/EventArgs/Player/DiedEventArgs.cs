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

    public class DiedEventArgs : IPlayerEvent
    {

        public DiedEventArgs(Player player)
        {
            Player = player;
        }

        public Player Player { get; }
        // public DamageType DamageType { get; set; }
        
    }
}
