namespace ProductManagement.Services.UoW
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();

        IUnitOfWork GetLatest();
    }
}
