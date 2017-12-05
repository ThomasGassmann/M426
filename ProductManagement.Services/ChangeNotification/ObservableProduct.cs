namespace ProductManagement.Services.ChangeNotification
{
    using ProductManagement.DataRepresentation.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ObservableProduct : IObservable<ObservableProduct>
    {
        private readonly IList<IObserver<ObservableProduct>> observers =
            new List<IObserver<ObservableProduct>>();

        public ObservableProduct(long productId, string mail)
        {
            this.ProductId = productId;
            this.Email = mail;
        }

        public long ProductId { get; private set; }

        public string Email { get; private set; }

        public void Attach(IObserver<ObservableProduct> observer) =>
            this.observers.Add(observer);

        public void Detach(IObserver<ObservableProduct> observer) =>
            this.observers.Remove(observer);

        public async Task Notify()
        {
            foreach (var observer in this.observers)
            {
                await observer.Update(this);
            }
        }
    }
}
