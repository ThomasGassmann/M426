namespace ProductManagement.Services.Query
{
    using Microsoft.EntityFrameworkCore;
    using ProductManagement.DataRepresentation;
    using ProductManagement.Services.UoW;
    using System.Linq;

    public class QueryService : IQueryService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        private IUnitOfWork currentUnitOfWork;

        public QueryService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.CreateNewUnitOfWork();
        }

        public IQueryable<T> Query<T>() where T : class, IIdentifiable
        {
            var repository = this.currentUnitOfWork.CreateEntityRepository<T>();
            var query = repository.Query();
            return query.AsNoTracking();
        }

        private void CreateNewUnitOfWork()
        {
            this.currentUnitOfWork = this.unitOfWorkFactory.CreateUnitOfWork();
        }
    }
}
