using AutoMapper;
using WebRestEF.EF.Models;

namespace WebRest.Code
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Gender, GenderDTO>().ReverseMap();
        }
    }
}
