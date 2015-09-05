using Core.Entities.Base;

namespace Core.Entities.Buildings
{
    public class SmallSugarMill : GoodsFactoryBase
    {
        public SmallSugarMill() : base(2, 1, 1, 1, 1, Goods.Indigo)
        {
        }
    }
}