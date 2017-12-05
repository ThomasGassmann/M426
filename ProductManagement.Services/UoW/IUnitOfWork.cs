namespace ProductManagement.Services.UoW
{
    using ProductManagement.Services.Repository;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> CreateEntityRepository<T>();

        void Save();
    }
}
