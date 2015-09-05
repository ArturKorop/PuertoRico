using Core.Entities.Base;

namespace Core.Entities.Buildings
{
    public class IndigoPlant : GoodsFactoryBase
    {
        public IndigoPlant() : base(3, 2, 1, 2, 1, Goods.Indigo)
        {
        }
    }
}