namespace ProductManagement.Services.Core.Product.Creation
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.Services.Core.Product.Creation.Strategy;
    using ProductManagement.Services.UoW;

    public class ProductCreationService : IProductCreationService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public ProductCreationService(IUnitOfWorkFactory unitOfWorkFactory) =>
            this.unitOfWorkFactory = unitOfWorkFactory;

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
