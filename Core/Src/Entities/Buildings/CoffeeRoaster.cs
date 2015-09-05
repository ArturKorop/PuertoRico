using Core.Entities.Base;

namespace Core.Entities.Buildings
{
    public class CoffeeRoaster : GoodsFactoryBase
    {
        public CoffeeRoaster() : base(6, 3, 1, 2, 1, Goods.Indigo)
        {
        }
    }
}