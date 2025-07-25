//crear mapper para mapear los modelos de vista a los modelos de dominio
using AutoMapper;
using Business.BusinessEntities;
using Business.BusinessLogic.Parameters;
using Domain;
using InventoryService.ViewModels;

namespace InventoryService.Mappers
{
    public class ViewModelMapperProfile : Profile
    {
        public ViewModelMapperProfile()
        {
            CreateMap<ProductViewModel, ProductCreateParameter>()
                .ReverseMap();
            CreateMap<ProductViewModel, ProductUpdateParameter>()
                .ReverseMap();
        }
    }
}