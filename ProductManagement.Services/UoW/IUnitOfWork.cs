namespace ProductManagement.Services.UoW
{
    using ProductManagement.Services.Repository;
    using System;

    /// <summary>
    /// The unit of work is a central part of this application. It is the logical
    /// representation of a transaction on the database. All the changes can be made
    /// by the client by creating repositories. The changes will only be saved to the 
    /// database when calling the <see cref="Save"/> method. The save method will also
    /// execute the business rules associated with the changed entity.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Creates an repository for the given entity type.
        /// </summary>
        /// <typeparam name="T">The entity type to get the repository for.</typeparam>
        /// <returns>Returns the repository of the given entity.</returns>
        IRepository<T> CreateEntityRepository<T>();

        /// <summary>
        /// Saves all changes to the database and executes all business rules. It will
        /// first execute all pre save methods on the changed entities and after saving
        /// it (successfully) it'll execute all post save methods.
        /// </summary>
        void Save();
    }
}
