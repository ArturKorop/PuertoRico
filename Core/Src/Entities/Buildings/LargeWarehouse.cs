using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class LargeWarehouse : BuildingBase<CaptainParameters>
    {
        public LargeWarehouse() : base(6, 2, 1, 2, 1)
        {
        }

        protected override void DoActionImpl(ref CaptainParameters parameters)
        {
            parameters.WarehousePlaces += 2;
        }
    }
}