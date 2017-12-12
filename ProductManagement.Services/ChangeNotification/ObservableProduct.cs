namespace ProductManagement.Services.ChangeNotification
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a product to be observed.
    /// </summary>
    public class ObservableProduct : IObservable<ObservableProduct>
    {
        /// <summary>
        /// Contains all observers for the product.
        /// </summary>
        private readonly IList<IObserver<ObservableProduct>> observers =
            new List<IObserver<ObservableProduct>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableProduct"/> class. 
        /// </summary>
        /// <param name="productId">The id of the product to observe.</param>
        /// <param name="mail">The mail to send the notification to.</param>
        public ObservableProduct(long productId, string mail)
        {
            this.ProductId = productId;
            this.Email = mail;
        }

        /// <summary>
        /// Gets the id of the changed product.
        /// </summary>
        public long ProductId { get; private set; }

        /// <summary>
        /// Gets the email of the person to be notified.
        /// </summary>
        public string Email { get; private set; }

        /// <inheritdoc />
        public void Attach(IObserver<ObservableProduct> observer) =>
            this.observers.Add(observer);
        
        /// <inheritdoc />
        public void Detach(IObserver<ObservableProduct> observer) =>
            this.observers.Remove(observer);

        /// <inheritdoc />
        public async Task Notify()
        {
            foreach (var observer in this.observers)
            {
                await observer.Update(this);
            }
        }
    }
}
