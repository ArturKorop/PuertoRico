using System;
using System.Collections.Generic;
using System.Linq;
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

        public ColonistsWarehouse Colonists { get; private set; }

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
            InitStartState();
        }

        public void ReceiveGoods(IEnumerable<Goods> goods)
        {
            Warehouse.AddGoods(goods);
        }

        public void UpdateCurrentPlantations()
        {
            for (int i = 0; i < OpenPlantationsCount; i++)
            {
                AvailablePlantations.Add(Plantations.Dequeue());
            }
        }

        private void InitStartState()
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

            Colonists = new ColonistsWarehouse(); 
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

    public class MainBoardController
    {
        private readonly MainBoardStatus _status;

        public MainBoardController(int playersCount)
        {
            _status = new MainBoardStatus(playersCount);
        }

        //Todo: implement clone
        public MainBoardStatus Status => _status;

        public int TakeDoubloons(int doubloons)
        {
            if (_status.Doubloons < doubloons)
            {
                throw new InvalidOperationException("Too less doubloons");
            }

            _status.Doubloons -= doubloons;

            return doubloons;
        }

        public void ReceiveDoubloons(int doubloons)
        {
            _status.Doubloons += doubloons;
        }

        public bool UpdateAvailableColonists(IEnumerable<IEnumerable<IBuilding>> allPlayerBuildings)
        {
            var expectedColonistsCount =
                allPlayerBuildings.SelectMany(x => x.ToList()).Sum(x => x.MaxColonistsCount - x.CurrentColonistsCount);

            if (expectedColonistsCount <= _status.Colonists.CurrentColonistsCount)
            {
                _status.Colonists.Move(_status.AvailableColonists, expectedColonistsCount);

                return true;
            }

            return false;
        }

        public bool TryGetRoleCard(Roles currentRole, out RoleCard roleCard)
        {
            // TODO: add find with max money
            roleCard  = _status.RoleCards.FirstOrDefault(x => x.Role == currentRole && !x.IsUsed);

            return roleCard != null;
        }
    }
}