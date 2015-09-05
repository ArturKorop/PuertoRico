using System.Linq;
using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class CityHall : GameEndBuildingBase
    {
        protected override void DoActionImpl(ref GameEndParameters parameters)
        {
            parameters.AdditionalVp += parameters.Status.Board.Buildings.Count() -
                                       parameters.Status.Board.Buildings.OfType<GoodsFactoryBase>().Count();
        }
    }
}