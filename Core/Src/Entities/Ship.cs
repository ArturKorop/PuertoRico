namespace Core.Entities
{
    public class Ship
    {
        private int _usedSpace;

        private Goods? _goodsType;

        public int Space { get; private set; }

        public int FreeSpace
        {
            get { return Space - _usedSpace; }
        }

        public Goods? Type
        {
            get { return _goodsType; }
        }

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

        public void Clear()
        {
            _usedSpace = 0;
            _goodsType = null;
        }
    }
}