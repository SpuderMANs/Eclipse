namespace Eclipse.Events.EventArgs.Player
{
    using Eclipse.Events.EventArgs.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Player = API.Features.Player;
    public class TaskCompletedEventArgs : IPlayerEvent
    {
        public TaskCompletedEventArgs(Player player, PlayerTaskBase task)
        {
            Player = player;
            Task = task;
        }
        public Player Player { get; }
        public PlayerTaskBase Task { get; }
    }
}
