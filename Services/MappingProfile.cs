using aHoang.DTO;
using aHoang.Entities;
using AutoMapper;
namespace aHoang.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemDTO>().ForMember(dest => dest.File, opt => opt.Ignore()); 
            CreateMap<ItemDTO, Item>().ForMember(dest => dest.ImageFileName, opt => opt.Ignore()); 
        }
    }
}
