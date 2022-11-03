using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Data
{
    /// <summary>
    ///     Represents the database manager.
    /// </summary>
    public sealed class DatabaseManager
    {
        private static MongoClient? _client;
        private static MongoDatabaseBase? _database;

        /// <summary>
        ///     Checks if the database is connected or not.
        /// </summary>
        public static bool IsConnected
        {
            get
            {
                try
                {
                    _client?.ListDatabaseNames();
                    return true;
                }
                catch (MongoException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///     Creates a new <see cref="DatabaseManager"/> with provided configuration action.
        /// </summary>
        /// <param name="action"></param>
        public DatabaseManager(Action<DatabaseConfiguration> action)
        {
            var config = new DatabaseConfiguration();
            action(config);

            var url = new MongoUrl(config.ConnectionString);

            _client = new MongoClient(url);

            Connect(config);

            config.LogAction?.Invoke("Database connected succesfully.");
        }

        /// <summary>
        ///     Creates a new <see cref="DatabaseManager"/> based on injection parameters.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="config"></param>
        public DatabaseManager(MongoClient client, DatabaseConfiguration config)
        {
            _client = client;

            Connect(config);

            config.LogAction?.Invoke("Database connected succesfully.");
        }

        /// <summary>
        ///     Runs mongo commands as if in shell.
        /// </summary>
        /// <param name="command">The command to run.</param>
        /// <returns>The resulting string.</returns>
        public static string RunCommand(string command)
        {
            try
            {
                var result = _database?.RunCommand<BsonDocument>(BsonDocument.Parse(command));
                return result.ToJson();
            }
            catch (Exception ex) when (ex is FormatException or MongoCommandException)
            {
                throw;
            }
        }

        /// <summary>
        ///     Extracts a collection and its current data from the database collection.
        /// </summary>
        /// <typeparam name="TEntity">The entity to base this collection on.</typeparam>
        /// <param name="collection">The name of the collection.</param>
        /// <returns>An instance of <see cref="MongoCollectionBase{TDocument}"/></returns>
        /// <exception cref="NullReferenceException">Thrown if no collection was found.</exception>
        public static MongoCollectionBase<TEntity> GetCollection<TEntity>(string collection) where TEntity : IEntity
            => _database?.GetCollection<TEntity>(collection) as MongoCollectionBase<TEntity>
            ?? throw new NullReferenceException();

        private static void Connect(DatabaseConfiguration config)
        {
            if (string.IsNullOrEmpty(config.DatabaseName))
                throw new ArgumentNullException(nameof(config.DatabaseName));

            if (_client is not null)
            {
                _database = _client.GetDatabase(config.DatabaseName) as MongoDatabaseBase;

                if (!IsConnected)
                    throw new InvalidOperationException("Database could not connect.");
            }
            else
                throw new InvalidOperationException("Client cannot resolve and was found null.");
        }
    }
}
