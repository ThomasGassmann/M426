namespace ProductManagement.Services.Core.Product.Creation.Strategy
{
    /// <summary>
    /// The factory for a product creation strategy.
    /// </summary>
    public static class CreationStrategyFactory
    {
        /// <summary>
        /// Creates a strategy for the given string, which identifies the strategy.
        /// </summary>
        /// <param name="identifier">The identifier of the strategy. Possible values are 'Food' and 'Electronics'.</param>
        /// <returns>Returns the instantiated strategy.</returns>
        public static IProductCreationStrategy CreateStrategy(string identifier)
        {
            switch (identifier.ToLower())
            {
                case "food":
                    return FoodCreationStrategy.Instance;
                case "electronics":
                    return ElectronicsCreationStrategy.Instance;
                default:
                    return null;
            }
        }
    }
}
