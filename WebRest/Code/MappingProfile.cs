using AutoMapper;
using WebRestEF.EF.Models;
using WebRestShared.DTO;

namespace WebRest.Code
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Gender, GenderDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
