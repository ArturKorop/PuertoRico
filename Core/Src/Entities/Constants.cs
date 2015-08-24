using System.Collections.Generic;

namespace Core.Entities
{
    public static class Constants
    {
        public static readonly Dictionary<int, int> VpByPlayers = new Dictionary<int, int>
        {
            {3, 75},
            {4, 100},
            {5, 122}
        };

        public static readonly Dictionary<int, int[]> ShipsByPlayers = new Dictionary<int, int[]>
        {
            {3, new[] {4, 5, 6}},
            {4, new[] {7, 5, 6}},
            {5, new[] {8, 7, 6}},
        };

        public static readonly Dictionary<int, int> ColonistsByPlayers = new Dictionary<int, int>
        {
            {3, 55},
            {4, 75},
            {5, 95},
        };

        public static readonly Dictionary<int, Roles[]> RolesByPlayers = new Dictionary<int, Roles[]>
        {
            {3, new[] {Roles.Builder, Roles.Captain, Roles.Craftsman, Roles.Mayor, Roles.Settler, Roles.Trader}},
            {
                4,
                new[]
                {
                    Roles.Builder, Roles.Captain, Roles.Craftsman, Roles.Mayor, Roles.Settler, Roles.Trader,
                    Roles.Prospector,
                }
            },
            {
                5,
                new[]
                {
                    Roles.Builder, Roles.Captain, Roles.Craftsman, Roles.Mayor, Roles.Settler, Roles.Trader,
                    Roles.Prospector, Roles.Prospector
                }
            },
        };

        public static readonly Dictionary<int, Dictionary<Goods, int>> PlantationsByPlayers = new Dictionary
            <int, Dictionary<Goods, int>>
        {
            {
                3, new Dictionary<Goods, int>
                {
                    {Goods.Indigo, 6},
                    {Goods.Corn, 7},
                }
            },
            {
                4, new Dictionary<Goods, int>
                {
                    {Goods.Indigo, 6},
                    {Goods.Corn, 6},
                }
            },
            {
                5, new Dictionary<Goods, int>
                {
                    {Goods.Indigo, 5},
                    {Goods.Corn, 6},
                }
            },
        };
    }
}
