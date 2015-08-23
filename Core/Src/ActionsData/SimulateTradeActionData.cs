using System.Collections.Generic;
using Core.Entitites;

namespace Core.ActionsData
{
    public class SimulateTradeActionData
    {
        public IDictionary<Goods, int?> PriceList { get; private set; }

        public SimulateTradeActionData(int? corn, int? indigo, int? sugar, int? tabacco, int? coffee)
        {
            var data = new Dictionary<Goods, int?>();
            data.Add(Goods.Corn, corn);
            data.Add(Goods.Indigo, indigo);
            data.Add(Goods.Sugar, sugar);
            data.Add(Goods.Tabacco, tabacco);
            data.Add(Goods.Coffee, coffee);
        }
    }
}
