using Disqord;
using Disqord.Bot.Commands;
using Disqord.Gateway;
using Disqord.Rest;
using Qmmands;

namespace Common.Disqord
{
    public class MessageParser : DiscordGuildTypeParser<IMessage>
    {
        public override async ValueTask<ITypeParserResult<IMessage>> ParseAsync(IDiscordGuildCommandContext context, IParameter parameter, ReadOnlyMemory<char> value)
        {
            if (!JumpUrl.TryParse(value.Span.ToString(), out var url))
            {
                if (url.GuildId != context.GuildId)
                    return Failure("You're not allowed to use message URL's outside of the current server.");

                IMessage? message = context.Bot.GetMessage(url.ChannelId, url.MessageId);

                if (message is null)
                {
                    message = await context.Bot.FetchMessageAsync(url.ChannelId, url.MessageId);

                    if (message is null)
                        return Failure("Unable to fetch message.");
                }

                return Success(new(message));
            }
            return Failure("Provided invalid message URL. Copy a message URL by right clicking the message > Copy message link.");
        }
    }
}
