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
        public string Name { get; }

        public RoleCardStatus SelectRole(List<RoleCardStatus> cards, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public IBuilding SelectBuildingToBuild(bool isHasPrivilage, PlayerStatus status, Dictionary<IBuilding, int> availableBuildings, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public MoveDirection MoveColonist(bool isHasPrivilage, PlayerStatus status, IEnumerable<IBuilding> availableBuildings,
            IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods? SelectGoodsToTrade(bool isHasPrivilage, PlayerStatus status, IEnumerable<IBuilding> availableBuildings,
            IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods SelectAdditionalGoods(PlayerStatus status, IEnumerable<IBuilding> availableBuildings, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IISlandObject> SelectISlandObjects(bool isHasPrivilage, PlayerStatus status, IEnumerable<IBuilding> availableBuildings,
            IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public GoodsToShip SelectGoodsToShip(PlayerStatus status, IEnumerable<IBuilding> availableBuildings, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Goods> SelectGoodsToWarehouse(PlayerStatus status, IEnumerable<IBuilding> availableBuildings, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods? SelectOneGoodsToStore(PlayerStatus status, IEnumerable<IBuilding> availableBuildings, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public void GameEnd(Dictionary<int, int> playersScore)
        {
            throw new NotImplementedException();
        }

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
    }

    public class PlayerTurnAction
    {
        public object BuilderAction { get; set; }
    }
}