namespace ProductManagement.Services.ChangeNotification
{
    using System.Threading.Tasks;

    public interface IObservable<T>
    {
        void Attach(IObserver<T> observer);

        void Detach(IObserver<T> observer);

        Task Notify();
    }
}
