using System.Collections.Generic;
using System.Linq;
using Core.Entities;

namespace Core.Utils
{
    public static class MainFactory
    {
        public static IEnumerable<Quarry> GenerateQuarries(int count)
        {
            var result = new List<Quarry>();
            for (int i = 0; i < count; i++)
            {
                result.Add(new Quarry());
            }

            return result;
        }

        public static IEnumerable<Plantation> GeneratePlantations(int count, Goods type)
        {
            var result = new List<Plantation>();
            for (int i = 0; i < count; i++)
            {
                result.Add(new Plantation(type));
            }

            return result;
        }

        public static IEnumerable<Plantation> GenerateAllPlantations(int corn, int tabacco, int sugar, int indigo,
            int coffee)
        {
            var result = new List<Plantation>();
            result.AddRange(GeneratePlantations(corn, Goods.Corn));
            result.AddRange(GeneratePlantations(tabacco, Goods.Tabacco));
            result.AddRange(GeneratePlantations(sugar, Goods.Sugar));
            result.AddRange(GeneratePlantations(indigo, Goods.Indigo));
            result.AddRange(GeneratePlantations(coffee, Goods.Coffee));

            return result;
        }

        public static List<Ship> GenerateShips(params int[] spaces)
        {
            return spaces.Select(space => new Ship(space)).ToList();
        }

        public static List<RoleCard> GenerateRoleCards(params Roles[] roles)
        {
            return roles.Select(x => new RoleCard(x)).ToList();
        }
    }
}