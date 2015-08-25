using System;
using System.Collections.Generic;
using System.Linq;
using Core.ActionsData;
using Core.Entities;

namespace Core.Core
{
    public class PlayerController
    {
        private readonly PlayerStatus _playerStatus;

        private readonly MainBoardController _mainBoardController;

        private IEnumerable<IBuilding> Buildings
        {
            get { return _playerStatus.Board.Buildings; }
        }

        public PlayerStatus PlayerStatus
        {
            get { return _playerStatus; }
        }

        public PlayerController(PlayerStatus playerStatus, MainBoardController mainBoardController)
        {
            _playerStatus = playerStatus;
            _mainBoardController = mainBoardController;
        }

        public void DoMayorAction(MayorActionParameter parameter)
        {
            if (parameter.IsRoleOwner)
            {
                _mainBoardController.Status.Colonists.Move(_playerStatus.Board.ColonistsWarehouse);
            }

            var newColonistsCount = _mainBoardController.Status.AvailableColonists.CurrentColonistsCount;
            var playerNewColonists = (newColonistsCount%_mainBoardController.Status.PlayersCount) == 0
                ? 0
                : 1 +
                  newColonistsCount/_mainBoardController.Status.PlayersCount;
            _mainBoardController.Status.AvailableColonists.Move(_playerStatus.Board.ColonistsWarehouse,
                playerNewColonists);
        }

        public SimulateSettlerActionData SimulateSettlerAction()
        {
            var result = new SimulateSettlerActionData();
            UpdateSettlerData(ref result);

            return result;
        }

        public SimulateTradeActionData SimulateTradeAction()
        {
            var warehouse = _playerStatus.Warehouse;
            int? cornPossiblePrice = CalculateGoodsPrice(Goods.Corn);
            int? indigoPossiblePrice = CalculateGoodsPrice(Goods.Indigo);
            int? sugarPossiblePrice = CalculateGoodsPrice(Goods.Sugar);
            int? tabaccoPossiblePrice = CalculateGoodsPrice(Goods.Tabacco);
            int? coffeePossiblePrice = CalculateGoodsPrice(Goods.Coffee);

            var result = new SimulateTradeActionData(cornPossiblePrice, indigoPossiblePrice, sugarPossiblePrice,
                tabaccoPossiblePrice, coffeePossiblePrice);

            return result;
        }

        private int? CalculateGoodsPrice(Goods type)
        {
            var warehouse = _playerStatus.Warehouse;
            int? result = warehouse.GetGoodsCount(type) > 0
                ? _mainBoardController.Status.Market.SimulateSellGoods(type,
                    Buildings.OfType<BuildingBase<TraderParameters>>())
                : null;

            return result;
        }

        private void UpdateSettlerData(ref SimulateSettlerActionData data)
        {
            var settlerParameters = new SettlerParameters();
            foreach (var building in Buildings.OfType<BuildingBase<SettlerParameters>>())
            {
                building.DoAction(ref settlerParameters);
            }

            data.CanTakeAdditionalColonist = settlerParameters.CanTakeAdditionalColonist;
            data.CanTakeAdditionalPlantation = settlerParameters.CanTakeAdditionalPlantation;
            data.CanTakeQuarryInsteadPlantation = settlerParameters.CanTakeQuarryInsteadPlantation;
            data.AvailablePlantations = _mainBoardController.Status.AvailablePlantations;
            data.AvailableQuarryCount = _mainBoardController.Status.Quarries.Count;
        }
    }
}