using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Warehouse
    {
        private readonly Dictionary<Goods, int> _goods;

        public Warehouse(int corn, int indigo, int sugar, int coffee, int tabacco)
        {
            _goods = new Dictionary<Goods, int>
            {
                {Goods.Corn, corn},
                {Goods.Indigo, indigo},
                {Goods.Sugar, sugar},
                {Goods.Coffee, coffee},
                {Goods.Tabacco, tabacco}
            };
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

                if (_goods[good] < 0)
                {
                    throw new InvalidOperationException("SendGoods");
                }
            }
        }

        public int GetGoodsCount(Goods type)
        {
            return _goods[type];
        }

        public string Status()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Warehouse:\n Corn: {0};\n Sugar: {1};\n Indigo: {2};\n Tabacco: {3};\n Coffee: {4};",
                _goods[Goods.Corn], _goods[Goods.Sugar], _goods[Goods.Indigo], _goods[Goods.Tabacco],
                _goods[Goods.Coffee]);

            return builder.ToString();
        }
    }
}