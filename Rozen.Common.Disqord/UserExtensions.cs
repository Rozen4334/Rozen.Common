using Disqord;

namespace Rozen.Common.Disqord
{
    public static class UserExtensions
    {
        public static string GetDisplayAvatarUrl(this IMember member, CdnAssetFormat format = CdnAssetFormat.None, int? size = null)
        {
            if (member.GuildAvatarHash is not null)
                return member.GetGuildAvatarUrl(format, size);
            return member.GetAvatarUrl(format, size);
        }
    }
}
