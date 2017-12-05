namespace ProductManagement.Services.Core.Product.Creation.Strategy
{
    using ProductManagement.DataRepresentation.Model;
    using System;

    public class FoodCreationStrategy : IProductCreationStrategy
    {
        private static readonly Lazy<IProductCreationStrategy> instance =
            new Lazy<IProductCreationStrategy>(() => new FoodCreationStrategy());

        private FoodCreationStrategy()
        {
        }

        public static IProductCreationStrategy Instance => FoodCreationStrategy.instance.Value;

        public void Execute(Product product)
        {
            product.Price = Math.Min(10, product.Price);
            product.Title = $"[Food] {product.Title}";
        }
    }
}
