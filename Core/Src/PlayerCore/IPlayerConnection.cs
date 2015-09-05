using System.Collections.Generic;
using Core.Entities;

namespace Core.PlayerCore
{
    public interface IPlayerConnection
    {
        string Name { get; }
        int Id { get; }

        PlayerTurnAction DoTurn(Roles role, bool isHasPrivilage, PlayerStatus status, MainBoardStatus board,
            PlayerStatus[] opponents);

        Roles SelectRole(PlayerStatus status, MainBoardStatus board, PlayerStatus[] opponents);
        void GameEnd(Dictionary<int, int> playersScore);
    }
}