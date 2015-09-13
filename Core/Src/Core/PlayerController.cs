using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Base;
using Core.Entities.Interfaces;
using Core.Entities.IslandObjects;
using Core.Entities.RoleParameters;
using Core.PlayerCore;
using Core.Utils;

namespace Core.Core
{
    public class PlayerController
    {
        private readonly PlayerStatus _playerStatus;

        private readonly MainBoardController _mainBoardController;

        public PlayerController(MainBoardController mainBoardController, PlayerStatus playerStatus)
        {
            _mainBoardController = mainBoardController;
            _playerStatus = playerStatus;
        }

        public Roles DoSelectRoleAction(RoleCard card)
        {
            if (card.IsUsed)
            {
                throw new InvalidOperationException("Role card is used");
            }

            var data = card.Take();
            _playerStatus.ReceiveDoubloons(data.Item2);

            return data.Item1;
        }

        public bool DoProspectorAction()
        {
            _playerStatus.ReceiveDoubloons(
                _mainBoardController.TakeDoubloons(_mainBoardController.TakeDoubloons(GameConstants.ProspectorMoney)));

            return true;
        }

        public bool DoSelectBuildingToBuild(IBuilding building, bool isHasPrivilage)
        {
            var buildingToBuild =
                _mainBoardController.Status.Buildings.Single(x => x.Key.GetType() == building.GetType());
            var totalDiscount = _playerStatus.Board.Quarries.Count(x => x.CurrentColonistsCount > 0);
            var realCost = buildingToBuild.Key.Cost - totalDiscount;

            if (buildingToBuild.Value > 0 && realCost <= _playerStatus.Doubloons)
            {
                // TODO: check place amount
                _playerStatus.Board.BuildBuilding(buildingToBuild.Key);
                _mainBoardController.Status.Buildings[buildingToBuild.Key]--;

                _mainBoardController.ReceiveDoubloons(_playerStatus.PayDoubloons(realCost));

                var param = new BuilderParameters();
                _playerStatus.Board.Buildings.OfType<BuildingBase<BuilderParameters>>()
                    .Where(x => x.IsActive)
                    .ToList()
                    .ForEach(x => x.DoAction(ref param));

                if (param.TakeAdditionalColonist)
                {
                    _mainBoardController.Status.Colonists.Move(buildingToBuild.Key);
                }

                return true;
            }

            return false;
        }

        public bool DoMayorActionTakeAdditionalColinists(bool usePrivilage)
        {
            if (usePrivilage)
            {
                _mainBoardController.Status.Colonists.Move(_playerStatus.Board.ColonistsWarehouse);

                return true;
            }

            return true;
        }

        public bool DoMoveColonistAction(MoveDirection moveDirection)
        {
            moveDirection.Source.Move(moveDirection.Destination);

            return true;
        }

        public bool DoSettlerAction(List<IISlandObject> islandObjects, bool isHasPrivilage)
        {
            var param = new SettlerParameters();
            _playerStatus.Board.Buildings.OfType<BuildingBase<SettlerParameters>>()
                .Where(x => x.IsActive)
                .ToList()
                .ForEach(x => x.DoAction(ref param));

            var tooManyObjects = islandObjects.Count() > 1 && !param.CanTakeAdditionalPlantation;
            var notAllowedQuarry = islandObjects.OfType<Quarry>().Any() &&
                                   !(param.CanTakeQuarryInsteadPlantation || isHasPrivilage);

            if (tooManyObjects || notAllowedQuarry)
            {
                return false;
            }

            var quarries = islandObjects.OfType<Quarry>();
            var plantations = islandObjects.OfType<Plantation>();

            foreach (var quarry in quarries)
            {
                var newQuarry = _mainBoardController.Status.Quarries.Dequeue();
                _playerStatus.Board.BuildQuarry(quarry);
            }

            foreach (var plantation in plantations)
            {
                _mainBoardController.Status.AvailablePlantations.Remove(plantation);
                _playerStatus.Board.BuildPlantation(plantation);
            }

            if (param.CanTakeAdditionalColonist)
            {
                foreach (var islandObject in islandObjects)
                {
                    _mainBoardController.Status.Colonists.Move(islandObject);
                }
            }

            return true;
        }

        public bool DoTradeAction(Goods value, bool isHasPrivilage)
        {
            var param = new TraderParameters();

            var traderBuildings =
                _playerStatus.Board.Buildings.OfType<BuildingBase<TraderParameters>>()
                    .Where(x => x.IsActive)
                    .ToList();
            traderBuildings.ForEach(x => x.DoAction(ref param));

            if (_mainBoardController.Status.Market.CanSellGood(value, param.PermissionToSellTheSame))
            {
                var money = _mainBoardController.Status.Market.SellGood(value, traderBuildings);
                if (money.HasValue)
                {
                    _playerStatus.Warehouse.RemoveGoods(new[] {value});
                    _mainBoardController.Status.Doubloons -= money.Value;
                    _playerStatus.ReceiveDoubloons(money.Value);

                    return true;
                }
            }

            return false;
        }

        public void DoCaptainAction(GoodsToShip goodsToShip)
        {
            var goods = goodsToShip.Goods;
            var ship = _mainBoardController.Status.Ships.Single(x => x.Space == goodsToShip.Ship.Space);
            var playerGoodsCount = _playerStatus.Warehouse.GetGoodsCount(goods);
            var goodsCount = Math.Min(playerGoodsCount, ship.FreeSpace);
            ship.AddGoods(goods, goodsCount);
            _playerStatus.Warehouse.RemoveGoods(goods, goodsCount);
            _playerStatus.AddVp(goodsCount);
            _mainBoardController.Status.Vp -= goodsCount;
        }

        public void DoCraftsmanAction(Dictionary<Goods, int> productions)
        {
            foreach (var production in productions)
            {
                _playerStatus.Warehouse.AddGoods(production.Key, production.Value);
                _mainBoardController.Status.Warehouse.RemoveGoods(production.Key, production.Value);
            }
        }

        public void DoCraftsmanActionReceiveAdditionalGoods(Goods additionalGoods)
        {
            _playerStatus.Warehouse.AddGoods(additionalGoods, 1);
            _mainBoardController.Status.Warehouse.RemoveGoods(additionalGoods, 1);
        }
    }
}