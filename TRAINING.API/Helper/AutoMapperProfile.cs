using System;
using AutoMapper;
using TRAINING.API.Model;
using TRAINING.API.Schema.Mutations;
using TRAINING.API.Schema.Queries;
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
                .ForMember(des => des.Photo, opt => opt.MapFrom<PhotoResolver, string>(src => src.EMBRNO + "|" + src.EMEMNO))
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

            CreateMap<MEMP, ELOG>()
                .ForMember(des => des.ELEMNO, opt => opt.MapFrom(src => src.EMEMNO))
                .ForMember(des => des.ELEMNA, opt => opt.MapFrom(src => src.EMEMNA))
                .ForMember(des => des.ELBRNO, opt => opt.MapFrom(src => src.EMBRNO))
                .ForMember(des => des.ELRFID, opt => opt.MapFrom(src => src.EMRFID))
                .ForMember(des => des.ELDENO, opt => opt.MapFrom(src => src.EMDENO))
                .ForMember(des => des.ELDENA, opt => opt.MapFrom(src => src.GOG1.GOOGNA))
                .ForMember(des => des.ELTRDT, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<ELOG, EmployeeDto>()
                .ForMember(des => des.Nik, opt => opt.MapFrom(src => src.ELEMNO))
                .ForMember(des => des.Nama, opt => opt.MapFrom(src => src.ELEMNA))
                .ForMember(des => des.DepartmentId, opt => opt.MapFrom(src => src.ELDENO))
                .ForMember(des => des.Department, opt => opt.MapFrom(src => src.ELDENA))
                .ForMember(des => des.Photo, opt => opt.MapFrom<PhotoResolver, string>(src => src.ELBRNO + "|" + src.ELEMNO))
                .ForMember(des => des.AttendDate, opt => opt.MapFrom(src => src.ELTRDT));

            CreateMap<ELOH, LogHeaderDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.EHRCID))
                .ForMember(des => des.Title, opt => opt.MapFrom(src => src.EHDESC))
                .ForMember(des => des.DateFrom, opt => opt.MapFrom(src => src.EHDTFR))
                .ForMember(des => des.DateTo, opt => opt.MapFrom(src => src.EHDTTO))
                .ForMember(des => des.DuplicateFlag, opt => opt.MapFrom(src => src.EHDUPF))
                .ForMember(des => des.PredefinedFlag, opt => opt.MapFrom(src => src.EHPREF))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => src.EHRCST));
            CreateMap<LogHeaderDto, ELOH>()
                .ForMember(des => des.EHRCID, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.EHDESC, opt => opt.MapFrom(src => src.Title))
                .ForMember(des => des.EHDTFR, opt => opt.MapFrom(src => src.DateFrom))
                .ForMember(des => des.EHDTTO, opt => opt.MapFrom(src => src.DateTo))
                .ForMember(des => des.EHDUPF, opt => opt.MapFrom(src => src.DuplicateFlag))
                .ForMember(des => des.EHPREF, opt => opt.MapFrom(src => src.PredefinedFlag))
                .ForMember(des => des.EHRCST, opt => opt.MapFrom(src => src.Status))
                .ForMember(des => des.EHCRDT, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(des => des.EHCHDT, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<MGRD, DropdownDto>()
                .ForMember(des => des.Label, opt => opt.MapFrom(src => src.GDEGNA))
                .ForMember(des => des.Value, opt => opt.MapFrom(src => src.GDEGNO));
            CreateMap<GOG1, DropdownDto>()
                .ForMember(des => des.Label, opt => opt.MapFrom(src => src.GOOGNA))
                .ForMember(des => des.Value, opt => opt.MapFrom(src => src.GOOGNO));
            CreateMap<MDF0, DropdownDto>()
                .ForMember(des => des.Label, opt => opt.MapFrom(src => src.DDDFGR))
                .ForMember(des => des.Value, opt => opt.MapFrom(src => src.DDDFGR));
            

            CreateMap<EHAL, EmployeeDto>()
                .ForMember(des => des.Nik, opt => opt.MapFrom(src => src.ELEMNO))
                .ForMember(des => des.Nama, opt => opt.MapFrom(src => src.ELEMNA))
                .ForMember(des => des.Department, opt => opt.MapFrom(src => src.ELDENA))
                .ForMember(des => des.FillDate, opt => opt.MapFrom(src => src.ELTRDT))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => ""));
            CreateMap<EmployeeDto, EHAL>()
                .ForMember(des => des.ELEMNO, opt => opt.MapFrom(src => src.Nik))
                .ForMember(des => des.ELEMNA, opt => opt.MapFrom(src => src.Nama))
                .ForMember(des => des.ELBRNO, opt => opt.MapFrom(src => "CKP"))
                .ForMember(des => des.ELRFID, opt => opt.MapFrom(src => src.RFID))
                .ForMember(des => des.ELDENO, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(des => des.ELDENA, opt => opt.MapFrom(src => src.Department))
                .ForMember(des => des.ELRCST, opt => opt.MapFrom(src => 1));

            CreateMap<EHAL, LebaranDto>()
                .ForMember(des => des.EmployeeId, opt => opt.MapFrom(src => src.ELEMNO))
                .ForMember(des => des.EmployeeName, opt => opt.MapFrom(src => src.ELEMNA))
                .ForMember(des => des.Location, opt => opt.MapFrom(src => src.ELBRNO))
                .ForMember(des => des.RFID, opt => opt.MapFrom(src => src.ELRFID))
                .ForMember(des => des.BirthCity, opt => opt.MapFrom(src => src.ELBTCT))
                .ForMember(des => des.BirthDate, opt => opt.MapFrom(src => src.ELBTDT))
                .ForMember(des => des.Age, opt => opt.MapFrom<AgeResolver, DateTime?>(src => src.ELBTDT))
                .ForMember(des => des.EKTP, opt => opt.MapFrom(src => src.ELEKTP))
                .ForMember(des => des.Photo, opt => opt.MapFrom<PhotoResolver, string>(src => src.ELBRNO + "|" + src.ELEMNO))
                .ForMember(des => des.DepartmentId, opt => opt.MapFrom(src => src.ELDENO))
                .ForMember(des => des.Department, opt => opt.MapFrom(src => src.ELDENA))
                .ForMember(des => des.FillDate, opt => opt.MapFrom(src => src.ELTRDT))
                .ForMember(des => des.Question01, opt => opt.MapFrom(src => src.ELSQ01))
                .ForMember(des => des.Question02, opt => opt.MapFrom(src => src.ELSQ02))
                .ForMember(des => des.Question03, opt => opt.MapFrom(src => src.ELSQ03))
                .ForMember(des => des.Question04, opt => opt.MapFrom(src => src.ELSQ04))
                .ForMember(des => des.Question05, opt => opt.MapFrom(src => src.ELSQ05))
                .ForMember(des => des.Question06, opt => opt.MapFrom(src => src.ELSQ06))
                .ForMember(des => des.Question07, opt => opt.MapFrom(src => src.ELSQ07))
                .ForMember(des => des.Question08, opt => opt.MapFrom(src => src.ELSQ08))
                .ForMember(des => des.Question09, opt => opt.MapFrom(src => src.ELSQ09))
                .ForMember(des => des.Question10, opt => opt.MapFrom(src => src.ELSQ10))
                .ForMember(des => des.Question11, opt => opt.MapFrom(src => src.ELSQ11))
                .ForMember(des => des.Question12, opt => opt.MapFrom(src => src.ELSQ12))
                .ForMember(des => des.Question13, opt => opt.MapFrom(src => src.ELSQ13))
                .ForMember(des => des.Question14, opt => opt.MapFrom(src => src.ELSQ14))
                .ForMember(des => des.Question15, opt => opt.MapFrom(src => src.ELSQ15))
                .ForMember(des => des.Question16, opt => opt.MapFrom(src => src.ELSQ16))
                .ForMember(des => des.Question17, opt => opt.MapFrom(src => src.ELSQ17))
                .ForMember(des => des.Question18, opt => opt.MapFrom(src => src.ELSQ18))
                .ForMember(des => des.Question19, opt => opt.MapFrom(src => src.ELSQ19))
                .ForMember(des => des.Question20, opt => opt.MapFrom(src => src.ELSQ20))
                .ForMember(des => des.Question21, opt => opt.MapFrom(src => src.ELSQ21))
                .ForMember(des => des.Question22, opt => opt.MapFrom(src => src.ELSQ22))
                .ForMember(des => des.Question23, opt => opt.MapFrom(src => src.ELSQ23))
                .ForMember(des => des.Question24, opt => opt.MapFrom(src => src.ELSQ24))
                .ForMember(des => des.Question25, opt => opt.MapFrom(src => src.ELSQ25))
                // .ForMember(des => des.Question26, opt => opt.MapFrom(src => src.ELSQ26))
                // .ForMember(des => des.Question27, opt => opt.MapFrom(src => src.ELSQ27))
                // .ForMember(des => des.Question28, opt => opt.MapFrom(src => src.ELSQ28))
                // .ForMember(des => des.Question29, opt => opt.MapFrom(src => src.ELSQ29))
                // .ForMember(des => des.Question30, opt => opt.MapFrom(src => src.ELSQ30))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => src.ELRCST))
                .ForMember(des => des.AttendDate, opt => opt.MapFrom(src => src.ELATDT))
                .ForMember(des => des.HealthCheckDate, opt => opt.MapFrom(src => src.ELHCDT))
                .ForMember(des => des.CheckPIC, opt => opt.MapFrom(src => src.ELCPIC))
                .ForMember(des => des.Remarks, opt => opt.MapFrom(src => src.ELREMA));
            CreateMap<LebaranDto, EHAL>()
                .ForMember(des => des.ELEMNO, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(des => des.ELEMNA, opt => opt.MapFrom(src => src.EmployeeName))
                .ForMember(des => des.ELBRNO, opt => opt.MapFrom(src => src.Location))
                .ForMember(des => des.ELRFID, opt => opt.MapFrom(src => src.RFID))
                .ForMember(des => des.ELBTCT, opt => opt.MapFrom(src => src.BirthCity))
                .ForMember(des => des.ELBTDT, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(des => des.ELEKTP, opt => opt.MapFrom(src => src.EKTP))
                .ForMember(des => des.ELDENA, opt => opt.MapFrom(src => src.Department))
                .ForMember(des => des.ELTRDT, opt => opt.MapFrom(src => src.FillDate))
                .ForMember(des => des.ELSQ01, opt => opt.MapFrom(src => src.Question01))
                .ForMember(des => des.ELSQ02, opt => opt.MapFrom(src => src.Question02))
                .ForMember(des => des.ELSQ03, opt => opt.MapFrom(src => src.Question03))
                .ForMember(des => des.ELSQ04, opt => opt.MapFrom(src => src.Question04))
                .ForMember(des => des.ELSQ05, opt => opt.MapFrom(src => src.Question05))
                .ForMember(des => des.ELSQ06, opt => opt.MapFrom(src => src.Question06))
                .ForMember(des => des.ELSQ07, opt => opt.MapFrom(src => src.Question07))
                .ForMember(des => des.ELSQ08, opt => opt.MapFrom(src => src.Question08))
                .ForMember(des => des.ELSQ09, opt => opt.MapFrom(src => src.Question09))
                .ForMember(des => des.ELSQ10, opt => opt.MapFrom(src => src.Question10))
                .ForMember(des => des.ELSQ11, opt => opt.MapFrom(src => src.Question11))
                .ForMember(des => des.ELSQ12, opt => opt.MapFrom(src => src.Question12))
                .ForMember(des => des.ELSQ13, opt => opt.MapFrom(src => src.Question13))
                .ForMember(des => des.ELSQ14, opt => opt.MapFrom(src => src.Question14))
                .ForMember(des => des.ELSQ15, opt => opt.MapFrom(src => src.Question15))
                .ForMember(des => des.ELSQ16, opt => opt.MapFrom(src => src.Question16))
                .ForMember(des => des.ELSQ17, opt => opt.MapFrom(src => src.Question17))
                .ForMember(des => des.ELSQ18, opt => opt.MapFrom(src => src.Question18))
                .ForMember(des => des.ELSQ19, opt => opt.MapFrom(src => src.Question19))
                .ForMember(des => des.ELSQ20, opt => opt.MapFrom(src => src.Question20))
                .ForMember(des => des.ELSQ21, opt => opt.MapFrom(src => src.Question21))
                .ForMember(des => des.ELSQ22, opt => opt.MapFrom(src => src.Question22))
                .ForMember(des => des.ELSQ23, opt => opt.MapFrom(src => src.Question23))
                .ForMember(des => des.ELSQ24, opt => opt.MapFrom(src => src.Question24))
                .ForMember(des => des.ELSQ25, opt => opt.MapFrom(src => src.Question25))
                // .ForMember(des => des.ELSQ26, opt => opt.MapFrom(src => src.Question26))
                // .ForMember(des => des.ELSQ27, opt => opt.MapFrom(src => src.Question27))
                // .ForMember(des => des.ELSQ28, opt => opt.MapFrom(src => src.Question28))
                // .ForMember(des => des.ELSQ29, opt => opt.MapFrom(src => src.Question29))
                // .ForMember(des => des.ELSQ30, opt => opt.MapFrom(src => src.Question30))
                .ForMember(des => des.ELRCST, opt => opt.MapFrom(src => src.Status))
                .ForMember(des => des.ELATDT, opt => opt.MapFrom(src => src.AttendDate))
                .ForMember(des => des.ELHCDT, opt => opt.MapFrom(src => src.HealthCheckDate))
                .ForMember(des => des.ELCPIC, opt => opt.MapFrom(src => src.CheckPIC))
                .ForMember(des => des.ELREMA, opt => opt.MapFrom(src => src.Remarks));

            CreateMap<EHAL, LebaranXLSDto>()
                .ForMember(des => des.EmployeeId, opt => opt.MapFrom(src => src.ELEMNO))
                .ForMember(des => des.EmployeeName, opt => opt.MapFrom(src => src.ELEMNA))
                .ForMember(des => des.Department, opt => opt.MapFrom(src => src.ELDENA))
                .ForMember(des => des.FillDate, opt => opt.MapFrom(src => src.ELTRDT))
                .ForMember(des => des.Question01, opt => opt.MapFrom(src => src.ELSQ01))
                .ForMember(des => des.Question02, opt => opt.MapFrom(src => src.ELSQ02))
                .ForMember(des => des.Question03, opt => opt.MapFrom(src => src.ELSQ03))
                .ForMember(des => des.Question04, opt => opt.MapFrom(src => src.ELSQ04))
                .ForMember(des => des.Question05, opt => opt.MapFrom(src => src.ELSQ05))
                .ForMember(des => des.Question06, opt => opt.MapFrom(src => src.ELSQ06))
                .ForMember(des => des.Question07, opt => opt.MapFrom(src => src.ELSQ07))
                .ForMember(des => des.Question08, opt => opt.MapFrom(src => src.ELSQ08))
                .ForMember(des => des.Question09, opt => opt.MapFrom(src => src.ELSQ09))
                .ForMember(des => des.Question10, opt => opt.MapFrom(src => src.ELSQ10))
                .ForMember(des => des.Question11, opt => opt.MapFrom(src => src.ELSQ11))
                .ForMember(des => des.Question12, opt => opt.MapFrom(src => src.ELSQ12))
                .ForMember(des => des.Question13, opt => opt.MapFrom(src => src.ELSQ13))
                .ForMember(des => des.Question14, opt => opt.MapFrom(src => src.ELSQ14))
                .ForMember(des => des.Question15, opt => opt.MapFrom(src => src.ELSQ15))
                .ForMember(des => des.Question16, opt => opt.MapFrom(src => src.ELSQ16))
                .ForMember(des => des.Question17, opt => opt.MapFrom(src => src.ELSQ17))
                .ForMember(des => des.Question18, opt => opt.MapFrom(src => src.ELSQ18))
                .ForMember(des => des.Question19, opt => opt.MapFrom(src => src.ELSQ19))
                .ForMember(des => des.Question20, opt => opt.MapFrom(src => src.ELSQ20))
                .ForMember(des => des.Question21, opt => opt.MapFrom(src => src.ELSQ21))
                .ForMember(des => des.Question22, opt => opt.MapFrom(src => src.ELSQ22))
                .ForMember(des => des.Question23, opt => opt.MapFrom(src => src.ELSQ23))
                .ForMember(des => des.Question24, opt => opt.MapFrom(src => src.ELSQ24))
                .ForMember(des => des.Question25, opt => opt.MapFrom(src => src.ELSQ25))
                // .ForMember(des => des.Question26, opt => opt.MapFrom(src => src.ELSQ26))
                // .ForMember(des => des.Question27, opt => opt.MapFrom(src => src.ELSQ27))
                // .ForMember(des => des.Question28, opt => opt.MapFrom(src => src.ELSQ28))
                // .ForMember(des => des.Question29, opt => opt.MapFrom(src => src.ELSQ29))
                // .ForMember(des => des.Question30, opt => opt.MapFrom(src => src.ELSQ30))
                .ForMember(des => des.StatusDescription, opt => opt.MapFrom(src => src.ELRCST == 0 ? "Boleh Bekerja" : "Check Kesehatan"))
                .ForMember(des => des.AttendDate, opt => opt.MapFrom(src => src.ELATDT))
                .ForMember(des => des.HealthCheckDate, opt => opt.MapFrom(src => src.ELHCDT))
                .ForMember(des => des.CheckPIC, opt => opt.MapFrom(src => src.ELCPIC))
                .ForMember(des => des.Remarks, opt => opt.MapFrom(src => src.ELREMA));

            CreateMap<SCMI, PartNumberType>()
                .ForMember(des => des.CustomerId, opt => opt.MapFrom(src => src.CXCUNO))
                .ForMember(des => des.ModelName, opt => opt.MapFrom(src => src.CXITNO))
                .ForMember(des => des.PartNumber, opt => opt.MapFrom(src => src.CXCUIT))
                .ForMember(des => des.PartDescription, opt => opt.MapFrom(src => src.CXITNA))
                .ForMember(des => des.Remarks, opt => opt.MapFrom(src => src.CXREMA))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => src.CXRCST))
                .ForMember(des => des.Created, opt => opt.MapFrom(src => CommonMethod.NumericToDateNullable(src.CXCRDT)))
                .ForMember(des => des.CreatedBy, opt => opt.MapFrom(src => src.CXCRUS))
                .ForMember(des => des.Updated, opt => opt.MapFrom(src => CommonMethod.NumericToDateNullable(src.CXCHDT)))
                .ForMember(des => des.UpdatedBy, opt => opt.MapFrom(src => src.CXCHUS));
            CreateMap<PartTypeInput, SCMI>()
                .ForMember(des => des.CXCONO, opt => opt.MapFrom(src => "AMG"))
                .ForMember(des => des.CXBRNO, opt => opt.MapFrom(src => "CKP"))
                .ForMember(des => des.CXCUNO, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(des => des.CXITNO, opt => opt.MapFrom(src => src.ModelName))
                .ForMember(des => des.CXCUIT, opt => opt.MapFrom(src => src.PartNumber))
                .ForMember(des => des.CXITNA, opt => opt.MapFrom(src => src.PartDescription))
                .ForMember(des => des.CXITN2, opt => opt.MapFrom(src => ""))
                .ForMember(des => des.CXUNNO, opt => opt.MapFrom(src => ""))
                .ForMember(des => des.CXREMA, opt => opt.MapFrom(src => src.Remarks))
                .ForMember(des => des.CXOTRM, opt => opt.MapFrom(src => ""))
                .ForMember(des => des.CXALCN, opt => opt.MapFrom(src => src.AlcNumber))
                .ForMember(des => des.CXEONO, opt => opt.MapFrom(src => src.EoNumber))
                .ForMember(des => des.CXSYST, opt => opt.MapFrom(src => "10"))
                .ForMember(des => des.CXSTAT, opt => opt.MapFrom(src => "10"))
                .ForMember(des => des.CXRCST, opt => opt.MapFrom(src => 1))
                .ForMember(des => des.CXCRDT, opt => opt.MapFrom(src => CommonMethod.DateToNumeric(DateTime.Now)))
                .ForMember(des => des.CXCRTM, opt => opt.MapFrom(src => CommonMethod.TimeToNumeric(DateTime.Now)))
                .ForMember(des => des.CXCHDT, opt => opt.MapFrom(src => CommonMethod.DateToNumeric(DateTime.Now)))
                .ForMember(des => des.CXCHTM, opt => opt.MapFrom(src => CommonMethod.TimeToNumeric(DateTime.Now)));

            // mapper for pallet type data object
            // RAW to DTO
            CreateMap<IPTY, PalletTypeDto>()
            .ForMember(des => des.Company, opt => opt.MapFrom(src => src.HSCONO))
            .ForMember(des => des.Branch, opt => opt.MapFrom(src => src.HSBRNO))
            .ForMember(des => des.PalletType, opt => opt.MapFrom(src => src.HSPETY))
            .ForMember(des => des.PalletName, opt => opt.MapFrom(src => src.HSPENA))
            .ForMember(des => des.PalletApp, opt => opt.MapFrom(src => src.HSPLAP))
            .ForMember(des => des.PalletLength, opt => opt.MapFrom(src => src.HSPELN))
            .ForMember(des => des.LengthUm, opt => opt.MapFrom(src => src.HSLNUM))
            .ForMember(des => des.PalletWidth, opt => opt.MapFrom(src => src.HSPEWD))
            .ForMember(des => des.WidthUm, opt => opt.MapFrom(src => src.HSWDUM))
            .ForMember(des => des.PalletHeight, opt => opt.MapFrom(src => src.HSPEHG))
            .ForMember(des => des.HeightUm, opt => opt.MapFrom(src => src.HSHGUM))
            .ForMember(des => des.PalletWeight, opt => opt.MapFrom(src => src.HSPEWG))
            .ForMember(des => des.WeightUm, opt => opt.MapFrom(src => src.HSWGUM))
            .ForMember(des => des.PalletColor, opt => opt.MapFrom(src => src.HSCLNO))
            .ForMember(des => des.PalletCurrency, opt => opt.MapFrom(src => src.HSCYNO))
            .ForMember(des => des.PalletPrice, opt => opt.MapFrom(src => src.HSUNPR))
            .ForMember(des => des.CNCode, opt => opt.MapFrom(src => src.HSCNNO))
            .ForMember(des => des.Remark, opt => opt.MapFrom(src => src.HSREMA))
            .ForMember(des => des.PalletCodification, opt => opt.MapFrom(src => src.HSRETY))
            .ForMember(des => des.MaterialType, opt => opt.MapFrom(src => src.HSMATY))
            .ForMember(des => des.Flag1, opt => opt.MapFrom(src => src.HSFL01))
            // .ForMember(des => des.PackingFlag, opt => opt.MapFrom(src => src.HSPKFL))
            .ForMember(des => des.CarryInFlag, opt => opt.MapFrom(src => src.HSCIFL))
            .ForMember(des => des.CarryOutFlag, opt => opt.MapFrom(src => src.HSCOFL))
            // .ForMember(des => des.RunOutFlag, opt => opt.MapFrom(src => src.HSROFL))
            .ForMember(des => des.SystemFlag, opt => opt.MapFrom(src => src.HSSYST))
            .ForMember(des => des.StatFlag, opt => opt.MapFrom(src => src.HSSTAT))
            .ForMember(des => des.RecordStatus, opt => opt.MapFrom(src => src.HSRCST))
            .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => src.HSCRDT))
            .ForMember(des => des.CreatedTime, opt => opt.MapFrom(src => src.HSCRTM))
            .ForMember(des => des.CreatedUser, opt => opt.MapFrom(src => src.HSCRUS))
            .ForMember(des => des.ChangedDate, opt => opt.MapFrom(src => src.HSCHDT))
            .ForMember(des => des.ChangedTime, opt => opt.MapFrom(src => src.HSCHTM))
            .ForMember(des => des.ChangedUser, opt => opt.MapFrom(src => src.HSCHUS));

            // DTO to RAW
            CreateMap<PalletTypeDto, IPTY>()
           .ForMember(des => des.HSCONO, opt => opt.MapFrom(src => src.Company))
           .ForMember(des => des.HSBRNO, opt => opt.MapFrom(src => src.Branch))
           .ForMember(des => des.HSPETY, opt => opt.MapFrom(src => src.PalletType))
           .ForMember(des => des.HSPENA, opt => opt.MapFrom(src => src.PalletName))
           .ForMember(des => des.HSPLAP, opt => opt.MapFrom(src => src.PalletApp))
           .ForMember(des => des.HSPELN, opt => opt.MapFrom(src => src.PalletLength))
           .ForMember(des => des.HSLNUM, opt => opt.MapFrom(src => src.LengthUm))
           .ForMember(des => des.HSPEWD, opt => opt.MapFrom(src => src.PalletWidth))
           .ForMember(des => des.HSWDUM, opt => opt.MapFrom(src => src.WidthUm))
           .ForMember(des => des.HSPEHG, opt => opt.MapFrom(src => src.PalletHeight))
           .ForMember(des => des.HSHGUM, opt => opt.MapFrom(src => src.HeightUm))
           .ForMember(des => des.HSPEWG, opt => opt.MapFrom(src => src.PalletWeight))
           .ForMember(des => des.HSWGUM, opt => opt.MapFrom(src => src.WeightUm))
           .ForMember(des => des.HSCLNO, opt => opt.MapFrom(src => src.PalletColor))
           .ForMember(des => des.HSCYNO, opt => opt.MapFrom(src => src.PalletCurrency))
           .ForMember(des => des.HSUNPR, opt => opt.MapFrom(src => src.PalletPrice))
           .ForMember(des => des.HSCNNO, opt => opt.MapFrom(src => src.CNCode))
           .ForMember(des => des.HSREMA, opt => opt.MapFrom(src => src.Remark))
           .ForMember(des => des.HSRETY, opt => opt.MapFrom(src => src.PalletCodification))
           .ForMember(des => des.HSMATY, opt => opt.MapFrom(src => src.MaterialType))
           .ForMember(des => des.HSFL01, opt => opt.MapFrom(src => src.Flag1))
           // .ForMember(des => des.HSPKFL, opt => opt.MapFrom(src => src.PackingFlag))
           .ForMember(des => des.HSCIFL, opt => opt.MapFrom(src => src.CarryInFlag))
           .ForMember(des => des.HSCOFL, opt => opt.MapFrom(src => src.CarryOutFlag))
           // .ForMember(des => des.HSROFL, opt => opt.MapFrom(src => src.RunOutFlag))
           .ForMember(des => des.HSSYST, opt => opt.MapFrom(src => src.SystemFlag))
           .ForMember(des => des.HSSTAT, opt => opt.MapFrom(src => src.StatFlag))
           .ForMember(des => des.HSRCST, opt => opt.MapFrom(src => src.RecordStatus))
           .ForMember(des => des.HSCRDT, opt => opt.MapFrom(src => src.CreatedDate))
           .ForMember(des => des.HSCRTM, opt => opt.MapFrom(src => src.CreatedTime))
           .ForMember(des => des.HSCRUS, opt => opt.MapFrom(src => src.CreatedUser))
           .ForMember(des => des.HSCHDT, opt => opt.MapFrom(src => src.ChangedDate))
           .ForMember(des => des.HSCHTM, opt => opt.MapFrom(src => src.ChangedTime))
           .ForMember(des => des.HSCHUS, opt => opt.MapFrom(src => src.ChangedUser));


            //buat step 3 disini
            CreateMap<MDF0, Mdf0Dto>()
            .ForMember(des => des.TransactionId, opt => opt.MapFrom(src => src.DDTRID))
            .ForMember(des => des.DefectGroup, opt => opt.MapFrom(src => src.DDDFGR))
            .ForMember(des => des.Remark, opt => opt.MapFrom(src => src.DDREMA))
            .ForMember(des => des.RecordStatus, opt => opt.MapFrom(src => src.DDRCST))
            .ForMember(des => des.CreateTime, opt => opt.MapFrom(src => src.DDCRTM))
            .ForMember(des => des.CreateUser, opt => opt.MapFrom(src => src.DDCRUS))
            .ForMember(des => des.ChangeTime, opt => opt.MapFrom(src => src.DDCHTM))
            .ForMember(des => des.ChangeUser, opt => opt.MapFrom(src => src.DDCHUS))
            .ForMember(des => des.RecordStatusText, opt => opt.MapFrom(src => src.DDRCST == 0 ? "Inactive" : "Active"));

            CreateMap<Mdf0Dto, MDF0>()
            .ForMember(des => des.DDTRID, opt => opt.MapFrom(src => src.TransactionId))
            .ForMember(des => des.DDDFGR, opt => opt.MapFrom(src => src.DefectGroup))
            .ForMember(des => des.DDREMA, opt => opt.MapFrom(src => src.Remark))
            .ForMember(des => des.DDRCST, opt => opt.MapFrom(src => src.RecordStatus))
            .ForMember(des => des.DDCRTM, opt => opt.MapFrom(src => src.CreateTime))
            .ForMember(des => des.DDCRUS, opt => opt.MapFrom(src => src.CreateUser))
            .ForMember(des => des.DDCHTM, opt => opt.MapFrom(src => src.ChangeTime))
            .ForMember(des => des.DDCHUS, opt => opt.MapFrom(src => src.ChangeUser));

            CreateMap<ZVAR, DefinitionVarDto>()
            .ForMember(des => des.Name, opt => opt.MapFrom(src => src.ZRVANA))
            .ForMember(des => des.Value, opt => opt.MapFrom(src => src.ZRVAVL));

            CreateMap<DefinitionVarDto, ZVAR>()
            .ForMember(des => des.ZRVANA, opt => opt.MapFrom(src => src.Name))
            .ForMember(des => des.ZRVAVL, opt => opt.MapFrom(src => src.Value));

            CreateMap<GCT2, GlobalCommonText2>()
            .ForMember(des => des.Name, opt => opt.MapFrom(src => src.CBKYNA))
            .ForMember(des => des.Value, opt => opt.MapFrom(src => src.CBKYNO));
            // .ForMember(des => des.Type, opt => opt.MapFrom(src => src.CBTBNO));

            CreateMap<GlobalCommonText2, GCT2>()
            .ForMember(des => des.CBKYNA, opt => opt.MapFrom(src => src.Name))
            .ForMember(des => des.CBKYNO, opt => opt.MapFrom(src => src.Value));
            // .ForMember(des => des.CBTBNO, opt => opt.MapFrom(src => src.Type));

            CreateMap<GCUR, CurrencyDefinitionDto>()
            .ForMember(des => des.Name, opt => opt.MapFrom(src => src.GGCYNA))
            .ForMember(des => des.Value, opt => opt.MapFrom(src => src.GGCYNO));

            CreateMap<CurrencyDefinitionDto, GCUR>()
            .ForMember(des => des.GGCYNA, opt => opt.MapFrom(src => src.Name))
            .ForMember(des => des.GGCYNO, opt => opt.MapFrom(src => src.Value));

            CreateMap<IUOM, MeasurementDefinitionDto>()
            .ForMember(des => des.Name, opt => opt.MapFrom(src => src.HUUMNA))
            .ForMember(des => des.Value, opt => opt.MapFrom(src => src.HUUMNO));

            CreateMap<MeasurementDefinitionDto, IUOM>()
            .ForMember(des => des.HUUMNA, opt => opt.MapFrom(src => src.Name))
            .ForMember(des => des.HUUMNO, opt => opt.MapFrom(src => src.Value));

            CreateMap<IWGR, WarehouseGroupDto>()
            .ForMember(des => des.Company, opt => opt.MapFrom(src => src.HVCONO))
            .ForMember(des => des.Code, opt => opt.MapFrom(src => src.HVWHGR))
            .ForMember(des => des.Name, opt => opt.MapFrom(src => src.HVGRNA))
            .ForMember(des => des.Branch, opt => opt.MapFrom(src => src.HVBRNO))
            .ForMember(des => des.Remark, opt => opt.MapFrom(src => src.HVREMA))
            .ForMember(des => des.System, opt => opt.MapFrom(src => src.HVSYST))
            .ForMember(des => des.Status, opt => opt.MapFrom(src => src.HVSTAT))
            .ForMember(des => des.RecordStatus, opt => opt.MapFrom(src => src.HVRCST))
            .ForMember(des => des.CreatedTime, opt => opt.MapFrom(src => src.HVCRTT))
            .ForMember(des => des.CreatedUser, opt => opt.MapFrom(src => src.HVCRUS))
            .ForMember(des => des.ChangedTime, opt => opt.MapFrom(src => src.HVCHTT))
            .ForMember(des => des.ChangedUser, opt => opt.MapFrom(src => src.HVCHUS));

            CreateMap<WarehouseGroupDto, IWGR>()
            .ForMember(des => des.HVCONO, opt => opt.MapFrom(src => src.Company))
            .ForMember(des => des.HVWHGR, opt => opt.MapFrom(src => src.Code))
            .ForMember(des => des.HVGRNA, opt => opt.MapFrom(src => src.Name))
            .ForMember(des => des.HVBRNO, opt => opt.MapFrom(src => src.Branch))
            .ForMember(des => des.HVREMA, opt => opt.MapFrom(src => src.Remark))
            .ForMember(des => des.HVSYST, opt => opt.MapFrom(src => src.System))
            .ForMember(des => des.HVSTAT, opt => opt.MapFrom(src => src.Status))
            .ForMember(des => des.HVRCST, opt => opt.MapFrom(src => src.RecordStatus))
            .ForMember(des => des.HVCRTT, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(des => des.HVCRUS, opt => opt.MapFrom(src => src.CreatedUser))
            .ForMember(des => des.HVCHTT, opt => opt.MapFrom(src => src.ChangedTime))
            .ForMember(des => des.HVCHUS, opt => opt.MapFrom(src => src.ChangedUser));

            CreateMap<IWHS, WarehouseDto>()
            .ForMember(des => des.Code, opt => opt.MapFrom(src => src.HWWHNO))
            .ForMember(des => des.Name, opt => opt.MapFrom(src => src.HWWHNA))
            .ForMember(des => des.Branch, opt => opt.MapFrom(src => src.HWBRNO))
            .ForMember(des => des.Nickname, opt => opt.MapFrom(src => src.HWNICK))
            .ForMember(des => des.Address, opt => opt.MapFrom(src => src.HWADNO))
            .ForMember(des => des.Type, opt => opt.MapFrom(src => src.HWWHTY))
            .ForMember(des => des.Group, opt => opt.MapFrom(src => src.HWWHGR))
            .ForMember(des => des.CarryOutFlag, opt => opt.MapFrom(src => src.HWCOFL))
            .ForMember(des => des.StocktakingFlag, opt => opt.MapFrom(src => src.HWSCFL))
            .ForMember(des => des.DepartmentCode, opt => opt.MapFrom(src => src.HWOGNO))
            .ForMember(des => des.ProfitCode, opt => opt.MapFrom(src => src.HWPCNO))
            .ForMember(des => des.CostCenter, opt => opt.MapFrom(src => src.HWCCNO))
            .ForMember(des => des.DocumentCode, opt => opt.MapFrom(src => src.HWDFWH))
            .ForMember(des => des.PoliceNumber, opt => opt.MapFrom(src => src.HWPIMF))
            .ForMember(des => des.FifoFlag, opt => opt.MapFrom(src => src.HWFIFO))
            .ForMember(des => des.FifoDays, opt => opt.MapFrom(src => src.HWFDAY))
            .ForMember(des => des.TransferModelFlag, opt => opt.MapFrom(src => src.HWTRMV))
            .ForMember(des => des.PalletGroup, opt => opt.MapFrom(src => src.HWWHPL))
            .ForMember(des => des.QualityFlag, opt => opt.MapFrom(src => src.HWQNPF))
            .ForMember(des => des.Remark, opt => opt.MapFrom(src => src.HWREMA))
            .ForMember(des => des.System, opt => opt.MapFrom(src => src.HWSYST))
            .ForMember(des => des.Status, opt => opt.MapFrom(src => src.HWSTAT))
            .ForMember(des => des.RecordStatus, opt => opt.MapFrom(src => src.HWRCST))
            .ForMember(des => des.CreatedTime, opt => opt.MapFrom(src => src.HWCRTT))
            .ForMember(des => des.CreatedUser, opt => opt.MapFrom(src => src.HWCRUS))
            .ForMember(des => des.ChangedTime, opt => opt.MapFrom(src => src.HWCHTT))
            .ForMember(des => des.ChangedUser, opt => opt.MapFrom(src => src.HWCHUS));

            CreateMap<WarehouseDto, IWHS>()
            .ForMember(des => des.HWWHNO, opt => opt.MapFrom(src => src.Code))
            .ForMember(des => des.HWWHNA, opt => opt.MapFrom(src => src.Name))
            .ForMember(des => des.HWNICK, opt => opt.MapFrom(src => src.Nickname))
            .ForMember(des => des.HWBRNO, opt => opt.MapFrom(src => src.Branch))
            .ForMember(des => des.HWADNO, opt => opt.MapFrom(src => src.Address))
            .ForMember(des => des.HWWHTY, opt => opt.MapFrom(src => src.Type))
            .ForMember(des => des.HWWHGR, opt => opt.MapFrom(src => src.Group))
            .ForMember(des => des.HWCOFL, opt => opt.MapFrom(src => src.CarryOutFlag))
            .ForMember(des => des.HWSCFL, opt => opt.MapFrom(src => src.StocktakingFlag))
            .ForMember(des => des.HWOGNO, opt => opt.MapFrom(src => src.DepartmentCode))
            .ForMember(des => des.HWPCNO, opt => opt.MapFrom(src => src.ProfitCode))
            .ForMember(des => des.HWCCNO, opt => opt.MapFrom(src => src.CostCenter))
            .ForMember(des => des.HWDFWH, opt => opt.MapFrom(src => src.DocumentCode))
            .ForMember(des => des.HWPIMF, opt => opt.MapFrom(src => src.PoliceNumber))
            .ForMember(des => des.HWFIFO, opt => opt.MapFrom(src => src.FifoFlag))
            .ForMember(des => des.HWFDAY, opt => opt.MapFrom(src => src.FifoDays))
            .ForMember(des => des.HWTRMV, opt => opt.MapFrom(src => src.TransferModelFlag))
            .ForMember(des => des.HWWHPL, opt => opt.MapFrom(src => src.PalletGroup))
            .ForMember(des => des.HWQNPF, opt => opt.MapFrom(src => src.QualityFlag))
            .ForMember(des => des.HWREMA, opt => opt.MapFrom(src => src.Remark))
            .ForMember(des => des.HWSYST, opt => opt.MapFrom(src => src.System))
            .ForMember(des => des.HWSTAT, opt => opt.MapFrom(src => src.Status))
            .ForMember(des => des.HWRCST, opt => opt.MapFrom(src => src.RecordStatus))
            .ForMember(des => des.HWCRTT, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(des => des.HWCRUS, opt => opt.MapFrom(src => src.CreatedUser))
            .ForMember(des => des.HWCHTT, opt => opt.MapFrom(src => src.ChangedTime))
            .ForMember(des => des.HWCHUS, opt => opt.MapFrom(src => src.ChangedUser));

                CreateMap<MDF1, Mdf1Dto>()
                .ForMember(des => des.Company, opt => opt.MapFrom(src => src.DECONO))
                .ForMember(des => des.Branch, opt => opt.MapFrom(src => src.DEBRNO))
                .ForMember(des => des.DefectCode, opt => opt.MapFrom(src => src.DEDFNO))
                .ForMember(des => des.DefectName, opt => opt.MapFrom(src => src.DEDFNA))
                .ForMember(des => des.IdGroup, opt => opt.MapFrom(src => src.DEGRID))
                .ForMember(des => des.DefectType, opt => opt.MapFrom(src => src.DEDPGR))
                .ForMember(des => des.DefectGroup1, opt => opt.MapFrom(src => src.DEDFG1))
                .ForMember(des => des.DefectGroup2, opt => opt.MapFrom(src => src.DEDFG2))
                .ForMember(des => des.Remark, opt => opt.MapFrom(src => src.DEREMA))
                .ForMember(des => des.RecordStatus, opt => opt.MapFrom(src => src.DERCST))
                .ForMember(des => des.CreateDate, opt => opt.MapFrom(src => src.DECRDT))
                .ForMember(des => des.CreateTime, opt => opt.MapFrom(src => src.DECRTM))
                .ForMember(des => des.CreateUser, opt => opt.MapFrom(src => src.DECRUS))
                .ForMember(des => des.ChangeDate, opt => opt.MapFrom(src => src.DECHDT))
                .ForMember(des => des.ChangeTime, opt => opt.MapFrom(src => src.DECHTM))
                .ForMember(des => des.ChangeUser, opt => opt.MapFrom(src => src.DECHUS))
                .ForMember(des => des.RecordStatusText, opt => opt.MapFrom(src => src.DERCST == 0 ? "Inactive" : "Active" ));

                CreateMap<Mdf1Dto, MDF1>()
                .ForMember(des => des.DECONO, opt => opt.MapFrom(src => src.Company))
                .ForMember(des => des.DEBRNO, opt => opt.MapFrom(src => src.Branch))
                .ForMember(des => des.DEDFNO, opt => opt.MapFrom(src => src.DefectCode))
                .ForMember(des => des.DEDFNA, opt => opt.MapFrom(src => src.DefectName))
                .ForMember(des => des.DEGRID, opt => opt.MapFrom(src => src.IdGroup))
                .ForMember(des => des.DEDPGR, opt => opt.MapFrom(src => src.DefectType))
                .ForMember(des => des.DEDFG1, opt => opt.MapFrom(src => src.DefectGroup1))
                .ForMember(des => des.DEDFG2, opt => opt.MapFrom(src => src.DefectGroup2))
                .ForMember(des => des.DEREMA, opt => opt.MapFrom(src => src.Remark))
                .ForMember(des => des.DERCST, opt => opt.MapFrom(src => src.RecordStatus))
                .ForMember(des => des.DECRDT, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(des => des.DECRTM, opt => opt.MapFrom(src => src.CreateTime))
                .ForMember(des => des.DECRUS, opt => opt.MapFrom(src => src.CreateUser))
                .ForMember(des => des.DECHDT, opt => opt.MapFrom(src => src.ChangeDate))
                .ForMember(des => des.DECHTM, opt => opt.MapFrom(src => src.ChangeTime))
                .ForMember(des => des.DECHUS, opt => opt.MapFrom(src => src.ChangeUser));


        }

        public class PhotoResolver : IMemberValueResolver<object, object, string, string>
        {
            public string Resolve(object source, object destination, string sourceMember, string destinationMember, ResolutionContext context)
            {
                string[] member = sourceMember.Split("|");
                string pictureUrl = "http://172.18.45.174/APRISE/Photo/";
                switch (member[0])
                {
                    case "JKT":
                        pictureUrl += "10000";
                        break;
                    case "SDA":
                        pictureUrl += "20000";
                        break;
                    case "CKP":
                        pictureUrl += "30000";
                        break;
                    case "CFG":
                        pictureUrl += "40000";
                        break;
                }
                pictureUrl += "/" + member[1] + ".jpg";

                return pictureUrl;
            }
        }
        public class AgeResolver : IMemberValueResolver<object, object, DateTime?, int>
        {
            public int Resolve(object source, object destination, DateTime? sourceMember, int destinationMember, ResolutionContext context)
            {
                if (!sourceMember.HasValue)
                    return 0;

                var today = DateTime.Today;
                var age = today.Year - sourceMember.Value.Year;
                if (sourceMember.Value.Date > today.AddYears(-age))
                    age--;

                return age;
            }
        }
    }
}