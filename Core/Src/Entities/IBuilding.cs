namespace Core.Entities
{
    public interface IBuilding
    {
        int Cost { get; }

        int Vp { get; }

        int Size { get; }

        int Discount { get; }

        int MaxColonistsCount { get; }

        int CurrentColonistsCount { get; }

        bool CanDoAction { get; }

        bool AddColonist();

        bool RemoveColonist();
    }
}