using Disqord;

namespace Rozen.Common.Disqord
{
    public static class ComponentExtensions
    {
        public static LocalSelectionComponent AddOption(
            this LocalSelectionComponent component, string label, string value, string? description = null, bool isDefault = false, LocalEmoji? emoji = null)
        {
            component.AddOption(new()
            {
                Description = description ?? new Qommon.Optional<string>(),
                Value = value,
                Label = label,
                IsDefault = isDefault,
                Emoji = emoji ?? new Qommon.Optional<LocalEmoji>()
            });
            return component;
        }
    }
}
