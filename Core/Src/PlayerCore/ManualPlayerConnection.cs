using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.PlayerCore
{
    public class ManualPlayerConnection : IPlayerConnection
    {
        public string Name { get; }

        public ManualPlayerConnection(string name)
        {
            Name = name;
        }

        public PlayerTurnAction DoTurn(Roles role, bool isHasPrivilage, PlayerStatus status, MainBoardStatus board,
            PlayerStatus[] opponents)
        {
            Console.WriteLine(role);

            return new PlayerTurnAction();
        }

        public RoleCard SelectRole(List<RoleCard> cards, PlayerStatus status, MainBoardStatus board, PlayerStatus[] opponents)
        {
            cards.Select(x=>x.Role).PE();

            var key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case 'b':
                    return cards.Single(x => x.Role == Roles.Builder);
                case 'c':
                    return cards.Single(x => x.Role == Roles.Captain);
                case 'n':
                    return cards.Single(x => x.Role == Roles.Craftsman);
                case 'm':
                    return cards.Single(x => x.Role == Roles.Mayor);
                case 't':
                    return cards.Single(x => x.Role == Roles.Trader);
                case 's':
                    return cards.Single(x => x.Role == Roles.Settler);
                case 'p':
                    return cards.Single(x => x.Role == Roles.Prospector);
                default:
                    throw new InvalidOperationException("Invorrect role");
            }
        }

        public void GameEnd(Dictionary<int, int> playersScore)
        {
            throw new NotImplementedException();
        }
    }

    public static class PrintExtensions
    {
        public static void PrintEnuberable<T>(this IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static void PE<T>(this IEnumerable<T> source)
        {
            source.PrintEnuberable();
        }
    }
}