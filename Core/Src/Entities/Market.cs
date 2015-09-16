using System.Collections.Generic;
using System.Linq;
using Core.Entities.Base;
using Core.Entities.RoleParameters;

namespace Core.Entities
{
    public class Market
    {
        public const int CornPrice = 0;

        public const int IndigoPrice = 1;

        public const int SugarPrice = 2;

        public const int TabaccoPrice = 3;

        public const int CoffeePrice = 4;

        public const int MaxGoodsCount = 4;

        public int FreeSpaces => MaxGoodsCount - _goodsToTrade.Count;

        private readonly Dictionary<Goods, int> _priceList;

        private readonly List<Goods> _goodsToTrade;

        public IEnumerable<Goods> GoodsToTrade => _goodsToTrade;

        public Market()
        {
            _goodsToTrade = new List<Goods>();
            _priceList = new Dictionary<Goods, int>
            {
                {Goods.Corn, CornPrice},
                {Goods.Indigo, IndigoPrice},
                {Goods.Sugar, SugarPrice},
                {Goods.Tabacco, TabaccoPrice},
                {Goods.Coffee, CoffeePrice}
            };
        }

        public bool CanSellGood(Goods good, bool permissionToSellTheSame)
        {
            if (FreeSpaces > 0)
            {
                if (!permissionToSellTheSame && _goodsToTrade.Any(x => x == good))
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public int? SimulateSellGoods(Goods good, IEnumerable<BuildingBase<TraderParameters>> buildings)
        {
            var traderParameters = new TraderParameters();

            foreach (var building in buildings)
            {
                building.DoAction(ref traderParameters);
            }

            if (CanSellGood(good, traderParameters.PermissionToSellTheSame))
            {
                return GetDefaultGoodPrice(good) + traderParameters.AdditionalPrice;
            }

            return null;
        }

        public int? SellGood(Goods good, IEnumerable<BuildingBase<TraderParameters>> buildings)
        {
            var price = SimulateSellGoods(good, buildings);
            if (price.HasValue)
            {
                _goodsToTrade.Add(good);
            }

            return price;
        }

        public IEnumerable<Goods> EndPhase()
        {
            if (FreeSpaces == 0)
            {
                var result = _goodsToTrade;
                _goodsToTrade.Clear();

                return result;
            }

            return null;
        }

        private int GetDefaultGoodPrice(Goods good)
        {
            return _priceList[good];
        }
    }
}