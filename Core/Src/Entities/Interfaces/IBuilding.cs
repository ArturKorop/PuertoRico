namespace Core.Entities.Interfaces
{
    public interface IBuilding : IColonistsHolder
    {
        int Cost { get; }

        int Vp { get; }

        int Size { get; }

        int Discount { get; }
    }
}