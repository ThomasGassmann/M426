namespace ProductManagement.Services.ChangeNotification
{
    using System.Threading.Tasks;

    public interface IObserver<T>
    {
        Task Update(T changed);
    }
}
