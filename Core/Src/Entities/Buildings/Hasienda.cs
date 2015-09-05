using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class Hasienda : BuildingBase<SettlerParameters>
    {
        public Hasienda() : base(2, 1, 1, 1, 1)
        {
        }

        protected override void DoActionImpl(ref SettlerParameters parameters)
        {
            parameters.CanTakeAdditionalPlantation = true;
        }
    }
}