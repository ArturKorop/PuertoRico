using System.Collections.Generic;
using Core.Entitites;

namespace Core.Utils
{
    public static class MainFactory
    {
        public static IEnumerable<Quarry> GetQuarries(int count)
        {
            var result = new List<Quarry>();
            for (int i = 0; i < count; i++)
            {
                result.Add(new Quarry());
            }

            return result;
        }

        public static IEnumerable<Plantation> GetPlantions(int count, Goods type)
        {
            var result = new List<Plantation>();
            for (int i = 0; i < count; i++)
            {
                result.Add(new Plantation(type));
            }

            return result;
        }
    }
}
