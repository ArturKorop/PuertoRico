using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.PlayerCore
{
    public class ManualPlayerConnection : IPlayerConnection
    {
        public string Name { get; }
        public int Id { get; }

        public ManualPlayerConnection(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public PlayerTurnAction DoTurn(Roles role, bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, PlayerStatus[] opponents)
        {
            throw new NotImplementedException();
        }

        public Roles SelectRole(PlayerStatus status, MainBoardStatus board, PlayerStatus[] opponents)
        {
            throw new NotImplementedException();
        }

        public void GameEnd(Dictionary<int, int> playersScore)
        {
            throw new NotImplementedException();
        }
    }
}