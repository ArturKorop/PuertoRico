using Core.Entities;
using Core.PlayerCore;

namespace Core.Core
{
    public class PlayerContainer
    {
        public PlayerStatus Status { get; set; }

        public PlayerController Controller { get; set; }

        public IPlayerConnection Connection { get; set; }
    }
}