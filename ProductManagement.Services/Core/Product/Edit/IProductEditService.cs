namespace ProductManagement.Services.Core.Product.Edit
{
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.ViewModel;

    public interface IProductEditService
    {
        EditProductViewModel GetViewModel(long? id);

        void SaveEdit(long id, ProductCreationDto edited);
    }
}
