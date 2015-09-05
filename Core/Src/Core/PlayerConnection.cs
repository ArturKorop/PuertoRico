using System.Collections.Generic;
using Core.Entities;

namespace Core.Core
{
    public class PlayerConnection : IPlayerConnection
    {
        private readonly PlayerController _controller;

        public string Name { get; }

        public int Id => _controller.PlayerStatus.Id;

        public PlayerConnection(string name, PlayerController controller)
        {
            Name = name;
            _controller = controller;
        }

        public PlayerTurnAction DoTurn(Roles role, bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, PlayerStatus[] opponents)
        {
            return _controller.DoTurn(role, isHasPrivilage, status, board, opponents);
        }

        public Roles SelectRole(PlayerStatus status, MainBoardStatus board, PlayerStatus[] opponents)
        {
            throw new System.NotImplementedException();
        }

        public void GameEnd(Dictionary<int, int> playersScore)
        {
            throw new System.NotImplementedException();
        }
    }
}