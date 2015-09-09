using Core.Entities.Base;

namespace Core.Entities.IslandObjects
{
    public class Plantation : ColonistsHolderBase, IISlandObject
    {
        public Goods Type { get; private set; }

        public Plantation(Goods type) : base(1)
        {
            Type = type;
        }
    }

    public interface IISlandObject
    {
        
    }
}
