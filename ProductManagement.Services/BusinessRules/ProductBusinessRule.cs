namespace ProductManagement.Services.BusinessRules
{
    using ProductManagement.DataRepresentation.Model;
    using System;
    using System.Collections.Generic;

    public class ProductBusinessRule : BusinessRuleBase<Product>
    {
        public override void PreSave(IList<Product> added, IList<Product> updated, IList<Product> removed)
        {
            foreach (var addedItem in added)
            {

            }
        }

        public void ThrowIfInvalid(Product product)
        {
            if (product.Price < 0)
            {
                throw new ArgumentOutOfRangeException("Price must be higher than 0");
            }
        }
    }
}
