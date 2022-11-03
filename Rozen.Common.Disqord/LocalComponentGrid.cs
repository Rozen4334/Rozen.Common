using Disqord;

namespace Rozen.Common.Disqord
{
    /// <summary>
    ///     Represents a builder that creates a range of components for messages.
    /// </summary>
    public class LocalComponentGrid
    {
        private readonly LocalComponent?[,] _components;

        /// <summary>
        ///     The last X position used to add a component.
        /// </summary>
        public int LastX { get; private set; }

        /// <summary>
        ///     The last Y position used to add a component.
        /// </summary>
        public int LastY { get; private set; }

        /// <summary>
        ///     If the builder is creating modal text inputs or regular components.
        /// </summary>
        public bool IsTextBuilder { get; private set; }

        public LocalComponentGrid()
        {
            _components = new LocalComponent?[5, 5];
            LastX = 0;
            LastY = 0;
            IsTextBuilder = false;
        }

        /// <summary>
        ///     Includes a <see cref="LocalButtonComponent"/> into the builder.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public LocalComponentGrid WithButton(LocalButtonComponent button, int x, int y)
        {
            ValidateCoordinate(x, y);

            if (IsTextBuilder)
                throw new ArgumentException("Cannot accept any component type but text input when text input has already been added.", nameof(button));

            _components[x, y] = button;

            LastX = x;
            LastY = y;
            return this;
        }

        /// <summary>
        ///     Includes a <see cref="LocalButtonComponent"/> into the builder.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="cid"></param>
        /// <param name="style"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LocalComponentGrid WithButton(string label, string cid, LocalButtonComponentStyle style, bool isDisabled, int x, int y)
            => WithButton(new()
            {
                Label = label,
                CustomId = cid,
                Style = style,
                IsDisabled = isDisabled,
            }, x, y);

        /// <summary>
        ///     Includes a <see cref="LocalButtonComponent"/> into the builder.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="cid"></param>
        /// <param name="style"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LocalComponentGrid WithButton(string label, string cid, LocalButtonComponentStyle style, int x, int y)
            => WithButton(new()
            {
                Label = label,
                CustomId = cid,
                Style = style,
            }, x, y);

        /// <summary>
        ///     Includes a <see cref="LocalLinkButtonComponent"/> into the builder.
        /// </summary>
        /// <param name="link"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LocalComponentGrid WithLink(LocalLinkButtonComponent link, int x, int y)
        {
            ValidateCoordinate(x, y);

            if (IsTextBuilder)
                throw new ArgumentException("Cannot accept any component type but text input when text input has already been added.", nameof(link));

            _components[x, y] = link;

            LastX = x;
            LastY = y;

            return this;
        }

        /// <summary>
        ///     Includes a <see cref="LocalLinkButtonComponent"/> into the builder.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="url"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LocalComponentGrid WithLink(string label, string url, int x, int y)
            => WithLink(new LocalLinkButtonComponent()
            {
                Label = label,
                Url = url,
            }, x, y);

        /// <summary>
        ///     Includes a <see cref="LocalLinkButtonComponent"/> into the builder.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="url"></param>
        /// <param name="isDisabled"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LocalComponentGrid WithLink(string label, string url, bool isDisabled, int x, int y)
            => WithLink(new LocalLinkButtonComponent()
            {
                Label = label,
                Url = url,
                IsDisabled = isDisabled
            }, x, y);

        /// <summary>
        ///     Includes a <see cref="LocalLinkButtonComponent"/> into the builder.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="url"></param>
        /// <param name="emoji"></param>
        /// <param name="isDisabled"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LocalComponentGrid WithLink(string label, string url, LocalEmoji emoji, bool isDisabled, int x, int y)
            => WithLink(new LocalLinkButtonComponent()
            {
                Label = label,
                Url = url,
                Emoji = emoji,
                IsDisabled = isDisabled
            }, x, y);

        /// <summary>
        ///     Includes a <see cref="LocalSelectionComponent"/> into the builder.
        /// </summary>
        /// <param name="selection"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public LocalComponentGrid WithSelectMenu(LocalSelectionComponent selection, int y)
        {
            if (y is > 4)
                throw new ArgumentOutOfRangeException(nameof(y));

            if (IsTextBuilder)
                throw new ArgumentException("Cannot accept any component type but text input when text input has already been added.", nameof(selection));

            _components[0, y] = selection;

            for (int i = 1; i < 5; i++)
                _components[i, y] = null;

            LastX = 0;
            LastY = y;

            return this;
        }

        /// <summary>
        ///     Includes a <see cref="LocalTextInputComponent"/> into the builder.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public LocalComponentGrid WithTextInput(LocalTextInputComponent text, int y)
        {
            if (y is > 4)
                throw new ArgumentOutOfRangeException(nameof(y));

            if (!IsTextBuilder)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (_components[0, i] is not null and not LocalTextInputComponent)
                        throw new ArgumentException("Cannot accept text inputs on ranges with any other component type.", nameof(text));
                }
                IsTextBuilder = true;
            }

            _components[0, y] = text;

            for (int i = 1; i < 5; i++)
                _components[i, y] = null;

            LastX = 0;
            LastY = y;

            return this;
        }

        /// <summary>
        ///     Includes a <see cref="LocalTextInputComponent"/> into the builder.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="cid"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="style"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LocalComponentGrid WithTextInput(string label, string cid, int minLength, int maxLength, TextInputComponentStyle style, int y)
            => WithTextInput(new()
            {
                Style = style,
                Label = label,
                CustomId = cid,
                MinimumInputLength = minLength,
                MaximumInputLength = maxLength,
            }, y);

        /// <summary>
        ///     Includes a <see cref="LocalTextInputComponent"/> into the builder.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="placeholder"></param>
        /// <param name="cid"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="style"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LocalComponentGrid WithTextInput(string label, string placeholder, string cid, int minLength, int maxLength, TextInputComponentStyle style, int y)
            => WithTextInput(new()
            {
                Placeholder = placeholder,
                Style = style,
                Label = label,
                CustomId = cid,
                MinimumInputLength = minLength,
                MaximumInputLength = maxLength,
            }, y);

        /// <summary>
        ///     Includes a <see cref="LocalTextInputComponent"/> into the builder.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="placeholder"></param>
        /// <param name="cid"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="isRequired"></param>
        /// <param name="style"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LocalComponentGrid WithTextInput(string label, string placeholder, string cid, int minLength, int maxLength, bool isRequired, TextInputComponentStyle style, int y)
            => WithTextInput(new()
            {
                Placeholder = placeholder,
                Style = style,
                Label = label,
                CustomId = cid,
                MinimumInputLength = minLength,
                MaximumInputLength = maxLength,
                IsRequired = isRequired,
            }, y);

        /// <summary>
        ///     Builds the current builder into a <see cref="IEnumerable{T}"/> of <see cref="LocalRowComponent"/>'s.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LocalRowComponent> Convert()
        {
            var grid = new List<LocalRowComponent>();
            for (int y = 0; y < 5; y++)
            {
                var row = new LocalRowComponent();
                for (int x = 0; x < 5; x++)
                {
                    var component = _components[x, y];
                    if (component is not null)
                        row.AddComponent(component);
                }

                if (row.Components.HasValue && row.Components.Value.Count > 0)
                    yield return row;
            }
        }

        public static explicit operator LocalRowComponent[](LocalComponentGrid? grid)
            => grid?.Convert().ToArray() ?? Array.Empty<LocalRowComponent>();

        private void ValidateCoordinate(int x, int y)
        {
            if (x is > 4)
                throw new ArgumentOutOfRangeException(nameof(x));

            if (y is > 4)
                throw new ArgumentOutOfRangeException(nameof(y));

            for (int i = 0; i < x; i++)
            {
                if (_components[i, y] is null)
                    throw new ArgumentOutOfRangeException(nameof(x), "Cannot add a button at a floating location.");
            }

            if (_components[0, y] is LocalSelectionComponent && x is not 0)
                throw new ArgumentOutOfRangeException(nameof(x), "You cannot add a button in a selection row.");
        }
    }
}
