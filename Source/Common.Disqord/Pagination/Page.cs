using Disqord;

namespace Common.Disqord
{
    public readonly struct Page
    {
        /// <summary>
        ///     The embed produced by this page.
        /// </summary>
        public LocalEmbed Embed { get; }

        /// <summary>
        ///     The components produced by this page.
        /// </summary>
        public LocalComponentGrid Components { get; }

        internal Page(LocalEmbed embed, LocalComponentGrid component)
        {
            Embed = embed;
            Components = component;
        }
    }
}
