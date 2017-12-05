namespace ProductManagement.Services.Core.Product.Edit
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.DataRepresentation.ViewModel;
    using ProductManagement.Services.Query;
    using System.Linq;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.Services.UoW;

    public class ProductEditService : IProductEditService
    {
        private readonly IQueryService queryService;

        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public ProductEditService(IQueryService queryService, IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.queryService = queryService;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public EditProductViewModel GetViewModel(long? id)
        {
            if (!id.HasValue)
            {
                return new EditProductViewModel();
            }

            var fetched = this.queryService.Query<Product>().FirstOrDefault(x => x.Id == id);
            return Mapper.Map<EditProductViewModel>(fetched);
        }

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
