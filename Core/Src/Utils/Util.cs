using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Utils
{
    public static class Util
    {
        public static IEnumerable<T> Shuffle<T>(IEnumerable<T> source)
        {
            var rand = new Random();
            var sourceWithNumbers = source.Select(x => new {Value = x, Position = rand.Next()}).OrderBy(x=>x.Position);
            var result = sourceWithNumbers.Select(x => x.Value);

            return result;
        }

        public static IEnumerable<T> Shuffle<T>(params IEnumerable<T>[] source)
        {
            var rand = new Random();
            var aggregateSource = source.SelectMany(x =>
            {
                var enumerable = x as T[] ?? x.ToArray();
                return enumerable;
            });

            var sourceWithNumbers = aggregateSource.Select(x => new { Value = x, Position = rand.Next() }).OrderBy(x => x.Position);
            var result = sourceWithNumbers.Select(x => x.Value);

            return result;
        }
    }
}
