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

namespace TRAINING.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UserController(IUserRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;            
        }

        [HttpGet("employee")]
        public async Task<IActionResult> GetEmployee([FromQuery]Params prm)
        {
            var employee = await _repo.GetListEmmployee(prm);
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(employee);
            Response.AddPagination(employee.CurrentPage, employee.PageSize, employee.TotalCount, employee.TotalPages);

            return Ok(result);
        }

        [HttpGet("employee2")]
        public async Task<IActionResult> GetEmployeeAPRISE([FromQuery]Params prm)
        {
            var employee = await _repo.GetListEmmployeeAPRISE(prm);
            var org = await _repo.GetOrganization();

            var result = from m in employee
                         join g in org on m.ORGANIZATIONSTRUCTURE equals g.GOOGNO 
                         select new EmployeeDto() 
                         {
                            Nik = m.NIK,
                            Nama = m.NAME,
                            DepartmentId = m.ORGANIZATIONSTRUCTURE,
                            Department = g.GOOGNA,
                            Grade = m.GRADECODE
                         };

            return Ok(result);
        }
    }
}