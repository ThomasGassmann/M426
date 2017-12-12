namespace ProductManagement.Services.BusinessRules
{
    using ProductManagement.DataRepresentation.Model;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Validates all products and throws an exception if they're invalid.
    /// </summary>
    public class ProductBusinessRule : BusinessRuleBase<Product>
    {
        /// <summary>
        /// Loops through all added items and throws an exception if the entity is invalid.
        /// </summary>
        /// <param name="added">All added items.</param>
        /// <param name="updated">All updated items.</param>
        /// <param name="removed">All removed items.</param>
        public override void PreSave(IList<Product> added, IList<Product> updated, IList<Product> removed)
        {
            foreach (var addedItem in added)
            {
                this.ThrowIfInvalid(addedItem);
            }
        }

        /// <summary>
        /// Throws an exception if the given product is not valid.
        /// </summary>
        /// <param name="product">The produdct to check.</param>
        private void ThrowIfInvalid(Product product)
        {
            if (product.Price < 0)
            {
                throw new ArgumentOutOfRangeException("Price must be higher than 0");
            }
        }
    }
}
