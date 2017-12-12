namespace ProductManagement.Services.Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Defines a repository to work with data on the database using CRUD (create,
    /// update and delete) operations.
    /// </summary>
    /// <typeparam name="T">The entity type to manage.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Queries the entities (tracked) from the database.
        /// </summary>
        /// <returns>Returns a query of the entity.</returns>
        IQueryable<T> Query();

        /// <summary>
        /// Deletes entites from the database where the given predicate matches.
        /// </summary>
        /// <param name="func">The predicate as an expression tree.</param>
        void Delete(Expression<Func<T, bool>> func);

        /// <summary>
        /// Deletes the entity with the given id. If the entity with the given id
        /// does not exist, nothing will be deleted.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        void Delete(long id);

        /// <summary>
        /// Creates an entity on the database.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        void Create(T entity);

        /// <summary>
        /// Finds an entity by its id.
        /// </summary>
        /// <param name="id">The id of the enttiy to return.</param>
        /// <returns>The entity found on the database. Default, if the entity does not exist.</returns>
        T FindById(long id);

        /// <summary>
        /// Updates the entity on the database.
        /// </summary>
        /// <param name="entity">The entity to udpate. It must be a tracked entity on the <see cref="IUnitOfWork"/>.</param>
        void Update(T entity);
    }
}
