namespace ProductManagement.Services.Core.Product.Edit
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.Services.Query;
    using ProductManagement.Services.UoW;
    using System.Linq;

    /// <summary>
    /// The service used to edit products.
    /// </summary>
    public class ProductEditService : IProductEditService
    {
        /// <summary>
        /// The service to query data from the database.
        /// </summary>
        private readonly IQueryService queryService;

        /// <summary>
        /// The unit of work factory to save changes to the database.
        /// </summary>
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductEditService"/> class.
        /// </summary>
        /// <param name="queryService">The <see cref="IQueryService"/> dependency.</param>
        /// <param name="unitOfWorkFactory">The <see cref="IUnitOfWorkFactory"/> dependency.</param>
        public ProductEditService(IQueryService queryService, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.queryService = queryService;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <inheritdoc />
        public EditProductDto GetProductEditDto(long? id)
        {
            if (!id.HasValue)
            {
                return new EditProductDto();
            }

            var fetched = this.queryService.Query<Product>().FirstOrDefault(x => x.Id == id);
            return Mapper.Map<EditProductDto>(fetched);
        }

        /// <inheritdoc />
        public void SaveEdit(long id, ProductCreationDto edited)
        {
            using (var unitOfWork = this.unitOfWorkFactory.CreateUnitOfWork())
            {
                var repository = unitOfWork.CreateEntityRepository<Product>();
                var product = repository.FindById(id);
                product.Description = edited.Description;
                product.Price = edited.Price;
                product.Title = edited.Title;
                repository.Update(product);
                unitOfWork.Save();
            }
        }
    }
}
