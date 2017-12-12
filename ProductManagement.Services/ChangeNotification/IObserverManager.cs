namespace ProductManagement.Services.ChangeNotification
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a manager for all observers. It will be used 
    /// as a singleton within the depdendency injection container.
    /// </summary>
    public interface IObserverManager
    {
        /// <summary>
        /// Notifies all observables about the changes.
        /// </summary>
        /// <param name="productId">The id of the changed product.</param>
        /// <returns>Returns the task, to await asynchronous operations.</returns>
        Task Notify(long productId);

        /// <summary>
        /// Creates a new observable for a given product.
        /// </summary>
        /// <param name="productId">The id of the product to observe.</param>
        /// <param name="email">The email to send the notifications to.</param>
        void Create(long productId, string email);
    }
}
