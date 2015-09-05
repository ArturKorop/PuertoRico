using Core.Entities.Base;

namespace Core.Entities.Buildings
{
    public class TobaccoStorage : GoodsFactoryBase
    {
        public TobaccoStorage() : base(5, 3, 1, 3, 1, Goods.Indigo)
        {
        }
    }
}