namespace ProductManagement.Services.Core.Product.Filtering
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.Services.Query;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The service used to filter products from the database.
    /// </summary>
    public class ProductFilterService : IProductFilterService
    {
        /// <summary>
        /// The service to query changes from the database.
        /// </summary>
        private readonly IQueryService queryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductFilterService"/> class.
        /// </summary>
        /// <param name="queryService">The <see cref="IQueryService"/> dependency.</param>
        public ProductFilterService(IQueryService queryService) =>
            this.queryService = queryService;

        /// <inheritdoc />
        public IEnumerable<ViewProductDto> GetByTitle(int page, int pageSize)
        {
            var ordered = this.queryService.Query<Product>().OrderBy(x => x.Title);
            var fetchedPage = ordered.Skip(page * pageSize).Take(pageSize);
            var mapped = fetchedPage.Select(Mapper.Map<ViewProductDto>);
            return mapped;
        }
    }
}
