namespace Rozen.Common
{
    /// <summary>
    ///     Manages creating and fetching <see cref="Pointer{T}"/>'s from memory with <see cref="Guid"/>'s.
    /// </summary>
    public static class GuidReference
    {
        private static readonly ExpiringDictionary<Guid, object> _dict = new();
        private static readonly object _lock = new();

        /// <summary>
        ///     Creates a new <see cref="Pointer{T}"/> with the value of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns>The <see cref="Guid"/> the value of <typeparamref name="T"/> is referenced with.</returns>
        public static Guid Create(object value)
        {
            lock (_lock)
            {
                var guid = Guid.NewGuid();
                _dict.Add(guid, value);
                return guid;
            }
        }

        /// <summary>
        ///     Attempts to parse an <see cref="IPointer"/> with the value of <typeparamref name="T"/> matching the provided <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns>True if the value was succesfully retrieved.</returns>
        public static bool TryParse(Guid id, out object? value, bool keepReference = false)
        {
            lock (_lock)
            {
                value = null;
                if (_dict.TryGetValue(id, keepReference, out var obj))
                {
                    value = obj;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        ///     Parses an <see cref="IPointer"/> with the value of <typeparamref name="T"/> matching the provided <see cref="Guid"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="keepReference"></param>
        /// <returns></returns>
        public static object? Parse(Guid id, bool keepReference)
        {
            lock (_lock)
            {
                return _dict.GetValue(id, keepReference)!;
            }
        }
    }
}
