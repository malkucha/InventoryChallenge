using AutoMapper;
using Business.BusinessEntities;
using Domain;

namespace Business.BusinessMapper
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<ProductEntity, ProductBusinessEntity>().ReverseMap();
        }
    }
}
