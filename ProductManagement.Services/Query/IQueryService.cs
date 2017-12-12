namespace ProductManagement.Services.Query
{
    using ProductManagement.DataRepresentation;
    using System.Linq;

    /// <summary>
    /// The service is used to query entites from the database.
    /// </summary>
    public interface IQueryService
    {
        /// <summary>
        /// Returns a query to the database as no tracking.
        /// </summary>
        /// <typeparam name="T">The type of the entity to query. It needs to implement
        /// <see cref="IIdentifiable"/> to uniquely identify it with its id.</typeparam>
        /// <returns>Returns the query.</returns>
        IQueryable<T> Query<T>() where T : class, IIdentifiable;
    }
}
