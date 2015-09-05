using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class Factory : BuildingBase<CraftsmanParameters>
    {
        public Factory() : base(7, 3, 1, 3, 1)
        {
        }

        protected override void DoActionImpl(ref CraftsmanParameters parameters)
        {
            parameters.TakeAdditionalDoubloonsForProduction = true;
        }
    }
}