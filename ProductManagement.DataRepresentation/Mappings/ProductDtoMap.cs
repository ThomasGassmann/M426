namespace ProductManagement.DataRepresentation.Mappings
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.Model;

    /// <summary>
    /// Defines how to map a <see cref="ProductCreationDto"/> to a <see cref="Product"/>.
    /// </summary>
    public class ProductDtoMap : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDtoMap"/> class.
        /// Defines how to map the data from the creation service to values which can be stored in the database.
        /// </summary>
        public ProductDtoMap()
        {
            this.CreateMap<ProductCreationDto, Product>()
                .ForMember(x => x.Description, x => x.MapFrom(j => j.Description))
                .ForMember(x => x.Price, x => x.MapFrom(j => j.Price))
                .ForMember(x => x.Title, x => x.MapFrom(j => j.Title))
                .ForMember(x => x.Id, x => x.Ignore());
        }
    }
}
