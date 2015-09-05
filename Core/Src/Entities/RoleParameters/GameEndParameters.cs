namespace Core.Entities.RoleParameters
{
    public class GameEndParameters
    {
        public PlayerStatus Status { get; }

        public int AdditionalVp { get; set; }

        public GameEndParameters(PlayerStatus status)
        {
            Status = status;
        }
    }
}