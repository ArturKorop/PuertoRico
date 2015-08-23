using Core.ActionsData;

namespace Core.Entitites.Buildings
{
    public class ConstructionHut : BuildingBase<SettlerParameters>
    {
        public ConstructionHut() : base(2, 1, 1, 1, 1)
        {
        }

        protected override void DoActionImpl(ref SettlerParameters parameters)
        {
            parameters.CanTakeQuarryInsteadPlantation = true;
        }
    }
}
