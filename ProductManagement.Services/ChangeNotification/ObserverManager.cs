namespace ProductManagement.Services.ChangeNotification
{
    using ProductManagement.DataRepresentation.Dto;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ObserverManager
    {
        private static readonly Lazy<ObserverManager> instance =
            new Lazy<ObserverManager>(() => new ObserverManager());

        private readonly ProductMailObserver productMailObserver;

        private readonly IDictionary<long, List<ObservableProduct>> observed = new Dictionary<long, List<ObservableProduct>>();

        private ObserverManager()
        {
            this.productMailObserver = new ProductMailObserver(new MailConfigurationDto
            {
                From = "productmanagement.m426@gmail.com",
                Host = "smtp.gmail.com",
                Password = "aslk34!$j",
                Port = 587,
                UserName = "productmanagement.m426@gmail.com",
                UseDefaultCredentials = false,
                UseSsl = true
            });
        }

        public static ObserverManager Instance => ObserverManager.instance.Value;

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
