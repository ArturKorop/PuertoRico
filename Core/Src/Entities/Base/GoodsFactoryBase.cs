using Core.Entities.RoleParameters;

namespace Core.Entities.Base
{
    public abstract class GoodsFactoryBase : BuildingBase<CraftsmanParameters>
    {
        protected Goods Type;

        protected GoodsFactoryBase(int cost, int vp, int size, int discount, int maxColonists, Goods type)
            : base(cost, vp, size, discount, maxColonists)
        {
            Type = type;
        }

        protected override void DoActionImpl(ref CraftsmanParameters parameters)
        {
            parameters.GoodsProduction[Type] += CurrentColonistsCount;
        }
    }
}