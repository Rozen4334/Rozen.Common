using Disqord;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Common.Disqord
{
    /// <summary>
    ///     Represents a message jump url.
    /// </summary>
    public readonly struct JumpUrl
    {
        private static readonly Regex _regex = new(
            @"(?<Prelink>\S+\s+\S*)?(?<OpenBrace><)?https?:\/\/(?:(?:ptb|canary)\.)?discord(?:app)?\.com\/channels\/(?<Location>\d+|@me)\/(?<ChannelId>\d+)\/(?<MessageId>\d+)\/?(?<CloseBrace>>)?(?<Postlink>\S*\s+\S+)?",
            RegexOptions.Compiled);

        /// <summary>
        ///     The url of this message reference.
        /// </summary>
        public string Url { get; }

        /// <summary>
        ///     The source of this url.
        /// </summary>
        public JumpUrlType Type { get; }

        /// <summary>
        ///     The Id of the guild this url jumps to.
        /// </summary>
        /// <remarks>
        ///     will be 0 if the <see cref="Type"/> is <see cref="JumpUrlType.DirectMessage"/>.
        /// </remarks>
        public ulong GuildId { get; }

        /// <summary>
        ///     The Id of the channel this url jumps to.
        /// </summary>
        public ulong ChannelId { get; }

        /// <summary>
        ///     The Id of the message this url jumps to.
        /// </summary>
        public ulong MessageId { get; }

        public JumpUrl(string url, JumpUrlType type, ulong guildId, ulong channelId, ulong messageId)
        {
            Url = url;
            Type = type;
            ChannelId = channelId;
            MessageId = messageId;
            GuildId = guildId;
        }

        /// <summary>
        ///     Attempts to create a <see cref="JumpUrl"/> from the provided <paramref name="messageUrl"/>.
        /// </summary>
        /// <param name="messageUrl">The url to attempt to parse.</param>
        /// <param name="value">The returned value.</param>
        /// <returns><see langword="true"/> if succesfully parsed. <see langword="false"/> if not.</returns>
        public static bool TryParse(string messageUrl, out JumpUrl value)
        {
            value = new();

            if (!TryGetUrlData(messageUrl, out var data))
                return false;

            var type = data[0] is 0ul
                ? JumpUrlType.DirectMessage
                : JumpUrlType.GuildMessage;

            value = new(messageUrl, type, data[0], data[1], data[2]);

            return true;
        }

        /// <summary>
        ///     Creates a <see cref="JumpUrl"/> from the provided <paramref name="messageUrl"/>.
        /// </summary>
        /// <param name="messageUrl">The url to parse.</param>
        /// <returns>A new instance of <see cref="JumpUrl"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="messageUrl"/> is an invalid jump url.</exception>
        public static JumpUrl Parse(string messageUrl)
        {
            if (!TryGetUrlData(messageUrl, out var data))
                throw new ArgumentException("Provided argument is not a valid message url.", nameof(messageUrl));

            var type = data[0] is 0ul
                ? JumpUrlType.DirectMessage
                : JumpUrlType.GuildMessage;

            return new(messageUrl, type, data[0], data[1], data[2]);
        }

        /// <summary>
        ///     Create a jump URL from the provided message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isDm"></param>
        /// <param name="guildId"></param>
        /// <returns></returns>
        public static JumpUrl Create(IMessage message, bool isDm, ulong? guildId = null)
            => Create(message.Id, message.ChannelId, isDm, guildId);

        /// <summary>
        ///     Create a jump URL from the provided message id.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isDm"></param>
        /// <param name="guildId"></param>
        /// <returns></returns>
        public static JumpUrl Create(ulong messageId, ulong channelId, bool isDm, ulong? guildId = null)
        {
            var url = $"https://discord.com/channels/{(isDm ? "@me" : $"{guildId}")}/{channelId}/{messageId}";
            var type = isDm ? JumpUrlType.DirectMessage : JumpUrlType.GuildMessage;

            return new(url, type, guildId ?? 0ul, channelId, messageId);
        }

        public override string ToString()
            => Url;

        public override int GetHashCode()
            => Url.GetHashCode();

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is JumpUrl url && url.Url == Url)
                return true;
            return false;
        }

        public static bool operator ==(JumpUrl left, JumpUrl right)
            => left.Equals(right);

        public static bool operator !=(JumpUrl left, JumpUrl right)
            => !(left == right);

        private static bool TryGetUrlData(string messageUrl, out List<ulong> data)
        {
            static bool IsJumpUrl(string messageUrl)
            {
                return _regex.IsMatch(messageUrl);
            }

            data = new();

            if (!IsJumpUrl(messageUrl))
                return false;

            var matches = _regex.Matches(messageUrl);

            foreach (Match match in matches)
                foreach (Group group in match.Groups)
                    switch (group.Name)
                    {
                        case "Location":
                            if (ulong.TryParse(group.Value, out var ul))
                                data.Add(ul); // guild id
                            else data.Add(0); // dm
                            break;
                        case "ChannelId":
                            var channelId = ulong.Parse(group.Value);
                            data.Add(channelId);
                            break;
                        case "MessageId":
                            var messageId = ulong.Parse(group.Value);
                            data.Add(messageId);
                            break;
                    }
            return true;
        }
    }
}
