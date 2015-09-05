namespace Core.Entities.Interfaces
{
    public interface IColonistsHolder
    {
        int MaxColonistsCount { get; }

        int CurrentColonistsCount { get; }

        void Move(IColonistsHolder destination, int count = 1);

        void ReceiveColonist(int count = 1);

        int ActivePoints { get; }
    }
}