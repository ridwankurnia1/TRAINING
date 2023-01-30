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
        public async Task<IActionResult> GetEmployee([FromQuery]Params prm)
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
                employee.EMEGNO = data.Grade;
                employee.EMDENO = data.DepartmentId;
                employee.EMCHDT = CommonMethod.DateToNumeric(DateTime.Now);
                employee.EMCHTM = CommonMethod.TimeToNumeric(DateTime.Now);
                employee.EMCHUS = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                var emp = _mapper.Map<MEMP>(data);
                emp.EMCRUS = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                emp.EMCHUS = User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
            if (employee == null)
                return BadRequest("Employee tidak ditemukan");
            
            employee.EMEMNA = data.Nama;
            employee.EMEGNO = data.Grade;
            employee.EMDENO = data.DepartmentId;
            employee.EMBTDT = CommonMethod.DateToNumeric(data.BirthDate.Value);
            employee.EMCHDT = CommonMethod.DateToNumeric(DateTime.Now);
            employee.EMCHTM = CommonMethod.TimeToNumeric(DateTime.Now);
            employee.EMCHUS = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
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
        public async Task<IActionResult> GetDropdown([FromQuery]Params prm)
        {
            var gog1 = await _repo.GetOrganization();
            var mgrd = await _repo.GetListGrade();
            var ddl_org = _mapper.Map<IEnumerable<DropdownDto>>(gog1);
            var ddl_grd = _mapper.Map<IEnumerable<DropdownDto>>(mgrd);            

            return Ok(new {
                grade = ddl_grd,
                dept = ddl_org
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