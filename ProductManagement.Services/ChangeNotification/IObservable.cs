namespace ProductManagement.Services.ChangeNotification
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a type that observes another type.
    /// </summary>
    /// <typeparam name="T">The type of the class to observe.</typeparam>
    public interface IObservable<T>
    {
        /// <summary>
        /// Attaches an observer to the observable.
        /// </summary>
        /// <param name="observer">The type of the observer.</param>
        void Attach(IObserver<T> observer);

        /// <summary>
        /// Detaches an observer to the observable.
        /// </summary>
        /// <param name="observer">The type of the observer.</param>
        void Detach(IObserver<T> observer);

        /// <summary>
        /// Notifies the observer about the change.
        /// </summary>
        /// <returns>Returns the task, to await asynchronous operations.</returns>
        Task Notify();
    }
}
