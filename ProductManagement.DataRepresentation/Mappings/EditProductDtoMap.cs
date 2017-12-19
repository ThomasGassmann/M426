namespace ProductManagement.DataRepresentation.Mappings
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.DataRepresentation.ViewModel;

    /// <summary>
    /// Defines the logic to map a model from <see cref="Product"/> to <see cref="EditProductDto"/>.
    /// </summary>
    public class EditProductDtoMap : Profile
    {
        /// <summary>
        /// Initialiezs a new instance of the <see cref="EditProductViewModelMap"/> class.
        /// Defines how to map the class from the database to the data transfer object.
        /// </summary>
        public EditProductDtoMap()
        {
            this.CreateMap<Product, EditProductDto>()
                .ForMember(x => x.Description, x => x.MapFrom(j => j.Description))
                .ForMember(x => x.Price, x => x.MapFrom(j => j.Price))
                .ForMember(x => x.Title, x => x.MapFrom(j => j.Title))
                .ForMember(x => x.SelectedStrategy, x => x.Ignore())
                .ForMember(x => x.Id, x => x.MapFrom(j => j.Id));

            this.CreateMap<EditProductViewModel, EditProductDto>()
                .ForMember(x => x.Description, x => x.MapFrom(j => j.Description))
                .ForMember(x => x.Price, x => x.MapFrom(j => j.Price))
                .ForMember(x => x.Title, x => x.MapFrom(j => j.Title))
                .ForMember(x => x.SelectedStrategy, x => x.Ignore())
                .ForMember(x => x.Id, x => x.MapFrom(j => j.Id));
        }
    }
}
