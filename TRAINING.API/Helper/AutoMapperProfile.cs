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
                .ForMember(des => des.Updated ,opt => opt.MapFrom(src => CommonMethod.NumericToDateNullable(src.CXCHDT)))
                .ForMember(des => des.UpdatedBy ,opt => opt.MapFrom(src => src.CXCHUS));
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
        }

        public class PhotoResolver : IMemberValueResolver<object, object, string, string>
        {
            public string Resolve(object source, object destination, string sourceMember, string destinationMember, ResolutionContext context)
            {
                string[] member = sourceMember.Split("|");
                string pictureUrl = "http://172.18.45.174/APRISE/Photo/";
                switch(member[0])
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