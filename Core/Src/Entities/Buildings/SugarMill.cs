using Core.Entities.Base;

namespace Core.Entities.Buildings
{
    public class SugarMill : GoodsFactoryBase
    {
        public SugarMill() : base(4, 2, 1, 2, 1, Goods.Indigo)
        {
        }
    }
}