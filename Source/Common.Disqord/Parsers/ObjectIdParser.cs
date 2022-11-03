using Disqord.Bot.Commands;
using MongoDB.Bson;
using Qmmands;

namespace Common.Disqord
{
    public class ObjectIdParser : DiscordTypeParser<ObjectId>
    {
        public override ValueTask<ITypeParserResult<ObjectId>> ParseAsync(IDiscordCommandContext context, IParameter parameter, ReadOnlyMemory<char> value)
        {
            if (ObjectId.TryParse(value.Span.ToString(), out var id))
                return Success(id);

            return Failure("Invalid Id.");
        }
    }
}
