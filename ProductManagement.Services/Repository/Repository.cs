namespace ProductManagement.Services.Repository
{
    using ProductManagement.DataAccess.Context;
    using ProductManagement.DataRepresentation;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Contains the default implementation of the repository to manage data on the database.
    /// </summary>
    /// <typeparam name="T">The type to manage the data from. It needs to implement <see cref="IIdentifiable"/>.</typeparam>
    public class Repository<T> : IRepository<T> where T : class, IIdentifiable
    {
        /// <summary>
        /// Contains the database context to make changes to the database.
        /// </summary>
        private readonly ProductManagementDbContext productManagementDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="productManagementDbContext">The database context dependency.</param>
        public Repository(ProductManagementDbContext productManagementDbContext) =>
            this.productManagementDbContext = productManagementDbContext;

        /// <inheritdoc />
        public void Create(T entity) =>
            this.productManagementDbContext.Add(entity);

        /// <inheritdoc />
        public void Delete(Expression<Func<T, bool>> func)
        {
            var results = this.Query().Where(func).ToArray();
            foreach (var result in results)
            {
                this.productManagementDbContext.Remove(result);
            }
        }

        /// <inheritdoc />
        public void Delete(long id)
        {
            var entity = this.FindById(id);
            if (entity != null)
            {
                this.productManagementDbContext.Set<T>().Remove(entity);
            }
        }

        /// <inheritdoc />
        public T FindById(long id) =>
            this.Query().FirstOrDefault(x => x.Id == id);

        /// <inheritdoc />
        public IQueryable<T> Query() =>
            this.productManagementDbContext.Set<T>().AsQueryable();

        /// <inheritdoc />
        public void Update(T entity) =>
            this.productManagementDbContext.Set<T>().Update(entity);
    }
}
