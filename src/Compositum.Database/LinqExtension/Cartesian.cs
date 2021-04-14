using System.Collections.Generic;
using System.Linq;

namespace Compositum.LinqExtension
{
    public static class Cartesian
    {
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(
            IEnumerable<IEnumerable<T>> inputs)
        {
            return inputs.Aggregate(
                EnumerableFrom(Enumerable.Empty<T>()),
                (soFar, input) =>
                    from prevProductItem in soFar
                    from item in input
                    select prevProductItem.Append(item));
        }
        private static IEnumerable<T> EnumerableFrom<T>(T item)
        {
            return new T[] { item };
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> that, T item)
        {
            IEnumerable<T> itemAsSequence = new T[] { item };
            return that.Concat(itemAsSequence);
        }
    }
}