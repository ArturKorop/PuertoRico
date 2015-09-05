using Core.Entities.RoleParameters;

namespace Core.Entities.Base
{
    public abstract class GameEndBuildingBase : BuildingBase<GameEndParameters>
    {
        protected GameEndBuildingBase() : base(10, 4, 2, 4, 1)
        {
        }
    }
}