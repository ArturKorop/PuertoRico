using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities.RoleParameters
{
    public class CraftsmanParameters
    {
        public Dictionary<Goods, int> GoodsProduction { get; } = new Dictionary<Goods, int>();

        public bool TakeAdditionalDoubloonsForProduction { get; set; }

        public CraftsmanParameters()
        {
            foreach (var item in Enum.GetValues(typeof (Goods)).OfType<Goods>())
            {
                GoodsProduction.Add(item, 0);
            }
        }
    }
}