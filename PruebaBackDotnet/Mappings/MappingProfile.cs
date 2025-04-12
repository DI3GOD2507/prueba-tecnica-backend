using AutoMapper;
using PruebaBackDotnet.Models.Entities;
using PruebaBackDotnet.DTOs;

namespace PruebaBackDotnet.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Departamento, DepartamentoDto>();
            CreateMap<Cargo, CargoDto>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.Departamento))
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo));

            // Este es el que te faltaba para poder hacer POST y PUT
            CreateMap<UserDto, User>();
        }
    }

}
