using System.Collections.Generic;
using System.Linq;
using Core.ActionsData;

namespace Core.Entitites
{
    public class PlayerController
    {
        private readonly Player _player;

        private readonly MainBoard _mainBoard;

        private IEnumerable<IBuilding> Buildings { get { return _player.Board.Buildings; } }

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

        public SimulateTradeActionData SimulateTradeAction()
        {
            var warehouse = _player.Warehouse;
            int? cornPossiblePrice = CalculateGoodPrice(Goods.Corn);
            int? indigoPossiblePrice = CalculateGoodPrice(Goods.Indigo);
            int? sugarPossiblePrice = CalculateGoodPrice(Goods.Sugar);
            int? tabaccoPossiblePrice = CalculateGoodPrice(Goods.Tabacco);
            int? coffeePossiblePrice = CalculateGoodPrice(Goods.Coffee);

            var result = new SimulateTradeActionData(cornPossiblePrice, indigoPossiblePrice, sugarPossiblePrice, tabaccoPossiblePrice, coffeePossiblePrice);

            return result;
        }

        private int? CalculateGoodPrice(Goods type)
        {
            var warehouse = _player.Warehouse;
            int? result = warehouse.GetGoodCount(type) > 0
                ? _mainBoard.Market.SimulateSellGood(type, Buildings.OfType<BuildingBase<TraderParameters>>())
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
            data.CanTakeQuarryIsteadPlantation = settlerParameters.CanTakeQuarryInsteadPlantation;
            data.AvailablePlantations = _mainBoard.AvailablePlantations;
            data.AvailableQuarryCount = _mainBoard.Quarries.Count;
        }
    }
}
