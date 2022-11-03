using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Common
{
    /// <summary>
    ///     Represents a configuration implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class JsonReader<T> 
        where T : IJsonObject, new()
    {
        private static string? _basePath;

        /// <summary>
        ///     The default serialization options.
        /// </summary>
        public static readonly JsonSerializerOptions DefaultOptions = new()
        {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
            WriteIndented = true
        };

        /// <summary>
        ///     If the settings were read from file or written to it.
        /// </summary>
        public static bool Loaded { get; private set; } = false;

        private static T _value = new();
        /// <summary>
        ///     The settings implementation itself.
        /// </summary>
        public static T Value
        {
            get
            {
                if (!Loaded)
                    throw new NotSupportedException("This operation cannot be completed without calling 'Load()' on the generic target.");

                return _value;
            }
            private set
                => _value = value;
        }

        /// <summary>
        ///     Reloads the active configuration.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public static T Load(string path)
        {
            if (_basePath is null)
                _basePath = path + ".json";

            if (!File.Exists(_basePath))
            {
                var obj = new T();

                var content = JsonSerializer.Serialize(obj, DefaultOptions);

                File.WriteAllText(_basePath, content);

                Value = obj;
            }
            else
            {
                var content = File.ReadAllText(_basePath);

                var obj = JsonSerializer.Deserialize<T>(content, DefaultOptions);

                if (obj is null)
                    throw new JsonException($"Encountered invalid JSON in file: {_basePath}.");

                Value = obj;
            }

            Loaded = true;

            return Value;
        }
    }
}
