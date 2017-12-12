namespace ProductManagement.DataRepresentation.Dto
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the view model used to create and edit products.
    /// </summary>
    public class EditProductDto
    {
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Gets or sets the title of the product.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product. It will be null, if it's a new product.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the selected strategy for creating a product.
        /// </summary>
        public string SelectedStrategy { get; set; }
    }
}
