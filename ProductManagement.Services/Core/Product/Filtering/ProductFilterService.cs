namespace ProductManagement.Services.Core.Product.Filtering
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.Services.Query;
    using System.Linq;

    public class ProductFilterService : IProductFilterService
    {
        private readonly IQueryService queryService;

        public ProductFilterService(IQueryService queryService) =>
            this.queryService = queryService;

        public ViewProductDto[] GetByTitle(int page, int pageSize)
        {
            var ordered = this.queryService.Query<Product>().OrderBy(x => x.Title);
            var fetchedPage = ordered.Skip(page * pageSize).Take(pageSize);
            var mapped = fetchedPage.Select(Mapper.Map<ViewProductDto>);
            return mapped.ToArray();
        }
    }
}
