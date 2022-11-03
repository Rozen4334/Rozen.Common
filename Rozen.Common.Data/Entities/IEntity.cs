using MongoDB.Bson;

namespace Rozen.Common.Data
{
    /// <summary>
    ///     A database entity.
    /// </summary>
    public interface IEntity : IDisposable
    {
        /// <summary>
        ///     The original ID of this entity.
        /// </summary>
        ObjectId ObjectId { get; set; }

        /// <summary>
        ///     Deletes this entity.
        /// </summary>
        /// <returns><see cref="true"/> if successful. <see cref="false"/> if failed.</returns>
        public Task<bool> DeleteAsync();
    }
}
