namespace ProductManagement.Services.Core.Product.Creation.Strategy
{
    using ProductManagement.DataRepresentation.Model;
    using System;

    /// <summary>
    /// The strategy for creating food products.
    /// </summary>
    public class FoodCreationStrategy : IProductCreationStrategy
    {
        /// <summary>
        /// Stores the instance of the singleton class lazily. It will instantiated the 
        /// <see cref="FoodCreationStrategy"/> whenever requested.
        /// </summary>
        private static readonly Lazy<IProductCreationStrategy> instance =
            new Lazy<IProductCreationStrategy>(() => new FoodCreationStrategy());

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodCreationStrategy"/> class.
        /// </summary>
        private FoodCreationStrategy()
        {
        }

        /// <summary>
        /// Gets the instance of the strategy.
        /// </summary>
        public static IProductCreationStrategy Instance => FoodCreationStrategy.instance.Value;

        /// <inheritdoc />
        public void Execute(Product product)
        {
            product.Price = Math.Min(10, product.Price);
            product.Title = $"[Food] {product.Title}";
        }
    }
}
