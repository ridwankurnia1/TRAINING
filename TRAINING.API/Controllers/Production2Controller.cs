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
using System.Linq;

namespace TRAINING.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class Production2Controller : ControllerBase
    {
        private readonly IProductionRepository2 _repo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public Production2Controller(IProductionRepository2 repo, IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _repo = repo;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        

        [AllowAnonymous]
        [HttpGet("DatMDF1")]
        //menampilkan semua data pada database MDF0 
        public async Task<IActionResult> GetListMDF1([FromQuery] InventoryParams prm)
        {
            var data = await _repo.GetListDefect2Paging(prm);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);

            var result = _mapper.Map<IEnumerable<Mdf1Dto>>(data);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("DatMDF1/{Id}")]
        //menampilkan semua data pada database MDF0 
        public async Task<IActionResult> GetListMDF1ById([FromQuery] InventoryParams prm, string Id)
        {
            var data = await _repo.GetMDF1ById(Id);

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("DatMDF1")]
        //menambahkan data ke dalam database MDF0 dengan bentuk objek dengan menggunakan mapper
        
        public async Task<IActionResult> AddListMDF0Map(Mdf1Dto data)
        {

            //Variabel penampung untuk menampung data yang akan di masukan
            var temp = await _repo.GetMDF1ById(data.DefectCode);
            if (temp != null)
                return BadRequest("Data Sudah Ada");

            var id = await _repo.GetMDF0ByName(data.DefectGroup1); //untuk meng get Transaction Id berdasarkan defectname 

            
            var mdf1 = _mapper.Map<MDF1>(data);
            
            if (temp == null)
            {
                mdf1.DEGRID = id.DDTRID;
                // mdf1.DEDFNO = '0' + RandomString(1);
                mdf1.DECRDT = CommonMethod.DateToNumeric(DateTime.Now);
                mdf1.DECRTM = CommonMethod.TimeToNumeric(DateTime.Now);
                mdf1.DECHDT = CommonMethod.DateToNumeric(DateTime.Now);
                mdf1.DECHTM = CommonMethod.TimeToNumeric(DateTime.Now);
                _repo.Add<MDF1>(mdf1);
                if (await _repo.SaveAll())
                    return Ok();
            }
            // _repo.Add<MDF0>(data);
            //  if (await _repo.SaveAll())
            //     return Ok("Data Berhasil Disimpan");

            throw new Exception("Gagal Menyimpan Data");
        }

        [AllowAnonymous]
        [HttpPut("DatMdf1/{Id}")]
        public async Task<IActionResult> EditMdf1(String Id, Mdf1Dto data)
        {
            // var mdf0 = _mapper.Map<MDF0>(data);
            // var mdf0 = await _repo.GetMDF0ByDdtrid(ddtrid);
            // check = await _repo.GetMDF1ByName;
            var mdf1 = await _repo.GetMDF1ById(Id);

            var id = await _repo.GetMDF0ByName(data.DefectGroup1); //untuk meng get Transaction Id berdasarkan defectname 

            if (mdf1 == null)
                return BadRequest("Data tidak ditemukan");
            
            mdf1.DEDFNA = data.DefectName;
            mdf1.DEGRID = id.DDTRID; //mengisikan DEGRID dengan value transaction Id
            mdf1.DEDPGR = data.DefectType;
            mdf1.DEDFG1 = data.DefectGroup1;
            mdf1.DEDFG2 = data.DefectGroup2;
            mdf1.DEREMA = data.Remark;
            mdf1.DERCST = data.RecordStatus;

            mdf1.DECHDT = CommonMethod.DateToNumeric(DateTime.Now);
            mdf1.DECHTM = CommonMethod.TimeToNumeric(DateTime.Now);
            mdf1.DECHUS = "GUSTI";
            
            if (await _repo.SaveAll())
                return Ok();
            // }
            
            throw new Exception("Gagal mengupdate data");
        }

        [AllowAnonymous]
        [HttpDelete("DatMdf1/{Id}")]
        //menghapus data pada database MDF0 dengan parameter id menggunakan params
        public async Task<IActionResult> DeleteListMDF0ByDdtridMap(String Id)
        {
            var emp = await _repo.GetMDF1ById(Id);
            
            if (emp == null)
                return BadRequest("Data tidak ditemukan");

            var delete = await _repo.GetMDF1ById(Id);
            _repo.Delete<MDF1>(delete);
            
            if (await _repo.SaveAll())
                return Ok();
            // return Ok();
            throw new Exception("Gagal Menghapus Data");
        }

        [AllowAnonymous]
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdown([FromQuery]InventoryParams prm)
        {
            var Mdf0Drop = await _repo.GetDefectGroup();
            
            var ddl_org = _mapper.Map<IEnumerable<DropdownDto>>(Mdf0Drop);            

            return Ok(
                new {
                defg = ddl_org}
                );
        }


    }
}