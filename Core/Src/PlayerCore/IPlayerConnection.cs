using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.PlayerCore
{
    public interface IPlayerConnection
    {
        string Name { get; }

        PlayerTurnAction DoTurn(Roles role, bool isHasPrivilage, PlayerStatus status, MainBoardStatus board,
            PlayerStatus[] opponents);

        RoleCard SelectRole(List<RoleCard> cards, PlayerStatus status, MainBoardStatus board, PlayerStatus[] opponents);
        void GameEnd(Dictionary<int, int> playersScore);
    }

    public interface IVisualizer
    {
        void OnPlayerSelectRoleHandler(RoleCard roleCard, int id, string name);
    }

    public class ConsoleVisualizer : IVisualizer
    {
        public void OnPlayerSelectRoleHandler(RoleCard roleCard, int id, string name)
        {
            Console.WriteLine($"{name}[{id}]: {roleCard.Role}");
        }
    }
}