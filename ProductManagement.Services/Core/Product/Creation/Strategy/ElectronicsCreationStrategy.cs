namespace ProductManagement.Services.Core.Product.Creation.Strategy
{
    using ProductManagement.DataRepresentation.Model;
    using System;

    /// <summary>
    /// The strategy for creating electronic products.
    /// </summary>
    public class ElectronicsCreationStrategy : IProductCreationStrategy
    {
        /// <summary>
        /// Contains the tax on electronics.
        /// </summary>
        private readonly double electronicsTax;

        /// <summary>
        /// Stores the instance of the singleton class lazily. It will instantiated the 
        /// <see cref="ElectronicsCreationStrategy"/> with the default tax rate whenever
        /// requested.
        /// </summary>
        private static readonly Lazy<IProductCreationStrategy> instance =
            new Lazy<IProductCreationStrategy>(() => new ElectronicsCreationStrategy(0.5));

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectronicsCreationStrategy"/> class.
        /// </summary>
        /// <param name="electronicsTax">The tax for electronics</param>
        private ElectronicsCreationStrategy(double electronicsTax) =>
            this.electronicsTax = electronicsTax;

        /// <summary>
        /// Gets the instance of the strategy.
        /// </summary>
        public static IProductCreationStrategy Instance => ElectronicsCreationStrategy.instance.Value;

        /// <inheritdoc />
        public void Execute(Product product)
        {
            var tax = product.Price * this.electronicsTax;
            product.Price += tax;
            product.Title = $"[Electronics] {product.Title}";
        }
    }
}
