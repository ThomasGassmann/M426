namespace ProductManagement.Services.Core.Product.Filtering
{
    using ProductManagement.DataRepresentation.Dto;
    using System.Collections.Generic;

    /// <summary>
    /// The service used to filter products from the database.
    /// </summary>
    public interface IProductFilterService
    {
        /// <summary>
        /// Gets a page of products to display in the list for the user.
        /// </summary>
        /// <param name="page">The page to display</param>
        /// <param name="pageSize">The size fo the page to display.</param>
        /// <returns>Returns all products on the page.</returns>
        IEnumerable<ViewProductDto> GetByTitle(int page, int pageSize);
    }
}
