using MongoDB.Driver;

namespace Common.Data
{
    /// <summary>
    ///     Represents an entity that can be called across multiple instances.
    /// </summary>
    /// <typeparam name="TEntity">The entity that can be accessed in multiplication</typeparam>
    public interface IConcurrentlyAccessible<TEntity> where TEntity : IEntity
    {
        /// <summary>
        ///     The state of this entity.
        /// </summary>
        public EntityState State { get; set; }

        /// <summary>
        ///     Modifies the entity.
        /// </summary>
        /// <param name="update">The definition of the field to edit, created through <see cref="Builders{TDocument}.Update"/>.</param>
        /// <returns></returns>
        public Task<bool> ModifyAsync(UpdateDefinition<TEntity> update);
    }
}
