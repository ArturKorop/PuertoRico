using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class CustomsHouse : GameEndBuildingBase
    {
        protected override void DoActionImpl(ref GameEndParameters parameters)
        {
            parameters.AdditionalVp += parameters.Status.Vp/4;
        }
    }
}