using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class Hospice : BuildingBase<SettlerParameters>
    {
        public Hospice()
            : base(4, 2, 1, 1, 1)
        {
        }

        protected override void DoActionImpl(ref SettlerParameters parameters)
        {
            parameters.CanTakeAdditionalColonist = true;
        }
    }
}
