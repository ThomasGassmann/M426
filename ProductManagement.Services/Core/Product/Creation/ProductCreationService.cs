namespace ProductManagement.Services.Core.Product.Creation
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.Services.Core.Product.Creation.Strategy;
    using ProductManagement.Services.UoW;

    /// <summary>
    /// Defines the default service to create products.
    /// </summary>
    public class ProductCreationService : IProductCreationService
    {
        /// <summary>
        /// The unit of work factory to create transactions on the database.
        /// </summary>
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCreationService"/> class.
        /// </summary>
        /// <param name="unitOfWorkFactory">The <see cref="IUnitOfWorkFactory"/> dependency.</param>
        public ProductCreationService(IUnitOfWorkFactory unitOfWorkFactory) =>
            this.unitOfWorkFactory = unitOfWorkFactory;

        /// <inheritdoc />
        public long CreateProduct(ProductCreationDto dto, IProductCreationStrategy creationStrategy)
        {
            using (var unitOfWork = this.unitOfWorkFactory.CreateUnitOfWork())
            {
                var productRepository = unitOfWork.CreateEntityRepository<Product>();
                var product = Mapper.Map<Product>(dto);
                creationStrategy.Execute(product);
                productRepository.Create(product);
                unitOfWork.Save();
                return product.Id;
            }
        }
    }
}
