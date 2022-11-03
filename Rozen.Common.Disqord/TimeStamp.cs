namespace Rozen.Common.Disqord
{
    /// <summary>
    ///     Represents a Discord timestamp.
    /// </summary>
    public readonly struct TimeStamp
    {
        /// <summary>
        ///     The global format for timestamp tags.
        /// </summary>
        public static string Format { get; } = "<t:{0}:{1}>";

        /// <summary>
        ///     The time in epoch second display.
        /// </summary>
        public long Time { get; }

        private TimeStamp(long time)
            => Time = time;

        /// <summary>
        ///     Creates a new <see cref="TimeStamp"/> from provided <paramref name="time"/>.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static TimeStamp Create(DateTimeOffset time)
            => new(time.ToUnixTimeSeconds());

        /// <summary>
        ///     Creates a new <see cref="TimeStamp"/> from provided <paramref name="time"/>.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static TimeStamp Create(DateTime time)
            => Create((DateTimeOffset)time);

        /// <summary>
        ///     Defaults the returned timestamp to <see cref="TimeStampType.ShortDateTime"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ToString(TimeStampType.ShortDateTime);

        /// <summary>
        ///     Returns a timestamp string from the provided <paramref name="type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string ToString(TimeStampType type)
            => string.Format(Format, Time, (char)type);
    }
}
