namespace Rozen.Common
{
    public static class LinqExtensions
    {
        /// <summary>
        ///     Creates a <see cref="List{T}"/> from a provided <see cref="IAsyncEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items,
            CancellationToken cancellationToken = default)
        {
            var results = new List<T>();
            await foreach (var item in items.WithCancellation(cancellationToken).ConfigureAwait(false))
                results.Add(item);
            return results;
        }

        /// <summary>
        ///     Extracts a range from a 2 dimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="yDimension"></param>
        /// <returns></returns>
        public static T[,] Extract<T>(this T[,] array, int yDimension)
        {
            T[,] result = new T[array.GetLength(0), array.GetLength(1) - 1];

            for (int i = 0, j = 0; i < array.GetLength(0); i++)
            {
                for (int k = 0, u = 0; k < array.GetLength(1); k++)
                {
                    if (k == yDimension)
                        continue;

                    result[j, u] = array[i, k];
                    u++;
                }
                j++;
            }

            return result;
        }
    }
}
