namespace ProductManagement.Services.Core.Product.Creation
{
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.Services.Core.Product.Creation.Strategy;

    /// <summary>
    /// Defines a service to create a product.
    /// </summary>
    public interface IProductCreationService
    {
        /// <summary>
        /// Creates a product for the given data with the given strategy.
        /// </summary>
        /// <param name="dto">The data to create the product.</param>
        /// <param name="taxStrategy">The strategy to create the product with.</param>
        /// <returns>Returns the id of the created product.</returns>
        long CreateProduct(ProductCreationDto dto, IProductCreationStrategy taxStrategy);
    }
}
