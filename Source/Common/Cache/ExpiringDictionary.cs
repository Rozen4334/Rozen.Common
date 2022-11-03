namespace Common
{
    /// <summary>
    ///     A dictionary that manages entities in memory and automatically clears entries if they do not meet specified fetch intervals.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ExpiringDictionary<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, (TValue, DateTime)> _dictionary;
        private readonly System.Timers.Timer _timer;
        private readonly ExpiringDictionarySettings _settings;

        /// <summary>
        ///     Creates a new <see cref="ExpiringDictionary{TKey, TValue}"/> with base settings.
        /// </summary>
        public ExpiringDictionary() : this(new())
        {

        }

        /// <summary>
        ///     Creates a new <see cref="ExpiringDictionary{TKey, TValue}"/> with specified settings.
        /// </summary>
        /// <param name="settings"></param>
        public ExpiringDictionary(ExpiringDictionarySettings settings)
        {
            _dictionary = new();
            _settings = settings;
            _timer = new System.Timers.Timer(_settings.TimerFrequencySpan.TotalMilliseconds)
            {
                AutoReset = true,
                Enabled = true,
            };
            _timer.Elapsed += (_, arg) => Tick();
            _timer.Start();
        }

        private void Tick()
        {
            foreach (var kvp in _dictionary)
                if (kvp.Value.Item2 <= DateTime.UtcNow)
                    _dictionary.Remove(kvp.Key);
        }

        /// <summary>
        ///     Adds a new entry to the <see cref="ExpiringDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="id">The key to add.</param>
        /// <param name="value">The value to pair to the input key.</param>
        /// <param name="removal">When the entry should be automatically removed.</param>
        public void Add(TKey id, TValue value, TimeSpan? removal = null)
            => _dictionary[id] = (value, DateTime.UtcNow + (removal ?? _settings.AutoRemovalSpan));

        /// <summary>
        ///     Removes an entry from the <see cref="ExpiringDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="id"></param>
        public void Remove(TKey id)
            => _dictionary.Remove(id);

        /// <summary>
        ///     Attempts to get the value tied to the input key.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keepReference"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey id, bool keepReference, out TValue? value)
        {
            if (_dictionary.TryGetValue(id, out var tuple))
            {
                value = tuple.Item1;
                if (!keepReference)
                    Remove(id);
                else
                {
                    if (_settings.FetchType is ExpirationType.Rolling)
                        _dictionary[id] = (value!, (DateTime.UtcNow + _settings.AutoRemovalSpan));
                }
                return true;
            }
            value = default(TValue);
            return false;
        }

        /// <summary>
        ///     Gets the value tied to the input key.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keepReference"></param>
        /// <returns></returns>
        /// <exception cref="KeyAbandonedException{TKey}"></exception>
        public TValue? GetValue(TKey id, bool keepReference)
        {
            if (TryGetValue(id, keepReference, out var value))
                return value;
            throw new KeyAbandonedException<TKey>(id);
        }
    }
}
