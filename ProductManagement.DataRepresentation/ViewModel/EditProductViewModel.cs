namespace ProductManagement.DataRepresentation.ViewModel
{
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the view model used to create and edit products.
    /// </summary>
    public class EditProductViewModel
    {
        /// <summary>
        /// Gets or sets the price. It is required and must be within a range of 0 and 1000.
        /// </summary>
        [Range(0, 1000)]
        [FromForm]
        [Required]
        public double? Price { get; set; }

        /// <summary>
        /// Gets or sets the title of the product. It needs to be at least 5 characters long and
        /// have a maximum length of a 100 characters.
        /// </summary>
        [MinLength(5)]
        [MaxLength(100)]
        [FromForm]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description. It needs to be at least 50 characters long.
        /// </summary>
        [MinLength(50)]
        [FromForm]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product. It will be null, if it's a new product.
        /// The parameter will be passed via the route.
        /// </summary>
        [FromRoute]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or sets the selected strategy for creating a product.
        /// </summary>
        [Display(Name = "Strategy")]
        public string SelectedStrategy { get; set; }
    }
}
