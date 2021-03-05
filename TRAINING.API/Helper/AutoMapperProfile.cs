using System;
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
                .ForMember(des => des.Grade, opt => opt.MapFrom(src => src.EMEGNO))
                .ForMember(des => des.BirthDate, opt => opt.MapFrom(src => CommonMethod.NumericToDateNullable(src.EMBTDT)));
            CreateMap<EmployeeDto, MEMP>()
                .ForMember(des => des.EMEMNO, opt => opt.MapFrom(src => src.Nik))
                .ForMember(des => des.EMEMNA, opt => opt.MapFrom(src => src.Nama))
                .ForMember(des => des.EMDENO, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(des => des.EMEGNO, opt => opt.MapFrom(src => src.Grade))
                .ForMember(des => des.EMRCST, opt => opt.MapFrom(src => 1))
                .ForMember(des => des.EMCRDT, opt => opt.MapFrom(src => CommonMethod.DateToNumeric(DateTime.Now)))
                .ForMember(des => des.EMCRTM, opt => opt.MapFrom(src => CommonMethod.TimeToNumeric(DateTime.Now)))
                .ForMember(des => des.EMCHDT, opt => opt.MapFrom(src => CommonMethod.DateToNumeric(DateTime.Now)))
                .ForMember(des => des.EMCHTM, opt => opt.MapFrom(src => CommonMethod.TimeToNumeric(DateTime.Now)));
            CreateMap<MGRD, DropdownDto>()
                .ForMember(des => des.Label, opt => opt.MapFrom(src => src.GDEGNA))
                .ForMember(des => des.Value, opt => opt.MapFrom(src => src.GDEGNO));
            CreateMap<GOG1, DropdownDto>()
                .ForMember(des => des.Label, opt => opt.MapFrom(src => src.GOOGNA))
                .ForMember(des => des.Value, opt => opt.MapFrom(src => src.GOOGNO));
        }
    }
}