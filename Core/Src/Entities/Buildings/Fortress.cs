using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class Fortress : GameEndBuildingBase
    {
        protected override void DoActionImpl(ref GameEndParameters parameters)
        {
            parameters.AdditionalVp += parameters.Status.Board.TotalColonists/3;
        }
    }
}