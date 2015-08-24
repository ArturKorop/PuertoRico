using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Warehouse
    {
        private readonly Dictionary<Goods, int> _goods;

        public Warehouse(int corn, int indigo, int sugar, int coffee, int tabacco)
        {
            _goods = new Dictionary<Goods, int>();
            _goods.Add(Goods.Corn, corn);
            _goods.Add(Goods.Indigo, indigo);
            _goods.Add(Goods.Sugar, sugar);
            _goods.Add(Goods.Coffee, coffee);
            _goods.Add(Goods.Tabacco, tabacco);
        }

        public Warehouse()
            : this(0, 0, 0, 0, 0)
        {
        }

        public void AddGoods(IEnumerable<Goods> goods)
        {
            foreach (var good in goods)
            {
                _goods[good]++;
            }
        }

        public void RemoveGoods(IEnumerable<Goods> goods)
        {
            foreach (var good in goods)
            {
                _goods[good]--;

                if(_goods[good] < 0)
                {
                    throw new InvalidOperationException("SendGoods");
                }
            }
        }

        public int GetGoodCount(Goods type)
        {
            return _goods[type];
        }
    }
}
