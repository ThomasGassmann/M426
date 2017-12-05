using System;

namespace ProductManagement.DataRepresentation.Model
{
    /// <summary>
    /// Defines the product entity on the database.
    /// </summary>
    public class Product : IIdentifiable, ICreated, IUpdated
    {
        /// <inheritdoc />
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the title of the product.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public double Price { get; set; }

        /// <inheritdoc />
        public DateTime Created { get; set; }

        /// <inheritdoc />
        public DateTime Updated { get; set; }
    }
}
