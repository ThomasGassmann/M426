namespace ProductManagement.Services.Core.Product.Filtering
{
    using ProductManagement.DataRepresentation.Dto;

    public interface IProductFilterService
    {
        ViewProductDto[] GetByTitle(int page, int pageSize);
    }
}
