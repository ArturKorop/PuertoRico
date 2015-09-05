using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class Harbor : BuildingBase<CaptainParameters>
    {
        public Harbor() : base(8, 3, 1, 3, 1)
        {
        }

        protected override void DoActionImpl(ref CaptainParameters parameters)
        {
            parameters.TakeAdditionalVpPerDelivery = true;
        }
    }
}