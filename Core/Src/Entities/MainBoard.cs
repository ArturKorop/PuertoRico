﻿using System;
using System.Collections.Generic;
using Core.Utils;

namespace Core.Entities
{
    public class MainBoard
    {
        public int OpenPlantationsCount { get; private set; }

        public int PlayersCount { get; private set; }

        public Market Market { get; set; }

        public Warehouse Warehouse { get; set; }

        public int Vp { get; set; }

        public int Doubloons { get; set; }

        public int Colonists { get; private set; }

        public Dictionary<IBuilding, int> Buildings { get; set; }

        public Queue<Plantation> Plantations { get; set; }

        public List<Plantation> AvailablePlantations { get; set; }

        public Queue<Quarry> Quarries { get; set; }

        public List<Ship> Ships { get; set; }

        public List<RoleCard> RoleCards { get; private set; }

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
            AvailablePlantations = new List<Plantation>();

            Warehouse = new Warehouse(10, 11, 11, 9, 9);
            Doubloons = 86;
            Vp = Constants.VpByPlayers[PlayersCount];

            var quarry = MainFactory.GenerateQuarries(8);
            Quarries = new Queue<Quarry>(quarry);

            var plantations = GetPlantations();
            Plantations = new Queue<Plantation>(plantations);

            Ships = MainFactory.GenerateShips(Constants.ShipsByPlayers[PlayersCount]);

            Colonists = Constants.ColonistsByPlayers[PlayersCount];

            RoleCards = MainFactory.GenerateRoleCards(Constants.RolesByPlayers[PlayersCount]);
        }

        private void UpdateCurrentPlantations()
        {
            for (int i = 0; i < OpenPlantationsCount; i++)
            {
                AvailablePlantations.Add(Plantations.Dequeue());
            }
        }

        private IEnumerable<Plantation> GetPlantations()
        {
            var plantations =
                MainFactory.GenerateAllPlantations(Constants.PlantationsByPlayers[PlayersCount][Goods.Corn], 8, 8,
                    Constants.PlantationsByPlayers[PlayersCount][Goods.Indigo], 8);

            var result = Util.Shuffle(plantations);

            return result;
        }

        public int TakeDoubloons(int doubloons)
        {
            if (Doubloons < doubloons)
            {
                throw new InvalidOperationException("Too less doubloons");
            }

            Doubloons -= doubloons;

            return doubloons;
        }
    }
}