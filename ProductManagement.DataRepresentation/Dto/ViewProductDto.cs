namespace ProductManagement.DataRepresentation.Dto
{
    using System;

    /// <summary>
    /// Defines the data transfer from the database to the view.
    /// </summary>
    public class ViewProductDto
    {
        /// <summary>
        /// Gets or sets the id on the database.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the product.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public DateTime Updated { get; set; }
    }
}
