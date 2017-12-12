namespace ProductManagement.Services.Query
{
    using Microsoft.EntityFrameworkCore;
    using ProductManagement.DataRepresentation;
    using ProductManagement.Services.UoW;
    using System.Linq;

    /// <summary>
    /// The implementation of the service used to query entites from the database.
    /// </summary>
    public class QueryService : IQueryService
    {
        /// <summary>
        /// The unit of work factory to create unit of works to conduct transactions
        /// on the database.
        /// </summary>
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        /// <summary>
        /// The current unit of work created by the unit of work factory.
        /// </summary>
        private IUnitOfWork currentUnitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryService"/> class.
        /// Creates a <see cref="IUnitOfWork"/> to query data from.
        /// </summary>
        /// <param name="unitOfWorkFactory">The <see cref="IUnitOfWorkFactory"/> dependency.</param>
        public QueryService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.CreateNewUnitOfWork();
        }

        /// <inheritdoc />
        public IQueryable<T> Query<T>() where T : class, IIdentifiable
        {
            var repository = this.currentUnitOfWork.CreateEntityRepository<T>();
            var query = repository.Query();
            return query.AsNoTracking();
        }

        /// <summary>
        /// Sets a new unit of work to use for filtering. It will be created using the
        /// unit of work factory.
        /// </summary>
        private void CreateNewUnitOfWork()
        {
            this.currentUnitOfWork = this.unitOfWorkFactory.CreateUnitOfWork();
        }
    }
}
