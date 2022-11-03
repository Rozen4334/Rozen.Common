namespace Common
{
    /// <summary>
    ///     Thrown when a key has not been found due to it being abandoned in the <see cref="Barriot.References.Caching.ExpiringDictionary{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class KeyAbandonedException<TKey> : Exception where TKey : notnull
    {
        public KeyAbandonedException(TKey id)
            : base($"{nameof(TKey)} with value: {id} holds no value;")
        {

        }
    }
}
