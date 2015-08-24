using Core.ActionsData;

namespace Core.Entities.Buildings
{
    public class LargeMarket : BuildingBase<TraderParameters>
    {
        public LargeMarket() : base(5, 2, 1, 2, 1)
        {
        }

        protected override void DoActionImpl(ref TraderParameters parameters)
        {
            parameters.AdditionalPrice += 2;
        }
    }
}
