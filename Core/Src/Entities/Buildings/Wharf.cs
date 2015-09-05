using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class Wharf : BuildingBase<CaptainParameters>
    {
        public Wharf() : base(9, 3, 1, 3, 1)
        {
        }

        protected override void DoActionImpl(ref CaptainParameters parameters)
        {
            parameters.HasOwnShip = true;
        }
    }
}