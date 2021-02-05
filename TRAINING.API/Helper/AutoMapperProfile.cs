using AutoMapper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MEMP, EmployeeDto>()
                .ForMember(des => des.Nik, opt => opt.MapFrom(src => src.EMEMNO))
                .ForMember(des => des.Nama, opt => opt.MapFrom(src => src.EMEMNA))
                .ForMember(des => des.DepartmentId, opt => opt.MapFrom(src => src.EMDENO))
                .ForMember(des => des.Department, opt => opt.MapFrom(src => src.GOG1.GOOGNA))
                .ForMember(des => des.Grade, opt => opt.MapFrom(src => src.EMEGNO));
        }
    }
}