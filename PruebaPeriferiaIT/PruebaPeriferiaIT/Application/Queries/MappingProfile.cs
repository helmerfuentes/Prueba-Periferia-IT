using AutoMapper;

namespace PruebaPeriferiaIT.Application.Queries
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entitites.Employee, EmployeeDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name));
        }
    }
}
