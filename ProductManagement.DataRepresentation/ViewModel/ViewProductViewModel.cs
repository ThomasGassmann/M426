namespace ProductManagement.DataRepresentation.ViewModel
{
    using System;

    /// <summary>
    /// Defines the view model used to display a product in the list.
    /// </summary>
    public class ViewProductViewModel
    {
        /// <summary>
        /// Gets or sets the ID of the product.
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
        /// Gets or setes the price of the product.
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
