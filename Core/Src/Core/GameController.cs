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
        private readonly MainBoardController _mainBoardController;

        private readonly List<IPlayerConnection> _connections;

        private readonly int _governer;

        private readonly IPlayerConnection _currentPlayer;

        private int _previousPlayer;

        public int PlayersCount => _connections.Count;

        public bool IsGameEnd
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public GameController(params IPlayerConnection[] connections)
        {
            var playerCount = connections.Length;
            _mainBoardController = new MainBoardController(playerCount);
            _connections = Util.Shuffle(connections).ToList();
            var players = PlayerStatusFactory.GeneratePlayers(_mainBoardController, playerCount,
                _connections.Select(x => x.Name).ToArray());

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
            throw new NotImplementedException();
        }
    }
}