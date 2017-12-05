namespace ProductManagement.Services.Core.Product.Creation.Strategy
{
    using ProductManagement.DataRepresentation.Model;

    public interface IProductCreationStrategy
    {
        void Execute(Product product);
    }
}
