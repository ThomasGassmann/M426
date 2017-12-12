namespace ProductManagement.Services.ChangeNotification
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an observer type.
    /// </summary>
    /// <typeparam name="T">The type to be observed.</typeparam>
    public interface IObserver<T>
    {
        /// <summary>
        /// Notifies the observer about a change.
        /// </summary>
        /// <param name="changed">The changed entity.</param>
        /// <returns>Returns the task, to await asynchronous operations.</returns>
        Task Update(T changed);
    }
}
