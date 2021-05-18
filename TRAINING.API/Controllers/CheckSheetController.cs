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
using TRAINING.API.Model;

namespace TRAINING.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class CheckSheetController : ControllerBase
    {
        private readonly ICheckSheetRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public CheckSheetController(ICheckSheetRepository repo, IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("employee/detail")]
        public async Task<IActionResult> GetEmployeeDetail([FromQuery] Params prm)
        {
            var data = await _repo.GetListEmployee(prm);
            var result = _mapper.Map<IEnumerable<LebaranDto>>(data);

            return Ok(result);
        }

        [HttpGet("employee/summary")]
        public async Task<IActionResult> GetEmployeeSummary([FromQuery] Params prm)
        {
            var data = await _repo.GetSummaryLebaran(prm.brno);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("employee")]
        public async Task<IActionResult> GetEmployee([FromQuery] Params prm)
        {
            var data = await _repo.GetListEmployeePaging(prm);
            if (!string.IsNullOrEmpty(prm.xls))
            {
                var xls = _mapper.Map<IEnumerable<LebaranXLSDto>>(data);
                Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
                return Ok(xls);
            }

            var result = _mapper.Map<IEnumerable<LebaranDto>>(data);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("employee/{id}", Name = "GetEmployee")]
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

        [HttpPost("employee")]
        public async Task<IActionResult> AddEmployee(EmployeeDto data)
        {
            var emp = await _repo.GetEmployee(data.Nik, "");
            if (emp != null)
                return BadRequest("NIK sudah terdaftar a.n. " + emp.ELEMNA);

            var ehal = _mapper.Map<EHAL>(data);
            _repo.Add<EHAL>(ehal);
            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Gagal menyimpan data");
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
            // emp.ELSQ26 = obj.Question26;
            // emp.ELSQ27 = obj.Question27;
            // emp.ELSQ28 = obj.Question28;
            // emp.ELSQ29 = obj.Question29;
            // emp.ELSQ30 = obj.Question30;
            emp.ELRCST = 0;

            var sum1 = obj.Question01 + obj.Question02 + obj.Question03 + obj.Question04 + obj.Question05 +
                       obj.Question06 + obj.Question07 + obj.Question08 + obj.Question09 + obj.Question10 +
                       obj.Question11 + obj.Question12 + obj.Question13 + obj.Question14 + obj.Question15 +
                       obj.Question16 + obj.Question17 + obj.Question18 + obj.Question19 + obj.Question20 +
                       obj.Question21 + obj.Question22 + obj.Question23 + obj.Question24 + obj.Question25;
            //    obj.Question26 + obj.Question27 + obj.Question28 + obj.Question29 + obj.Question30;
            // var sum2 = obj.Question05 + obj.Question06 + obj.Question07 + obj.Question08;

            if (sum1 == 0 || sum1 >= 11)
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
        [HttpGet("employee/security")]
        public async Task<IActionResult> GetEmployeeAttendance([FromQuery] Params prm)
        {
            prm.attendance = 1;
            prm.PageNumber = 1;
            prm.PageSize = 5;
            var data = await _repo.GetListEmployeePaging(prm);
            var result = _mapper.Map<IEnumerable<LebaranDto>>(data);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("employee/security")]
        public async Task<IActionResult> EditEmployeeAttendance(EmployeeIdDto obj)
        {
            if (string.IsNullOrEmpty(obj.Nik) && string.IsNullOrEmpty(obj.RFID))
                return BadRequest("Harap TAP Id Card atau input NIK");

            var emp = await _repo.GetEmployee(obj.Nik, obj.RFID);
            if (emp == null)
                return BadRequest("Data Karyawan tidak ditemukan");

            if (!emp.ELATDT.HasValue)
            {
                emp.ELATDT = DateTime.Now;
                if (await _repo.SaveAll())
                {
                    var ehal = await _repo.GetEmployee(emp.ELEMNO, "");
                    var response = _mapper.Map<LebaranDto>(ehal);
                    return CreatedAtRoute("GetEmployee", new { id = response.EmployeeId }, response);
                }
            }
            else
            {
                var resp = _mapper.Map<LebaranDto>(emp);
                return Ok(resp);
            }

            throw new Exception("Gagal menyimpan data, ulangi proses absensi");
        }

        [HttpGet("department")]
        public async Task<IActionResult> GetDepartment([FromQuery] Params prm)
        {
            var dept = await _repo.GetDepartment(prm.brno);
            return Ok(dept);
        }

        [AllowAnonymous]
        [HttpGet("tap")]
        public async Task<IActionResult> GetTapHeader([FromQuery]Params prm)
        {
            var data = await _repo.GetListTapHeader();
            var result = _mapper.Map<IEnumerable<LogHeaderDto>>(data);
            
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("tap/{id}")]
        public async Task<IActionResult> GetTapHeaderById(int id)
        {
            var data = await _repo.GetTapHeader(id);
            var result = _mapper.Map<LogHeaderDto>(data);
            var count = await _repo.GetTapLogCount(DateTime.Now);
            result.Count = count;
            
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("tap/log")]
        public async Task<IActionResult> GetTapLog([FromQuery]Params prm)
        {
            var data = await _repo.GetListTapLog(prm);
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(data);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("tap/{id}")]
        public async Task<IActionResult> AddTapLog(int id, EmployeeIdDto obj)
        {
            if (string.IsNullOrEmpty(obj.Nik) && string.IsNullOrEmpty(obj.RFID))
                return BadRequest("Harap TAP Id Card atau input NIK");

            var emp = await _userRepo.GetEmployee(obj.Nik, obj.RFID);
            if (emp == null)
                return BadRequest("Data Karyawan tidak ditemukan");

            var elog = _mapper.Map<ELOG>(emp);
            elog.ELRCID = id;
            _repo.Add<ELOG>(elog);
            if (await _repo.SaveAll())
            {
                var log = await _repo.GetTapLog(elog.ELTRID);
                var response = _mapper.Map<EmployeeDto>(log);
                return CreatedAtRoute("GetEmployee", new { id = response.Nik }, response);
            }

            throw new Exception("Gagal menyimpan data, ulangi proses absensi");
        }
    }
}