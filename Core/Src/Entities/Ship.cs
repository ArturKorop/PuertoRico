using System;

namespace Core.Entities
{
    public class Ship
    {
        private int _usedSpace;

        private Goods? _goodsType;

        public int Space { get; }

        public int FreeSpace => Space - _usedSpace;

        public Goods? Type => _goodsType;

        public Ship(int space)
        {
            Space = space;
            _usedSpace = 0;
            _goodsType = null;
        }

        public bool AddGoods(Goods type, int count)
        {
            if (count > FreeSpace)
            {
                return false;
            }

            if (_goodsType.HasValue && _goodsType.Value != type)
            {
                return false;
            }

            _goodsType = type;
            _usedSpace = count;

            return true;
        }

        public Tuple<Goods, int> FinishRound()
        {
            if (FreeSpace == 0 && _goodsType.HasValue)
            {
                var result = new Tuple<Goods, int>(_goodsType.Value, _usedSpace);
                Clear();

                return result;
            }

            return null;
        }

        private void Clear()
        {
            _usedSpace = 0;
            _goodsType = null;
        }
    }
}