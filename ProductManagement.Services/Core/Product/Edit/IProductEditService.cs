namespace ProductManagement.Services.Core.Product.Edit
{
    using ProductManagement.DataRepresentation.Dto;

    /// <summary>
    /// Defines a service to edit products.
    /// </summary>
    public interface IProductEditService
    {
        /// <summary>
        /// Gets the product data used to edit a given product
        /// </summary>
        /// <param name="id">The id of the product to edit.</param>
        /// <returns>Returns the data used to edit a product in form of a data transfer object.</returns>
        EditProductDto GetProductEditDto(long? id);

        /// <summary>
        /// Saves the changed produdct to the database.
        /// </summary>
        /// <param name="id">The id of the object to save the edit to.</param>
        /// <param name="edited">The edited product data.</param>
        void SaveEdit(long id, ProductCreationDto edited);
    }
}
