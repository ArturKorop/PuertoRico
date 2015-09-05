using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class University : BuildingBase<BuilderParameters>
    {
        public University() : base(8, 3, 1, 3, 1)
        {
        }

        protected override void DoActionImpl(ref BuilderParameters parameters)
        {
            parameters.TakeAdditionalColonist = true;
        }
    }
}