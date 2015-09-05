using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities.Buildings
{
    public class Residence : GameEndBuildingBase
    {
        private readonly Dictionary<int, int> _vpPerIslandObjects = new Dictionary<int, int>
        {
            {9, 4},
            {10, 5},
            {11, 6},
            {12, 7},
        };

        protected override void DoActionImpl(ref GameEndParameters parameters)
        {
            var islandObjects = parameters.Status.Board.IslandObjectsCount();
            islandObjects = Math.Max(islandObjects, _vpPerIslandObjects.Keys.Min());
            islandObjects = Math.Min(islandObjects, _vpPerIslandObjects.Keys.Max());

            parameters.AdditionalVp += _vpPerIslandObjects[islandObjects];
        }
    }
}