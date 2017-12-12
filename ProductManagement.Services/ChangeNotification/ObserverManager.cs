namespace ProductManagement.Services.ChangeNotification
{
    using ProductManagement.DataRepresentation.Settings;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    /// <summary>
    /// The class to manage all observables and observers.
    /// </summary>
    public class ObserverManager : IObserverManager
    {
        /// <summary>
        /// Contains the observer, which will send an email to the user, if a product has changed.
        /// </summary>
        private readonly ProductMailObserver productMailObserver;

        /// <summary>
        /// Contains all observables for mapped for a given product id.
        /// Every product has multiple observables.
        /// </summary>
        private readonly IDictionary<long, List<ObservableProduct>> observed = new Dictionary<long, List<ObservableProduct>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ObserverManager"/> class.
        /// It creates the observer.
        /// </summary>
        /// <param name="settings">The <see cref="ISmtpSettings"/> dependency.</param>
        public ObserverManager(ISmtpSettings settings)
        {
            this.productMailObserver = new ProductMailObserver(settings);
        }

        /// <inheritdoc />
        public void Create(long productId, string email)
        {
            var observer = new ObservableProduct(productId, email);
            observer.Attach(this.productMailObserver);
            if (this.observed.ContainsKey(productId))
            {
                this.observed[productId].Add(observer);
            }
            else
            {
                this.observed.Add(productId, new List<ObservableProduct> { observer });
            }
        }

        /// <inheritdoc />
        public async Task Notify(long productId)
        {
            if (this.observed.ContainsKey(productId))
            {
                var observedProducts = this.observed[productId];
                foreach (var observedProduct in observedProducts)
                {
                    await observedProduct.Notify();
                }
            }
        }
    }
}
