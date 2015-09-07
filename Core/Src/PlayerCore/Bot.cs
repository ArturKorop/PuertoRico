using System;
using System.Collections.Generic;
using System.Linq;
using Core.ActionsData;
using Core.Entities;
using Core.Entities.Base;
using Core.Entities.Interfaces;
using Core.Entities.RoleParameters;

namespace Core.PlayerCore
{
    public class Bot : IPlayerConnection
    {
        public string Name { get; }

        public Bot(string name)
        {
            Name = name;
        }

        public PlayerTurnAction DoTurn(Roles status, bool isHasPrivilage, PlayerStatus opponents, MainBoardStatus board, PlayerStatus[] playerStatuses)
        {
            return new PlayerTurnAction();
        }

        public RoleCard SelectRole(List<RoleCard> cards, PlayerStatus status, MainBoardStatus board, PlayerStatus[] opponents)
        {
            return cards.First();
        }

        public void GameEnd(Dictionary<int, int> playersScore)
        {
            throw new NotImplementedException();
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
    }
}