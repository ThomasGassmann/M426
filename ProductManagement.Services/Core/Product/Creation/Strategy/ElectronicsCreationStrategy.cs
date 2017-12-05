namespace ProductManagement.Services.Core.Product.Creation.Strategy
{
    using ProductManagement.DataRepresentation.Model;
    using System;

    public class ElectronicsCreationStrategy : IProductCreationStrategy
    {
        private readonly double electronicsTax;

        private static readonly Lazy<IProductCreationStrategy> instance =
            new Lazy<IProductCreationStrategy>(() => new ElectronicsCreationStrategy(0.5));

        private ElectronicsCreationStrategy(double electronicsTax)
        {
            this.electronicsTax = electronicsTax;
        }

        public static IProductCreationStrategy Instance => ElectronicsCreationStrategy.instance.Value;

        public void Execute(Product product)
        {
            var tax = product.Price * this.electronicsTax;
            product.Price += tax;
            product.Title = $"[Electronics] {product.Title}";
        }
    }
}
