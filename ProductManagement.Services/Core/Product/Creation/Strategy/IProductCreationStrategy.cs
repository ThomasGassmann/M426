namespace ProductManagement.Services.Core.Product.Creation.Strategy
{
    using ProductManagement.DataRepresentation.Model;

    /// <summary>
    /// Defines a creation strategy for products.
    /// </summary>
    public interface IProductCreationStrategy
    {
        /// <summary>
        /// Executes the given strategy.
        /// </summary>
        /// <param name="product">The product to execute the strategy on.</param>
        void Execute(Product product);
    }
}
