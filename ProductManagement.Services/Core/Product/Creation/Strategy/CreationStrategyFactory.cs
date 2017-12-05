namespace ProductManagement.Services.Core.Product.Creation.Strategy
{
    public static class CreationStrategyFactory
    {
        public static IProductCreationStrategy CreateStrategy(string identifier)
        {
            switch (identifier)
            {
                case "Food":
                    return FoodCreationStrategy.Instance;
                case "Electronics":
                    return ElectronicsCreationStrategy.Instance;
                default:
                    return null;
            }
        }
    }
}
