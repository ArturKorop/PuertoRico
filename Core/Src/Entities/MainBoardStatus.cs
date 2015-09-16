using System.Collections.Generic;
using Core.Entities.Interfaces;
using Core.Entities.IslandObjects;
using Core.Utils;

namespace Core.Entities
{
    public class MainBoardStatus
    {
        public int OpenPlantationsCount { get; }

        public int PlayersCount { get; }

        public Market Market { get; set; }

        public Warehouse Warehouse { get; set; }

        public int Vp { get; set; }

        public int Doubloons { get; set; }

        public ColonistsWarehouse Colonists { get; } = new ColonistsWarehouse();

        public Dictionary<IBuilding, int> Buildings { get; set; }

        public Queue<Plantation> Plantations { get; set; }

        public List<Plantation> AvailablePlantations { get; set; }

        public Queue<Quarry> Quarries { get; set; }

        public List<Ship> Ships { get; set; }

        public List<RoleCard> RoleCards { get; private set; }

        public ColonistsWarehouse AvailableColonists { get; } = new ColonistsWarehouse();

        public MainBoardStatus(int playersCount)
        {
            PlayersCount = playersCount;
            OpenPlantationsCount = PlayersCount + 1;
            Init();
        }

        private void Init()
        {
            Market = new Market();
            AvailablePlantations = new List<Plantation>();

            Warehouse = new Warehouse(10, 11, 11, 9, 9);
            Doubloons = 86;
            Vp = Constants.VpByPlayers[PlayersCount];

            var quarry = MainFactory.GenerateQuarries(8);
            Quarries = new Queue<Quarry>(quarry);

            var plantations = GetPlantations();
            Plantations = new Queue<Plantation>(plantations);

            Ships = MainFactory.GenerateShips(Constants.ShipsByPlayers[PlayersCount]);

            Colonists.ReceiveColonist(Constants.ColonistsByPlayers[PlayersCount]);

            RoleCards = MainFactory.GenerateRoleCards(Constants.RolesByPlayers[PlayersCount]);

            Colonists.Move(AvailableColonists, PlayersCount);

            Buildings = MainFactory.GenerateBuildings(PlayersCount);
        }

        private IEnumerable<Plantation> GetPlantations()
        {
            var plantations =
                MainFactory.GenerateAllPlantations(Constants.PlantationsByPlayers[PlayersCount][Goods.Corn], 8, 8,
                    Constants.PlantationsByPlayers[PlayersCount][Goods.Indigo], 8);

            var result = Util.Shuffle(plantations);

            return result;
        }
    }
}