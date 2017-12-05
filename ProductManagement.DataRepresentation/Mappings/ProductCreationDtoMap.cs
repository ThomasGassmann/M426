namespace ProductManagement.DataRepresentation.Mappings
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.ViewModel;

    /// <summary>
    /// Defines how to map an <see cref="EditProductViewModel"/> to an <see cref="ProductCreationDto"/>.
    /// </summary>
    public class ProductCreationDtoMap : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCreationDtoMap"/> class.
        /// Defines how to map the product values from the view to values for the service used to create the product.
        /// </summary>
        public ProductCreationDtoMap()
        {
            this.CreateMap<EditProductViewModel, ProductCreationDto>()
                .ForMember(x => x.Description, x => x.MapFrom(j => j.Description))
                .ForMember(x => x.Price, x => x.MapFrom(j => j.Price))
                .ForMember(x => x.Title, x => x.MapFrom(j => j.Title));
        }
    }
}
