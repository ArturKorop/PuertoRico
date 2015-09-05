using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class SmallWarehosue : BuildingBase<CaptainParameters>
    {
        public SmallWarehosue() : base(3, 1, 1, 1, 1)
        {
        }

        protected override void DoActionImpl(ref CaptainParameters parameters)
        {
            parameters.WarehousePlaces += 1;
        }
    }
}