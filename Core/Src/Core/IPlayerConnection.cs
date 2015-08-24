namespace Core.Core
{
    public interface IPlayerConnection
    {
        void Init(PlayerController playerController);
        string Name { get; }
        int Id { get; }
        Manager Manager { get; }
    }
}