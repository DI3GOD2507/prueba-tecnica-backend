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
                .ForMember(dest => dest.Cargo, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.IdDepartamento, opt => opt.MapFrom(src => src.IdDepartamento))
                .ForMember(dest => dest.IdCargo, opt => opt.MapFrom(src => src.IdCargo));


            CreateMap<UserDto, User>()
                .ForMember(dest => dest.IdDepartamento, opt => opt.MapFrom(src => src.IdDepartamento))
                .ForMember(dest => dest.IdCargo, opt => opt.MapFrom(src => src.IdCargo))

                .ForMember(dest => dest.Departamento, opt => opt.Ignore())
                .ForMember(dest => dest.Cargo, opt => opt.Ignore())

                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}