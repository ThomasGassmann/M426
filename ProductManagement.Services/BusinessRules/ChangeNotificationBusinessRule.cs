namespace ProductManagement.Services.BusinessRules
{
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.Services.ChangeNotification;
    using ProductManagement.Services.UoW;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// A business rule used for the observer pattern implementation. After saving the entity on
    /// the database all observers will be notified about the change.
    /// </summary>
    public class ChangeNotificationBusinessRule : BusinessRuleBase<Product>
    {
        /// <summary>
        /// The list of all updated products to notify about after saving.
        /// </summary>
        private IList<Product> updatedProducts;

        /// <summary>
        /// The manager of all observers.
        /// </summary>
        private readonly IObserverManager observerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeNotificationBusinessRule"/> class.
        /// </summary>
        /// <param name="observerManager">The <see cref="IObserverManager"/> dependency.</param>
        public ChangeNotificationBusinessRule(IObserverManager observerManager) =>
            this.observerManager = observerManager;

        /// <summary>
        /// All updated items will be stored in this class before saving the changes to the database.
        /// </summary>
        /// <param name="added">All added items.</param>
        /// <param name="updated">All updated items.</param>
        /// <param name="removed">All removed items.</param>
        public override void PreSave(IList<Product> added, IList<Product> updated, IList<Product> removed)
        {
            this.updatedProducts = updated;
        }

        /// <summary>
        /// Notifies all observers about the changes made after saving those changes.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> of the process.</param>
        public override void PostSave(IUnitOfWork unitOfWork)
        {
            foreach (var updatedProduct in this.updatedProducts)
            {
                Task.Run(async () => await this.observerManager.Notify(updatedProduct.Id));
            }
        }
    }
}
