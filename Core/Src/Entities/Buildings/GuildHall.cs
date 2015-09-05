using System.Linq;
using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class GuildHall : GameEndBuildingBase
    {
        protected override void DoActionImpl(ref GameEndParameters parameters)
        {
            var vpForLargeProductiveBuildings =
                parameters.Status.Board.Buildings.OfType<GoodsFactoryBase>().Count(x => x.MaxColonistsCount > 1)*2;
            var vpForSmallProductiveBuildings =
                parameters.Status.Board.Buildings.OfType<GoodsFactoryBase>().Count(x => x.MaxColonistsCount == 1)*2;

            parameters.AdditionalVp += vpForSmallProductiveBuildings + vpForLargeProductiveBuildings;
        }
    }
}