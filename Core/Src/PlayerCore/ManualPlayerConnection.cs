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

        public RoleCardStatus SelectRole(List<RoleCardStatus> cards, PlayerStatus status, MainBoardStatus board,
            IEnumerable<PlayerStatus> opponents)
        {
            Console.WriteLine("Select role: ");

            var i = 0;
            var roles = cards.ToDictionary(roleCardStatus => i++);

            foreach (var roleCardStatuse in roles)
            {
                Console.WriteLine("{0}: {1}", roleCardStatuse.Key, roleCardStatuse.Value.Role);
            }

            var key = int.Parse(Console.ReadLine());
            var role = roles[key];

            return role;
        }

        public IBuilding SelectBuildingToBuild(bool isHasPrivilage, PlayerStatus status,
            MainBoardStatus board,
            IEnumerable<PlayerStatus> opponents)
        {
            var availableBuildings = board.Buildings;
            Console.WriteLine("{0}: {1} Doubloons", status.Name, status.Doubloons);
            var buildings = new Dictionary<int, Tuple<IBuilding, int>>();
            Console.WriteLine("Select building: ");
            var i = 0;
            foreach (var availableBuilding in availableBuildings)
            {
                buildings.Add(i, new Tuple<IBuilding, int>(availableBuilding.Key, availableBuilding.Value));
                Console.WriteLine("{0}: {1}[{3}] - {2}", i, availableBuilding.Key.GetType().Name, availableBuilding.Value, availableBuilding.Key.Cost);
                i++;
            }
            
            Console.WriteLine("-1: Quit");

            var number = int.Parse(Console.ReadLine());

            if (number == -1)
            {
                return null;
            }

            var buildingToBuild = buildings[number].Item1;

            return buildingToBuild;
        }

        public MoveDirection MoveColonist(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board,
            IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods? SelectGoodsToTrade(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board,
            IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods SelectAdditionalGoods(PlayerStatus status, MainBoardStatus board,
            IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IISlandObject> SelectISlandObjects(bool isHasPrivilage, PlayerStatus status,
            MainBoardStatus board, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public GoodsToShip SelectGoodsToShip(PlayerStatus status, MainBoardStatus board,
            IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Goods> SelectGoodsForWarehouse(PlayerStatus status, MainBoardStatus board,
            IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public Goods? SelectOneGoodsForStore(PlayerStatus status, IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public bool IsUsePrivilage(PlayerStatus status, MainBoardStatus mainBoardStatus,
            IEnumerable<PlayerStatus> opponents)
        {
            throw new NotImplementedException();
        }

        public bool IsTakeAdditionalColonist(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, List<PlayerStatus> opponents)
        {
            Console.WriteLine("Take additional colonsit?\n y/n");

            return Console.ReadKey().KeyChar == 'y';
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