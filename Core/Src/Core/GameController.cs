using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Utils;

namespace Core.Core
{
    public class GameController
    {
        private readonly MainBoard _mainBoard;

        private readonly List<IPlayerConnection> _connections;

        private int _governer;

        private IPlayerConnection _currentPlayer;

        private int _previousPlayer;

        public int PlayersCount
        {
            get { return _connections.Count; }
        }

        public bool IsGameEnd
        {
            get
            {
                return _connections.Any(x => x.Manager.IsGameEnd);
            }
        }

        public GameController(params IPlayerConnection[] connections)
        {
            var playerCount = connections.Length;
            _mainBoard = new MainBoard(playerCount);
            _connections = Util.Shuffle(connections).ToList();
            var players = PlayerFactory.GeneratePlayers(_mainBoard, playerCount,
                _connections.Select(x => x.Name).ToArray());
            for (int i = 0; i < playerCount; i++)
            {
                var controller = new PlayerController(players[i], _mainBoard);
                _connections[i].Init(controller);
            }

            _governer = 0;
            _currentPlayer = _connections.Single(x=>x.Id == _governer);
            _previousPlayer = 0;
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
            var role = _currentPlayer.Manager.PlayFirstRoundTurn();
            var nextPlayer = GetNextPlayer(_currentPlayer);
            while (nextPlayer != null)
            {
                nextPlayer.Manager.PlayRoundTurn(role);
                nextPlayer = GetNextPlayer(nextPlayer);
            }
        }

        private IPlayerConnection GetNextPlayer(IPlayerConnection currentPlayer)
        {
            var firstPlayerOrder = _currentPlayer.Id;
            var currentPlayerOrder = currentPlayer.Id;
            int nextPlayerOrder = currentPlayerOrder + 1;
            if (nextPlayerOrder == PlayersCount)
            {
                nextPlayerOrder = 0;
            }

            if (nextPlayerOrder == firstPlayerOrder)
            {
                return null;
            }

            return _connections.Single(x => x.Id == nextPlayerOrder);
        }
    }
}