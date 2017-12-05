namespace ProductManagement.DataRepresentation.Mappings
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.Model;

    /// <summary>
    /// Defines how to map a <see cref="Product"/> to a <see cref="ViewProductDto"/>.
    /// </summary>
    public class ViewProductDtoMap : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewProductDtoMap"/> class.
        /// Defines how to map an product to its view model to use these values to render the view.
        /// </summary>
        public ViewProductDtoMap()
        {
            this.CreateMap<Product, ViewProductDto>()
                .ForMember(x => x.Description, x => x.MapFrom(j => j.Description))
                .ForMember(x => x.Title, x => x.MapFrom(j => j.Title))
                .ForMember(x => x.Price, x => x.MapFrom(j => j.Price))
                .ForMember(x => x.Created, x => x.MapFrom(j => j.Created))
                .ForMember(x => x.Updated, x => x.MapFrom(j => j.Updated))
                .ForMember(x => x.Id, x => x.MapFrom(j => j.Id));
        }
    }
}
