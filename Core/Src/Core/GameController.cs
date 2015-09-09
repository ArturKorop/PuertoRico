using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Interfaces;
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
            var currentOpponents = _players.Opponents(_governor);

            var availableRoleCars = _mainBoardController.Status.RoleCards.Where(x => !x.IsUsed).Select(x=>x as RoleCardStatus).ToList();

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

                foreach (var id in playerIds)
                {
                    var status = _players[id].Status;
                    var connection = _players[id].Connection;
                    var controller = _players[id].Controller;
                    var isHasPrivilage = status.Id == _governor;
                    var availableBuildings = _mainBoardController.Status.Buildings;

                    switch (roleCard.Role)
                    {
                            case Roles.Builder:
                        {
                            var building = connection.SelectBuildingToBuild(isHasPrivilage, status, availableBuildings,
                                _players.Opponents(id));

                            bool isSuccessfull = controller.DoSelectBuildingToBuild(building);

                        }
                            break;
                    }

                    //var turnAction = connection.DoTurn(cardStatus.Role, status.Id == _governor, status,
                    //    _mainBoardController.Status,
                    //    _players.Where(x => x.Key != id).Select(x => x.Value.Status).ToArray());

                    //_players[id].Controller.DoTurnAction(turnAction, status.Id == _governor);
                }
            }
            else if (cardStatus.IsRequiredCurrentPlayerAction())
            {
                //var turnAction = currentConnection.DoTurn(cardStatus.Role, true, currentPlayerStatus, _mainBoardController.Status,
                //    _players.Where(x => x.Key != currentPlayerStatus.Id).Select(x => x.Value.Status).ToArray());

                //_players[currentPlayerStatus.Id].Controller.DoTurnAction(turnAction, currentPlayerStatus.Id == _governor);
            }
            else
            {
                _players[currentPlayerStatus.Id].Controller.DoNoPlayerAction(cardStatus.Role);
            }


            _governor = GetNextGovernor();
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

        public bool DoTurnAction(Roles role, PlayerTurnAction turnAction, bool isPrivilage)
        {
            switch (role)
            {

            }
            return true;
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

        public bool DoSelectBuildingToBuild(IBuilding building)
        {

            return false;
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