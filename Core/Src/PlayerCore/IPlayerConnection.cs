using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Entities.Interfaces;
using Core.Entities.IslandObjects;

namespace Core.PlayerCore
{
    public interface IPlayerConnection
    {
        string Name { get; }

        RoleCardStatus SelectRole(List<RoleCardStatus> cards, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents);

        IBuilding SelectBuildingToBuild(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents);

        MoveDirection MoveColonist(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents);

        Goods? SelectGoodsToTrade(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents);

        Goods SelectAdditionalGoods(PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents);

        IEnumerable<IISlandObject> SelectISlandObjects(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents);

        GoodsToShip SelectGoodsToShip(PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents);

        IEnumerable<Goods> SelectGoodsForWarehouse(PlayerStatus status, MainBoardStatus board, IEnumerable<PlayerStatus> opponents);

        Goods? SelectOneGoodsForStore(PlayerStatus status, IEnumerable<PlayerStatus> opponents);

        bool IsUsePrivilage(PlayerStatus status, MainBoardStatus mainBoardStatus, IEnumerable<PlayerStatus> opponents);

        bool IsTakeAdditionalColonist(bool isHasPrivilage, PlayerStatus status, MainBoardStatus board, List<PlayerStatus> opponents);

        void GameEnd(Dictionary<int, int> playersScore);
    }

    public class MoveDirection
    {
        public IColonistsHolder Source { get; set; }

        public IColonistsHolder Destination { get; set; }
    }

    public class GoodsToShip
    {
        public Goods Goods { get; set; }

        public Ship Ship { get; set; }
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