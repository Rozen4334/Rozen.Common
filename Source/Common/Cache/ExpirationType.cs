namespace Common
{
    /// <summary>
    ///     Tells the <see cref="ExpiringDictionary{TKey, TValue}"/> how it's <see cref="KeyValuePair{TKey, TValue}"/>'s should be preserved if the references are kept after fetch.
    /// </summary>
    public enum ExpirationType
    {
        /// <summary>
        ///     The entry will keep incrementing the remove time after each fetch, if the reference is kept. 
        ///     The value it will be incremented by will be <see cref="ExpiringDictionarySettings.AutoRemovalSpan"/>.
        /// </summary>
        Rolling,

        /// <summary>
        ///     The entry will always be removed at specified initial creation time, even if its fetched and the reference is kept.
        /// </summary>
        Absolute
    }
}
