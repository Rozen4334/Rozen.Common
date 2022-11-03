using Disqord.Bot.Commands;
using Qmmands;

namespace Common.Disqord
{
    public sealed class TimeSpanParser : DiscordTypeParser<TimeSpan>
    {
        public override ValueTask<ITypeParserResult<TimeSpan>> ParseAsync(IDiscordCommandContext context, IParameter parameter, ReadOnlyMemory<char> value)
        {
            var span = value.Span.ToString().GetTimeSpan();

            if (span == TimeSpan.Zero)
                return Failure("Provided an invalid time.");

            return Success(span);
        }
    }
}
