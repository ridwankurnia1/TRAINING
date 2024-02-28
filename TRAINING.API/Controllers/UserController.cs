using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TRAINING.API.Data;
using TRAINING.API.Helper;
using AutoMapper;
using TRAINING.API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TRAINING.API.Model;
using TRAINING.API.Schema.Queries;

namespace TRAINING.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly ISalesRepository _salesRepo;
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UserController(IUserRepository repo, ISalesRepository salesRepo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _salesRepo = salesRepo;
        }

        [HttpGet("employee")]
        public async Task<IActionResult> GetEmployee([FromQuery]InventoryParams prm)
        {
            var employee = await _repo.GetListEmmployee(prm);
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(employee);
            Response.AddPagination(employee.CurrentPage, employee.PageSize, employee.TotalCount, employee.TotalPages);

            return Ok(result);
        }
        [HttpGet("employee/{nik}")]
        public async Task<IActionResult> GetEmployeeByNik(string nik)
        {
            var employee = await _repo.GetEmployee(nik);
            if (employee == null)
                return NoContent();
            
            var dataToReturn = _mapper.Map<EmployeeDto>(employee);
            return Ok(dataToReturn);
        }
        [HttpPost("employee")]
        public async Task<IActionResult> AddEmployee(EmployeeDto data)
        {
            var employee = await _repo.GetEmployee(data.Nik);
            if (employee != null)
            {

                
                // employee.EMEGNO = data.Grade;
                // employee.EMDENO = data.DepartmentId;
                // employee.EMCHDT = CommonMethod.DateToNumeric(DateTime.Now);
                // employee.EMCHTM = CommonMethod.TimeToNumeric(DateTime.Now);

                // // hardcode emchus
                // employee.EMCHUS = "Ricky";

                // // add address and branch
                // employee.EMADR1 = data.Address;
                // employee.EMBRNO = data.Branch;
            }
            else
            {
                var emp = _mapper.Map<MEMP>(data);
                emp.EMCRUS = "Ricky";
                emp.EMCHUS = "Ricky";

                // hardcode cono
                emp.EMCONO = "AMG";

                _repo.Add<MEMP>(emp);
            }

            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Gagal menyimpan data");
        }        
        [HttpPut("employee/{nik}")]
        public async Task<IActionResult> EditEmployee(string nik, EmployeeDto data)
        {
            var employee = await _repo.GetEmployee(nik);
            if (employee == null){
                return BadRequest("Employee tidak ditemukan");
            }

            //Convert waktu angular ke timezone c#
            TimeZoneInfo timeZoneInfo; 
            DateTime dateTime ; 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); 
            dateTime = TimeZoneInfo.ConvertTime(data.BirthDate.Value, timeZoneInfo);

            employee.EMEMNA = data.Nama;
            employee.EMEGNO = data.Grade;
            employee.EMDENO = data.DepartmentId;
            employee.EMBTDT = CommonMethod.DateToNumeric(dateTime);
            employee.EMCHDT = CommonMethod.DateToNumeric(DateTime.Now);
            employee.EMCHTM = CommonMethod.TimeToNumeric(DateTime.Now);
            
            //Perlu login untuk mengambil nilai Employee Change User
            employee.EMCHUS = "Ricky";

            // add address and branch
            employee.EMADR1 = data.Address;
            employee.EMBRNO = data.Branch;

           
            if (await _repo.SaveAll())
                return Ok();
            
            throw new Exception("Gagal mengupdate data");
        }
        [HttpDelete("employee/{nik}")]
        public async Task<IActionResult> DeleteEmployee(string nik)
        {
            var employee = await _repo.GetEmployee(nik);
            if (employee == null)
                return BadRequest("Employee tidak ditemukan");
            
            // _repo.Delete<MEMP>(employee);
            employee.EMRCST = 0;
            employee.EMCHDT = CommonMethod.DateToNumeric(DateTime.Now);
            employee.EMCHTM = CommonMethod.TimeToNumeric(DateTime.Now);
            employee.EMCHUS = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            if (await _repo.SaveAll())
                return Ok();
            
            throw new Exception("Gagal mengupdate data");
        }
        
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdown([FromQuery]InventoryParams prm)
        {
            var gog1 = await _repo.GetOrganization();
            var mgrd = await _repo.GetListGrade();
            var brno = await _repo.GetListBranch();
            var ddl_org = _mapper.Map<IEnumerable<DropdownDto>>(gog1);
            var ddl_grd = _mapper.Map<IEnumerable<DropdownDto>>(mgrd);            
            var ddl_brno = _mapper.Map<IEnumerable<DropdownDto>>(brno);            

            return Ok(new {
                grade = ddl_grd,
                dept = ddl_org,
                branch = ddl_brno
            });
        }

        // [HttpGet("employee2")]
        // public async Task<IActionResult> GetEmployeeAPRISE([FromQuery]Params prm)
        // {
        //     var data = await _salesRepo.GetPartNumber("311050", "73450-T7Y -K100");
        //     var result = _mapper.Map<PartNumberType>(data);
        //     return Ok(result);
        // }
    }
}