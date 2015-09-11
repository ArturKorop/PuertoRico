using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Entities.Interfaces;
using Core.Entities.IslandObjects;

namespace Core.PlayerCore
{
    public class ManualPlayerConnection : IPlayerConnection
    {
        public string Name { get; }

        public ManualPlayerConnection(string name)
        {
            Name = name;
        }

        public RoleCardStatus SelectRole(List<RoleCardStatus> cards, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
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

        public IBuilding SelectBuildingToBuild(bool isHasPrivilage, PlayerStatus status, Dictionary<IBuilding, int> availableBuildings,
            IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public MoveDirection MoveColonist(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods? SelectGoodsToTrade(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods SelectAdditionalGoods(PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IISlandObject> SelectISlandObjects(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public GoodsToShip SelectGoodsToShip(PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Goods> SelectGoodsForWarehouse(PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods? SelectOneGoodsForStore(PlayerStatus status, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public bool IsUsePrivilage(PlayerStatus status, MainBoardStatus mainBoardStatus, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
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