using System;
using System.Collections.Generic;
using System.Linq;
using Core.ActionsData;
using Core.Entities;
using Core.Entities.Base;
using Core.Entities.Interfaces;
using Core.Entities.IslandObjects;
using Core.Entities.RoleParameters;

namespace Core.PlayerCore
{
    public class Bot : IPlayerConnection
    {
        public Bot(string name)
        {
            Name = name;
        }

        private SimulateSettlerActionData SimulateSettlerAction()
        {
            var result = new SimulateSettlerActionData();
            UpdateSettlerData(ref result, new IBuilding[] {});

            return result;
        }

        private SimulateTradeActionData SimulateTradeAction(PlayerStatus playerStatus)
        {
            var warehouse = playerStatus.Warehouse;
            int? cornPossiblePrice = CalculateGoodsPrice(Goods.Corn);
            int? indigoPossiblePrice = CalculateGoodsPrice(Goods.Indigo);
            int? sugarPossiblePrice = CalculateGoodsPrice(Goods.Sugar);
            int? tabaccoPossiblePrice = CalculateGoodsPrice(Goods.Tabacco);
            int? coffeePossiblePrice = CalculateGoodsPrice(Goods.Coffee);

            var result = new SimulateTradeActionData(cornPossiblePrice, indigoPossiblePrice, sugarPossiblePrice,
                tabaccoPossiblePrice, coffeePossiblePrice);

            return result;
        }

        private int? CalculateGoodsPrice(Goods goods)
        {
            throw new NotImplementedException();
        }


        private void UpdateSettlerData(ref SimulateSettlerActionData data, IEnumerable<IBuilding> buildings)
        {
            var settlerParameters = new SettlerParameters();
            foreach (var building in buildings.OfType<BuildingBase<SettlerParameters>>())
            {
                building.DoAction(ref settlerParameters);
            }

            data.CanTakeAdditionalColonist = settlerParameters.CanTakeAdditionalColonist;
            data.CanTakeAdditionalPlantation = settlerParameters.CanTakeAdditionalPlantation;
            data.CanTakeQuarryInsteadPlantation = settlerParameters.CanTakeQuarryInsteadPlantation;
            //data.AvailablePlantations = _mainBoardController.Status.AvailablePlantations;
            //data.AvailableQuarryCount = _mainBoardController.Status.Quarries.Count;
        }

        public string Name { get; }

        public RoleCardStatus SelectRole(List<RoleCardStatus> cards, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            var rand = new Random();

            return cards[rand.Next(cards.Count - 1)];
        }

        public IBuilding SelectBuildingToBuild(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board,
            IEnumerable<PlayerStatus> opponents)
        {
            var allBuildings = board.Buildings.Where(x => x.Value > 0);
            var param = new BuilderParameters();
                status.Board.Buildings.OfType<BuildingBase<BuilderParameters>>()
                    .ToList()
                    .ForEach(x => x.DoAction(ref param));
            var discount = status.Board.Quarries.Count(x => x.IsActive);

            var availableBuldings =
                allBuildings.Where(x => x.Key.Cost - Math.Min(discount, x.Key.Discount) <= status.Doubloons).ToArray();

            var building = availableBuldings[new Random().Next(availableBuldings.Count() - 1)];

            return building.Key;
        }

        public MoveDirection MoveColonist(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods? SelectGoodsToTrade(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods SelectAdditionalGoods(PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IISlandObject> SelectISlandObjects(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public GoodsToShip SelectGoodsToShip(PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Goods> SelectGoodsForWarehouse(PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods? SelectOneGoodsForStore(PlayerStatus status, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public bool IsUsePrivilage(PlayerStatus status, MainBoardStatus mainBoardStatus, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public bool IsTakeAdditionalColonist(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, List<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public void GameEnd(Dictionary<int, int> playersScore)
        {
            throw new NotImplementedException();
        }
    }

    public class PlayerTurnAction
    {
        public object BuilderAction { get; set; }
    }
}