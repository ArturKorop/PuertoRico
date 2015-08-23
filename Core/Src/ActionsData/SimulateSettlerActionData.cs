using System.Collections.Generic;
using Core.Entitites;

namespace Core.ActionsData
{
    public class SimulateSettlerActionData
    {
        public bool CanTakeAdditionalPlantation { get; set; }

        public bool CanTakeQuarryIsteadPlantation { get; set; }

        public bool CanTakeAdditionalColonist { get; set; }

        public List<Plantation> AvailablePlantations { get; set; }

        public int AvailableQuarryCount { get; set; }
    }
}
