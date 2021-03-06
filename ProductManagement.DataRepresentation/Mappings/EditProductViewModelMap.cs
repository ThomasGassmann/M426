﻿namespace ProductManagement.DataRepresentation.Mappings
{
    using AutoMapper;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.ViewModel;

    /// <summary>
    /// Defines the logic to map a model from <see cref="EditProductDto"/> to <see cref="EditProductViewModel"/>.
    /// </summary>
    public class EditProductViewModelMap : Profile
    {
        /// <summary>
        /// Initialiezs a new instance of the <see cref="EditProductViewModelMap"/> class.
        /// Defines how to map the data transfer object to the class used to display the product in the edit view.
        /// </summary>
        public EditProductViewModelMap()
        {
            this.CreateMap<EditProductDto, EditProductViewModel>()
                .ForMember(x => x.Description, x => x.MapFrom(j => j.Description))
                .ForMember(x => x.Price, x => x.MapFrom(j => j.Price))
                .ForMember(x => x.Title, x => x.MapFrom(j => j.Title))
                .ForMember(x => x.SelectedStrategy, x => x.MapFrom(j => j.SelectedStrategy))
                .ForMember(x => x.Id, x => x.MapFrom(j => j.Id));
        }
    }
}
