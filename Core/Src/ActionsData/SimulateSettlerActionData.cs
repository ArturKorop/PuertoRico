using System.Collections.Generic;
using Core.Entities;
using Core.Entities.IslandObjects;

namespace Core.ActionsData
{
    public class SimulateSettlerActionData
    {
        public bool CanTakeAdditionalPlantation { get; set; }

        public bool CanTakeQuarryInsteadPlantation { get; set; }

        public bool CanTakeAdditionalColonist { get; set; }

        public List<Plantation> AvailablePlantations { get; set; }

        public int AvailableQuarryCount { get; set; }
    }
}
