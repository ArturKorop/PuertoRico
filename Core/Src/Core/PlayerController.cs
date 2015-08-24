using System;
using System.Collections.Generic;
using System.Linq;
using Core.ActionsData;
using Core.Entities;

namespace Core.Core
{
    public class PlayerController
    {
        private readonly Player _player;

        private readonly MainBoard _mainBoard;

        private IEnumerable<IBuilding> Buildings
        {
            get { return _player.Board.Buildings; }
        }

        public Player Player
        {
            get { return _player; }
        }

        public PlayerController(Player player, MainBoard mainBoard)
        {
            _player = player;
            _mainBoard = mainBoard;
        }

        public SimulateSettlerActionData SimulateSettlerAction()
        {
            var result = new SimulateSettlerActionData();
            UpdateSettlerData(ref result);

            return result;
        }

        public event EventHandler<Roles> OnSelectRole;

        public SimulateTradeActionData SimulateTradeAction()
        {
            var warehouse = _player.Warehouse;
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
            var warehouse = _player.Warehouse;
            int? result = warehouse.GetGoodsCount(type) > 0
                ? _mainBoard.Market.SimulateSellGoods(type, Buildings.OfType<BuildingBase<TraderParameters>>())
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
            data.AvailablePlantations = _mainBoard.AvailablePlantations;
            data.AvailableQuarryCount = _mainBoard.Quarries.Count;
        }
    }
}