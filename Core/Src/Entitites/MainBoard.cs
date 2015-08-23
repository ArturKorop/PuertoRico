using System.Collections.Generic;
using Core.Utils;

namespace Core.Entitites
{
    public class MainBoard
    {
        public int OpenPlantationsCount { get; private set; }

        public int PlayersCount { get; private set; }

        public Market Market { get; set; }

        public Warehouse Warehouse { get; set; }

        public int Vp { get; set; }

        public int Doubloons { get; set; }

        public int Colonists { get; set; }

        public Dictionary<IBuilding, int> Buildings { get; set; }

        public Queue<Plantation> Plantations { get; set; }

        public List<Plantation> AvailablePlantations { get; set; }

        public Queue<Quarry> Quarries { get; set; }

        public Ship[] Ships { get; set; }

        private Dictionary<int, int> _vpByPlayers = new Dictionary<int, int>
        {
            {3, 75},
            {4, 100},
            {5, 122}
        };

        public MainBoard(int playersCount)
        {
            PlayersCount = playersCount;
            OpenPlantationsCount = PlayersCount + 1;
            InitStartState();
        }

        public void ReturnGoods(IEnumerable<Goods> goods)
        {
            Warehouse.AddGoods(goods);
        }

        private void InitStartState()
        {
            Market = new Market();

            Ships = new Ship[PlayersCount - 1];
            AvailablePlantations = new List<Plantation>();

            Warehouse = new Warehouse(10, 11, 11, 9, 9);
            Doubloons = 86;
            Vp = _vpByPlayers[PlayersCount];

            var quarry = MainFactory.GetQuarries(8);
            Quarries = new Queue<Quarry>(quarry);

            var plantations = GetPlantations();
            Plantations = new Queue<Plantation>(plantations);
        }

        private void UpdateCurrentPlantations()
        {
            for (int i = 0; i < OpenPlantationsCount; i++)
            {
                AvailablePlantations[i] = Plantations.Dequeue();
            }
        }

        private static IEnumerable<Plantation> GetPlantations()
        {
            var coffeePlantations = MainFactory.GetPlantions(8, Goods.Coffee);
            var tabaccoPlantations = MainFactory.GetPlantions(8, Goods.Tabacco);
            var indigoPlantations = MainFactory.GetPlantions(8, Goods.Indigo);
            var cornPlantations = MainFactory.GetPlantions(8, Goods.Corn);
            var sugarPlantations = MainFactory.GetPlantions(8, Goods.Sugar);

            var result = Util.Shuffle(coffeePlantations, tabaccoPlantations, indigoPlantations, cornPlantations,
                sugarPlantations);

            return result;
        }
    }
}