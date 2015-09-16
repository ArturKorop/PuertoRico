using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Interfaces;

namespace Core.Entities
{
    public class MainBoardController
    {
        private readonly MainBoardStatus _status;

        public MainBoardController(int playersCount)
        {
            _status = new MainBoardStatus(playersCount);
        }

        //Todo: implement clone
        public MainBoardStatus Status => _status;

        public int TakeDoubloons(int doubloons)
        {
            if (_status.Doubloons < doubloons)
            {
                throw new InvalidOperationException("Too less doubloons");
            }

            _status.Doubloons -= doubloons;

            return doubloons;
        }

        public void ReceiveDoubloons(int doubloons)
        {
            _status.Doubloons += doubloons;
        }

        public void ReceiveGoods(IEnumerable<Goods> goods)
        {
            _status.Warehouse.AddGoods(goods);
        }

        public void ReceiveGoods(IEnumerable<Tuple<Goods, int>> goods)
        {
            foreach (var tuple in goods)
            {
                _status.Warehouse.AddGoods(tuple.Item1, tuple.Item2);
            }
        }

        public void UpdateCurrentPlantations()
        {
            for (int i = 0; i < _status.OpenPlantationsCount; i++)
            {
                _status.AvailablePlantations.Add(_status.Plantations.Dequeue());
            }
        }

        public bool UpdateAvailableColonists(IEnumerable<IEnumerable<IBuilding>> allPlayerBuildings)
        {
            var expectedColonistsCount =
                allPlayerBuildings.SelectMany(x => x.ToList()).Sum(x => x.MaxColonistsCount - x.CurrentColonistsCount);

            if (expectedColonistsCount <= _status.Colonists.CurrentColonistsCount)
            {
                _status.Colonists.Move(_status.AvailableColonists, expectedColonistsCount);

                return true;
            }

            return false;
        }

        public bool TryGetRoleCard(Roles currentRole, out RoleCard roleCard)
        {
            // TODO: add find with max money
            roleCard  = _status.RoleCards.FirstOrDefault(x => x.Role == currentRole && !x.IsUsed);

            return roleCard != null;
        }
    }
}