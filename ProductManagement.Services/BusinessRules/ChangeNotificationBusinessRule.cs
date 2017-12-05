namespace ProductManagement.Services.BusinessRules
{
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.Services.ChangeNotification;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProductManagement.Services.UoW;

    public class ChangeNotificationBusinessRule : BusinessRuleBase<Product>
    {
        private IList<Product> updatedProducts;

        public override void PreSave(IList<Product> added, IList<Product> updated, IList<Product> removed)
        {
            this.updatedProducts = updated;
        }

        public override void PostSave(IUnitOfWork unitOfWork)
        {
            foreach (var updatedProduct in this.updatedProducts)
            {
                Task.Run(async () => await ObserverManager.Instance.Notify(updatedProduct.Id));
            }
        }
    }
}
