using Core.ActionsData;

namespace Core.Entities.Buildings
{
    public class Office : BuildingBase<TraderParameters>
    {
        public Office() : base(5, 2, 1, 2, 1)
        {
        }

        protected override void DoActionImpl(ref TraderParameters parameters)
        {
            parameters.PermissionToSellTheSame = true;
        }
    }
}
