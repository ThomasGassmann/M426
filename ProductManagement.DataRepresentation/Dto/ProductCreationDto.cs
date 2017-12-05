namespace ProductManagement.DataRepresentation.Dto
{
    /// <summary>
    /// Defines the data used to create a new product.
    /// </summary>
    public class ProductCreationDto
    {
        /// <summary>
        /// Gets or sets the title of the new product.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the price of the new product.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }
    }
}
