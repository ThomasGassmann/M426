namespace ProductManagement.Services.Core.Product.Creation
{
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.Services.Core.Product.Creation.Strategy;

    public interface IProductCreationService
    {
        long CreateProduct(ProductCreationDto dto, IProductCreationStrategy taxStrategy);
    }
}
