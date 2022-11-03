using Disqord.Bot.Commands;
using Qmmands;

namespace Common.Disqord
{
    public sealed class PointerParser<T> : DiscordTypeParser<T> where T : notnull
    {
        public override ValueTask<ITypeParserResult<T>> ParseAsync(IDiscordCommandContext context, IParameter parameter, ReadOnlyMemory<char> value)
        {
            if (Guid.TryParse(value.Span, out var guid))
            {
                if (GuidReference.TryParse(guid, out var ptr) && ptr is T type)
                    return Success(type);

                return Failure("Request timed out!");
            }
            return Failure("Invalid reference ID! Please report this to the developer.");
        }
    }
}
