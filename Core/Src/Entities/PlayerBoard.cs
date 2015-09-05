using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entities.Interfaces;
using Core.Entities.IslandObjects;

namespace Core.Entities
{
    public class PlayerBoard
    {
        public const int MaxBuildingTitles = 12;

        public const int MaxIslandObjects = 12;

        private readonly List<IBuilding> _buildings;

        public IEnumerable<IBuilding> Buildings => _buildings;

        private readonly List<Plantation> _plantations;

        public IEnumerable<Plantation> Plantations => _plantations;

        private readonly List<Quarry> _quarries;

        public IEnumerable<Quarry> Quarries => _quarries;

        public ColonistsWarehouse ColonistsWarehouse { get; }

        public int TotalColonists
        {
            get
            {
                return ColonistsWarehouse.CurrentColonistsCount + Buildings.Sum(x => x.CurrentColonistsCount) +
                       Plantations.Sum(x => x.CurrentColonistsCount) + Quarries.Sum(x => x.CurrentColonistsCount);
            }
        }

        public PlayerBoard()
        {
            _buildings = new List<IBuilding>();
            _plantations = new List<Plantation>();
            _quarries = new List<Quarry>();
            ColonistsWarehouse = new ColonistsWarehouse();
        }

        public void BuildBuilding(IBuilding building)
        {
            if (CanBuildBuilding(building.Size))
            {
                _buildings.Add(building);
            }
        }

        public void BuildQuarry(Quarry quarry)
        {
            if (CanBuildIslandObject())
            {
                _quarries.Add(quarry);
            }
        }

        public void BuildPlantation(Plantation plantation)
        {
            if (CanBuildIslandObject())
            {
                _plantations.Add(plantation);
            }
        }

        public bool CanBuildBuilding(int size)
        {
            return _buildings.Sum(x => x.Size) + size <= MaxBuildingTitles;
        }

        public bool CanBuildIslandObject()
        {
            return this.IslandObjectsCount() < MaxIslandObjects;
        }

        public string Status()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Board:");
            builder.AppendLine("Buildings: ");
            foreach (var building in Buildings)
            {
                builder.AppendLine(building.GetType().Name);
            }

            builder.AppendLine("Plantations: ");
            foreach (var plantation in Plantations)
            {
                builder.AppendLine(plantation.Type.ToString());
            }

            return builder.ToString();
        }
    }

    public static class PlayerBoardExtensions
    {
        public static int IslandObjectsCount(this PlayerBoard board)
        {
            return board.Plantations.Count() + board.Quarries.Count();
        }
    }

}