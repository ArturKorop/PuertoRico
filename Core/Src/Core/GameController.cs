using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
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

        private readonly List<PlayerStatus> _players;

        public int PlayersCount => _connections.Count;

        public bool IsGameEnd => false;

        public GameController(IVisualizer visualizer, params IPlayerConnection[] connections)
        {
            InitEvents(visualizer);

            var playerCount = connections.Length;
            _mainBoardController = new MainBoardController(playerCount);
            _connections = Util.Shuffle(connections).ToList();
            _players = PlayerStatusFactory.GeneratePlayers(_mainBoardController, playerCount,
                _connections.Select(x => x.Name).ToArray());

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
            var currentPlayerStatus = _players.Single(x => x.Id == _governor);
            var currentConnection = _connections.Single(x => x.Name == currentPlayerStatus.Name);
            var availableRoleCars = _mainBoardController.Status.RoleCards.Where(x => !x.IsUsed).ToList();
            var roleCard = currentConnection.SelectRole(availableRoleCars, currentPlayerStatus,
                _mainBoardController.Status,
                _players.Where(x => x.Id != currentPlayerStatus.Id).ToArray());

            OnPlayerSelectRole?.Invoke(roleCard, _governor, currentPlayerStatus.Name);

            if (roleCard.IsRequiredAllPlayerActions())
            {
                var playerIds = GeneratePlayersOrder(_governor);

                foreach (var id in playerIds)
                {
                    var status = _players.Single(x => x.Id == id);
                    var connection = _connections.Single(x => x.Name == status.Name);
                    var turnAction = connection.DoTurn(roleCard.Role, status.Id == _governor, status,
                        _mainBoardController.Status, _players.Where(x => x.Id != id).ToArray());


                }
            }
            else if (roleCard.IsRequiredCurrentPlayerAction())
            {
                currentConnection.DoTurn(roleCard.Role, true, currentPlayerStatus, _mainBoardController.Status,
                    _players.Except(new[] {currentPlayerStatus}).ToArray());
            }
            else
            {
                
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
}