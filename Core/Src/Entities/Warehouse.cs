using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        public void AddGoods(Goods type, int count)
        {
            _goods[type] += count;
        }

        public void RemoveGoods(Goods type, int count)
        {
            _goods[type] -= count;

            if (_goods[type] < 0)
            {
                throw new InvalidOperationException("Too many goods for remove");
            }
        }

        public void RemoveGoods(IEnumerable<Goods> types)
        {
            foreach (var type in types)
            {
                _goods[type]--;

                if (_goods[type] < 0)
                {
                    throw new InvalidOperationException("Too many goods for remove");
                }
            }
        }

        public int GetGoodsCount(Goods type)
        {
            return _goods[type];
        }

        public string Display()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Warehouse:\n Corn: {0};\n Sugar: {1};\n Indigo: {2};\n Tabacco: {3};\n Coffee: {4};",
                _goods[Goods.Corn], _goods[Goods.Sugar], _goods[Goods.Indigo], _goods[Goods.Tabacco],
                _goods[Goods.Coffee]);

            return builder.ToString();
        }
    }

    public static class WarehouseExtensions
    {
        public static IEnumerable<Goods> GetAvailableGoods(this Warehouse warehouse)
        {
            return Enum.GetValues(typeof (Goods)).Cast<Goods>().Where(goods => warehouse.GetGoodsCount(goods) > 0);
        }
    }
}