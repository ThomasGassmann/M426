namespace ProductManagement.DataRepresentation.Mappings
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.ViewModel;

    /// <summary>
    /// Defines how to map a <see cref="ViewProductDto"/> to a <see cref="ViewProductViewModel"/>.
    /// </summary>
    public class ViewProductViewModelMap : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewProductViewModelMap"/> class.
        /// Defines how to map the temporary data from the service to the data displayed in the view.
        /// </summary>
        public ViewProductViewModelMap()
        {
            this.CreateMap<ViewProductDto, ViewProductViewModel>()
                .ForMember(x => x.Description, x => x.MapFrom(j => j.Description))
                .ForMember(x => x.Title, x => x.MapFrom(j => j.Title))
                .ForMember(x => x.Price, x => x.MapFrom(j => j.Price))
                .ForMember(x => x.Updated, x => x.MapFrom(j => j.Updated))
                .ForMember(x => x.Created, x => x.MapFrom(j => j.Created))
                .ForMember(x => x.Id, x => x.MapFrom(j => j.Id));
        }
    }
}
