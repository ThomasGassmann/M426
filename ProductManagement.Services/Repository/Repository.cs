namespace ProductManagement.Services.Repository
{
    using ProductManagement.DataAccess.Context;
    using ProductManagement.DataRepresentation;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class Repository<T> : IRepository<T> where T : class, IIdentifiable
    {
        private readonly ProductManagementDbContext productManagementDbContext;

        public Repository(ProductManagementDbContext productManagementDbContext) =>
            this.productManagementDbContext = productManagementDbContext;

        public T Create(T entity)
        {
            var entry = this.productManagementDbContext.Add(entity);
            return entry.Entity;
        }

        public void Delete(Expression<Func<T, bool>> func)
        {
            var results = this.Query().Where(func).ToArray();
            foreach (var result in results)
            {
                this.productManagementDbContext.Remove(result);
            }
        }

        public void Delete(long id)
        {
            var entity = this.FindById(id);
            if (entity != null)
            {
                this.productManagementDbContext.Set<T>().Remove(entity);
            }
        }

        public T FindById(long id) =>
            this.Query().FirstOrDefault(x => x.Id == id);

        public IQueryable<T> Query() =>
            this.productManagementDbContext.Set<T>().AsQueryable();

        public T Update(T entity)
        {
            this.productManagementDbContext.Set<T>().Update(entity);
            return entity;
        }
    }
}
