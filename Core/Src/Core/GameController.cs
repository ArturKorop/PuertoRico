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
    public class GameController
    {
        public event Action<RoleCard, int, string> OnPlayerSelectRole;

        private readonly MainBoardController _mainBoardController;

        private readonly List<IPlayerConnection> _connections;

        private int _governor;

        private readonly Dictionary<int, PlayerContainer> _players;

        public int PlayersCount => _connections.Count;

        public bool IsGameEnd => false;

        public GameController(IVisualizer visualizer, params IPlayerConnection[] connections)
        {
            InitEvents(visualizer);

            var playerCount = connections.Length;
            _mainBoardController = new MainBoardController(playerCount);
            _connections = Util.Shuffle(connections).ToList();
            var players = PlayerStatusFactory.GeneratePlayers(_mainBoardController, playerCount,
                _connections.Select(x => x.Name).ToArray());

            _players = players.ToDictionary(x => x.Id,
                x =>
                    new PlayerContainer
                    {
                        Connection = _connections.Single(c => c.Name == x.Name),
                        Status = x,
                        Controller = new PlayerController(_mainBoardController, x)
                    });

            _governor = 0;
        }

        private void InitEvents(IVisualizer visualizer)
        {
            OnPlayerSelectRole += visualizer.OnPlayerSelectRoleHandler;
        }

        public void Start()
        {
            while (!IsGameEnd)
            {
                PlayRound();
            }
        }

        private void PlayRound()
        {
            var current = _players[_governor];
            var currentPlayerStatus = current.Status;
            var currentPlayerController = current.Controller;
            var currentConnection = current.Connection;
            var currentOpponents = _players.Opponents(_governor).ToList();

            var availableRoleCars =
                _mainBoardController.Status.RoleCards.Where(x => !x.IsUsed).Select(x => x as RoleCardStatus).ToList();

            var cardStatus = currentConnection.SelectRole(availableRoleCars, currentPlayerStatus,
                _mainBoardController.Status,
                currentOpponents);

            RoleCard roleCard;
            //TODO: Should be in while cycle
            _mainBoardController.TryGetRoleCard(cardStatus.Role, out roleCard);
            _players[_governor].Controller.DoRoleAction(roleCard);

            OnPlayerSelectRole?.Invoke(roleCard, _governor, currentPlayerStatus.Name);

            if (cardStatus.IsRequiredAllPlayerActions())
            {
                var playerIds = GeneratePlayersOrder(_governor);

                if (roleCard.Role == Roles.Mayor)
                {
                    var isSuccessfull = DoMayorInitAction(currentPlayerStatus, _mainBoardController, playerIds);
                }

                foreach (var id in playerIds)
                {
                    var status = _players[id].Status;
                    var connection = _players[id].Connection;
                    var controller = _players[id].Controller;
                    var isHasPrivilage = status.Id == _governor;
                    var availableBuildings = _mainBoardController.Status.Buildings;
                    var opponents = _players.Opponents(id).ToList();

                    switch (roleCard.Role)
                    {
                        case Roles.Builder:
                            DoBuilderAction(connection, isHasPrivilage, status, availableBuildings, opponents,
                                controller);
                            break;
                        case Roles.Mayor:
                            DoMayorAction(isHasPrivilage, connection, status, opponents, controller);
                            break;
                        case Roles.Settler:
                            DoSettlerAction(connection, isHasPrivilage, status, opponents, controller);
                            break;
                        case Roles.Trader:
                            DoTradeAction(connection, isHasPrivilage, status, opponents, controller);
                            break;
                        default:
                            throw new InvalidOperationException("Wrong Role");
                    }
                }

                if (roleCard.Role == Roles.Trader)
                {
                    DoTradeFinishAction(_mainBoardController);
                }
            }
            else if (cardStatus.IsRequiredCurrentPlayerAction())
            {
                DoCraftsmanAction(currentConnection, currentPlayerController, _mainBoardController.Status,
                    currentOpponents, currentPlayerStatus);
            }
            else if (cardStatus.IsRequiredAllPlayersActionSeveralTimes())
            {
                var currentPlayer = _governor;
                while (!IsCaptaionActionFinished())
                {
                    var status = _players[currentPlayer].Status;
                    var connection = _players[currentPlayer].Connection;
                    var controller = _players[currentPlayer].Controller;
                    var isHasPrivilage = status.Id == _governor;
                    var opponents = _players.Opponents(currentPlayer).ToList();

                    DoCaptainAction(status, connection, controller, isHasPrivilage, opponents);
                    currentPlayer = GetNextPlayer(currentPlayer);
                }

                DoCaptainFinishAction(_mainBoardController);
            }
            else
            {
                _players[currentPlayerStatus.Id].Controller.DoNoPlayerAction(cardStatus.Role);
            }


            _governor = GetNextGovernor();

            CheckForNextRound(_mainBoardController.Status);
        }

        private void DoCraftsmanAction(IPlayerConnection currentConnection, PlayerController currentPlayerController,
            MainBoardStatus boardStatus, IEnumerable<PlayerStatus> currentOpponents, PlayerStatus currentPlayerStatus)
        {
            var players = GeneratePlayersOrder(_governor);

            foreach (var player in players)
            {
                var status = _players[player].Status;

                var param = new CraftsmanParameters();
                status.Board.Buildings.OfType<GoodsFactoryBase>().ToList().ForEach(x => x.DoAction(ref param));

                var plantations =
                    status.Board.Plantations.Where(x => x.ActivePoints > 0).Select(x => x.Type).GroupBy(x => x);

                var production = plantations.ToDictionary(plantation => plantation.Key,
                    plantation => Math.Min(plantation.Count(), param.GoodsProduction[plantation.Key]));

                currentPlayerController.DoCraftsmanAction(production);
            }

            var additionalGoods = currentConnection.SelectAdditionalGoods(currentPlayerStatus, boardStatus,
                currentOpponents);

            currentPlayerController.DoCraftsmanActionReceiveAdditionalGoods(additionalGoods);
        }

        private void DoCaptainFinishAction(MainBoardController mainBoardController)
        {
            var freeGoods = mainBoardController.Status.Ships.Select(x => x.FinishRound()).Where(x => x != null);
            mainBoardController.Status.ReceiveGoods(freeGoods);
        }

        private void DoCaptainAction(PlayerStatus status, IPlayerConnection connection, PlayerController controller,
            bool isHasPrivilage, List<PlayerStatus> opponents)
        {
            var goodsToShip = connection.SelectGoodsToShip(status, _mainBoardController.Status, opponents);
            controller.DoCaptainAction(goodsToShip);
        }

        private bool IsCaptaionActionFinished()
        {
            var allShipsFull = _mainBoardController.Status.Ships.All(x => x.FreeSpace == 0);

            var playersGoods = _players.Values.SelectMany(x => x.Status.Warehouse.GetAvailableGoods()).Distinct();
            var playersNotHaveGoodsForShips =
                _mainBoardController.Status.Ships.Where(x => x.FreeSpace != 0)
                    .Select(x => x.Type)
                    .Any(x => x.HasValue && playersGoods.Contains(x.Value));

            return allShipsFull || playersNotHaveGoodsForShips;
        }

        private void DoTradeFinishAction(MainBoardController mainBoardController)
        {
            var market = mainBoardController.Status.Market;
            var endPhaseResult = market.EndPhase();

            if (endPhaseResult != null)
            {
                mainBoardController.Status.ReceiveGoods(endPhaseResult);
            }
        }

        private void DoTradeAction(IPlayerConnection connection, bool isHasPrivilage, PlayerStatus status,
            List<PlayerStatus> opponents, PlayerController controller)
        {
            var goodsForTrade = connection.SelectGoodsToTrade(isHasPrivilage, status, _mainBoardController.Status,
                opponents);

            if (goodsForTrade.HasValue)
            {
                controller.DoTradeAction(goodsForTrade.Value, isHasPrivilage);
            }
        }

        private void DoSettlerAction(IPlayerConnection connection, bool isHasPrivilage, PlayerStatus status,
            List<PlayerStatus> opponents, PlayerController controller)
        {
            var islandObjects = connection.SelectISlandObjects(isHasPrivilage, status, _mainBoardController.Status,
                opponents);
            controller.DoSettlerAction(islandObjects.ToList(), isHasPrivilage);
        }

        private bool DoMayorInitAction(PlayerStatus currentPlayerStatus, MainBoardController mainBoardController,
            List<int> playerIds)
        {
            playerIds.Insert(0, currentPlayerStatus.Id);
            var playersCount = playerIds.Count;
            foreach (var playerId in playerIds)
            {
                var currentTotalColonistsCount = _mainBoardController.Status.AvailableColonists.CurrentColonistsCount;
                var status = _players[playerId].Status;
                var colonistsCount = currentTotalColonistsCount%playersCount + currentTotalColonistsCount/playersCount;

                _mainBoardController.Status.Colonists.Move(status.Board.ColonistsWarehouse, colonistsCount);

                playersCount--;
            }

            return true;
        }

        private void CheckForNextRound(MainBoardStatus status)
        {
            var allRoleCards = status.RoleCards;
            if (allRoleCards.Count(x => x.IsUsed) == PlayersCount)
            {
                allRoleCards.ToList().ForEach(x => x.NextRound());
            }
        }

        private void DoMayorAction(bool isHasPrivilage, IPlayerConnection connection, PlayerStatus status,
            List<PlayerStatus> opponents,
            PlayerController controller)
        {
            if (isHasPrivilage)
            {
                var usePrivilage = connection.IsUsePrivilage(status, _mainBoardController.Status,
                    opponents);
                controller.DoMayorActionTakeAdditionalColinists(usePrivilage);
            }

            Func<MoveDirection> moveDirectionAction =
                () => connection.MoveColonist(isHasPrivilage, status, _mainBoardController.Status,
                    opponents);
            var moveDirection = moveDirectionAction();
            while (moveDirection != null)
            {
                controller.DoMoveColonistAction(moveDirection);
                moveDirection = moveDirectionAction();
            }
        }

        private void DoBuilderAction(IPlayerConnection connection, bool isHasPrivilage, PlayerStatus status,
            Dictionary<IBuilding, int> availableBuildings, List<PlayerStatus> opponents, PlayerController controller)
        {
            var building = connection.SelectBuildingToBuild(isHasPrivilage, status, availableBuildings,
                opponents);

            bool isSuccessfull = controller.DoSelectBuildingToBuild(building, isHasPrivilage);
        }

        private List<int> GeneratePlayersOrder(int governor)
        {
            var result = new List<int> {governor};
            for (int i = 1; i < PlayersCount; i++)
            {
                var nextId = governor + i < PlayersCount ? governor + 1 : 0;
                result.Add(nextId);
            }

            return result;
        }

        private int GetNextGovernor()
        {
            var nextGovernor = _governor < _players.Count - 1 ? _governor + 1 : 0;

            return nextGovernor;
        }

        private int GetNextPlayer(int currentPlayer)
        {
            var nextPlayer = currentPlayer < _players.Count - 1 ? currentPlayer + 1 : 0;

            return nextPlayer;
        }
    }

    public class PlayerController
    {
        private readonly PlayerStatus _playerStatus;

        private readonly MainBoardController _mainBoardController;

        public PlayerController(MainBoardController mainBoardController, PlayerStatus playerStatus)
        {
            _mainBoardController = mainBoardController;
            _playerStatus = playerStatus;
        }

        public Roles DoRoleAction(RoleCard card)
        {
            if (card.IsUsed)
            {
                throw new InvalidOperationException("Role card is used");
            }

            var data = card.Take();
            _playerStatus.ReceiveDoubloons(data.Item2);

            return data.Item1;
        }

        public bool DoNoPlayerAction(Roles role)
        {
            switch (role)
            {
                case Roles.Prospector:
                    ReceiveProspectorMoney();
                    return true;
                default:
                    return false;
            }
        }

        private void ReceiveProspectorMoney()
        {
            _playerStatus.ReceiveDoubloons(_mainBoardController.TakeDoubloons(GameConstants.ProspectorMoney));
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
                    .Where(x => x.ActivePoints > 0)
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
                .Where(x => x.ActivePoints > 0)
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
                    .Where(x => x.ActivePoints > 0)
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

    public static class Extensions
    {
        public static IEnumerable<PlayerStatus> Opponents(this Dictionary<int, PlayerContainer> players,
            int currentPlayerId)
        {
            return players.Where(x => x.Key != currentPlayerId).Select(x => x.Value.Status);
        }
    }
}