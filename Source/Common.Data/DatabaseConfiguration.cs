namespace Common.Data
{
    /// <summary>
    ///     Represents the configuration for a mongo database.
    /// </summary>
    public class DatabaseConfiguration
    {
        /// <summary>
        ///     The connection string for the mongo instance.
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        ///     The database to use.
        /// </summary>
        public string DatabaseName { get; set; } = string.Empty;

        /// <summary>
        ///     An action that can be used to print logs to the database resolver.
        /// </summary>
        public Action<string>? LogAction { get; set; }
    }
}
