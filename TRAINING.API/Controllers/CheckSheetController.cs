using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TRAINING.API.Data;
using TRAINING.API.Helper;
using TRAINING.API.ViewModel;
using TRAINING.API.Extensions;
using System.Collections.Generic;

namespace TRAINING.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class CheckSheetController : ControllerBase
    {
        private readonly ICheckSheetRepository _repo;
        private readonly IMapper _mapper;
        public CheckSheetController(ICheckSheetRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("employee/detail")]
        public async Task<IActionResult> GetEmployeeDetail([FromQuery]Params prm)
        {
            var data = await _repo.GetListEmployee(prm);
            var result = _mapper.Map<IEnumerable<LebaranDto>>(data);

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("employee/summary")]
        public async Task<IActionResult> GetEmployeeSummary([FromQuery]Params prm)
        {
            var data = await _repo.GetSummaryLebaran("CKP");
            return Ok(data);
        }        
        [AllowAnonymous]
        [HttpGet("employee")]
        public async Task<IActionResult> GetEmployee([FromQuery]Params prm)
        {
            var data = await _repo.GetListEmployeePaging(prm);
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(data);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("employee/{id}")]
        public async Task<IActionResult> GetEmployeeByNikOrRFID(string id)
        {
            string strNIK = string.Empty;
            string strRFID = string.Empty;

            if (id.Length == 10)
                strRFID = id;
            else
                strNIK = id;
            
            var emp = await _repo.GetEmployee(strNIK, strRFID);
            var result = _mapper.Map<LebaranDto>(emp);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPut("employee/{id}")]
        public async Task<IActionResult> EditEmployeeData(string id, LebaranDto obj)
        {
            var emp = await _repo.GetEmployee(id, "");
            if (emp == null)
                return BadRequest("Data karyawan tidak ditemukan");
            
            if (emp.ELTRDT.HasValue)
                return BadRequest("Anda sudah mengisi data pada tanggal " + emp.ELTRDT.Value.ToString("dd-MM-yyyy hh:mm"));
            
            emp.ELTRDT = DateTime.Now;
            emp.ELSQ01 = obj.Question01;
            emp.ELSQ02 = obj.Question02;
            emp.ELSQ03 = obj.Question03;
            emp.ELSQ04 = obj.Question04;
            emp.ELSQ05 = obj.Question05;
            emp.ELSQ06 = obj.Question06;
            emp.ELSQ07 = obj.Question07;
            emp.ELSQ08 = obj.Question08;
            emp.ELSQ09 = obj.Question09;
            emp.ELSQ10 = obj.Question10;
            emp.ELSQ11 = obj.Question11;
            emp.ELSQ12 = obj.Question12;
            emp.ELSQ13 = obj.Question13;
            emp.ELSQ14 = obj.Question14;
            emp.ELSQ15 = obj.Question15;
            emp.ELSQ16 = obj.Question16;
            emp.ELSQ17 = obj.Question17;
            emp.ELSQ18 = obj.Question18;
            emp.ELSQ19 = obj.Question19;
            emp.ELSQ20 = obj.Question20;
            emp.ELSQ21 = obj.Question21;
            emp.ELSQ22 = obj.Question22;
            emp.ELSQ23 = obj.Question23;
            emp.ELSQ24 = obj.Question24;
            emp.ELSQ25 = obj.Question25;
            emp.ELSQ26 = obj.Question26;
            emp.ELSQ27 = obj.Question27;
            emp.ELSQ28 = obj.Question28;
            emp.ELSQ29 = obj.Question29;
            emp.ELSQ30 = obj.Question30;
            emp.ELRCST = 0;

            var sum1 = obj.Question01 + obj.Question02 + obj.Question03 + obj.Question04 + obj.Question05 +
                       obj.Question06 + obj.Question07 + obj.Question08 + obj.Question09 + obj.Question10 +
                       obj.Question11 + obj.Question12 + obj.Question13 + obj.Question14 + obj.Question15 +
                       obj.Question16 + obj.Question17 + obj.Question18 + obj.Question19 + obj.Question20 +
                       obj.Question21 + obj.Question22 + obj.Question23 + obj.Question24 + obj.Question25 +
                       obj.Question26 + obj.Question27 + obj.Question28 + obj.Question29 + obj.Question30;
            var sum2 = obj.Question05 + obj.Question06 + obj.Question07 + obj.Question08;
            
            if (sum1 == 0 || sum1 >= 11 || sum2 < 3)
            {
                emp.ELRCST = 1;
            }

            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Gagal menyimpan data");
        }

        [HttpPut("employee/{id}/clinic")]
        public async Task<IActionResult> EditEmployeeStatus(string id, LebaranDto obj)
        {
            var emp = await _repo.GetEmployee(id, "");
            if (emp == null)
                return BadRequest("Data karyawan tidak ditemukan");

            emp.ELHCDT = DateTime.Now;
            emp.ELRCST = obj.Status;
            emp.ELCPIC = User.GetUserName();
            emp.ELREMA = obj.Remarks;

            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Gagal menyimpan data");
        }
        [AllowAnonymous]
        [HttpGet("department")]
        public async Task<IActionResult> GetDepartment([FromQuery]Params prm)
        {
            var dept = await _repo.GetDepartment(prm.brno);
            return Ok(dept);
        }
    }
}