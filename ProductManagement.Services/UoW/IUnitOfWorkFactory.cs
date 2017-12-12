namespace ProductManagement.Services.UoW
{
    /// <summary>
    /// Contains the factory for <see cref="IUnitOfWork"/> instances.
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates a new <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <returns>returns the created <see cref="IUnitOfWork"/>.</returns>
        IUnitOfWork CreateUnitOfWork();
    }
}
