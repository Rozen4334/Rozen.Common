namespace Common
{
    /// <summary>
    ///     The settings that control how the <see cref="ExpiringDictionary{TKey, TValue}"/> will operate.
    /// </summary>
    public class ExpiringDictionarySettings
    {
        /// <summary>
        ///     Gets or sets the timespan that controls when an entry is removed automatically to prevent memory leakage. 
        ///     Default is 15 minutes.
        /// </summary>
        public TimeSpan AutoRemovalSpan { get; set; } = TimeSpan.FromMinutes(15);

        /// <summary>
        ///     Gets or sets the <see cref="TimeSpan"/> that controls when all entries are iterated to see if the <see cref="AutoRemovalSpan"/> is addressed.
        ///     Default is 5 minutes.
        /// </summary>
        /// <remarks>
        ///     The smaller this value is, the more accurate the <see cref="ExpiringDictionary{TKey, TValue}"/> will be with its entries. 
        ///     The bigger the value is, the less resource intensive the removal operation will be.
        /// </remarks>
        public TimeSpan TimerFrequencySpan { get; set; } = TimeSpan.FromMinutes(5);

        /// <summary>
        ///     Gets or sets if the automated prune logic should be <see cref="ExpirationType.Rolling"/> or <see cref="ExpirationType.Absolute"/>.
        /// </summary>
        public ExpirationType FetchType { get; set; } = ExpirationType.Absolute;
    }
}
