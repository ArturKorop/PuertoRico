using Core.ActionsData;

namespace Core.Entitites.Buildings
{
    public class SmallMarket : BuildingBase<TraderParameters>
    {
        public SmallMarket()
            : base(3, 2, 1, 1, 1)
        {
        }

        protected override void DoActionImpl(ref TraderParameters parameters)
        {
            parameters.AdditionalPrice += 1;
        }
    }
}