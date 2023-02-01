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
    public class Production3Controller : ControllerBase
    {
        private readonly IProductionRepository3 _repo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public Production3Controller(IProductionRepository3 repo, IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _repo = repo;
        }        

        [AllowAnonymous]
        [HttpGet("MDMP")]
        //menampilkan semua data pada database MDF0 
        public async Task<IActionResult> GetListMDMP([FromQuery] Params prm)
        {
            var data = await _repo.GetMdmpPaging(prm);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);

            var result = _mapper.Map<IEnumerable<MdmpDto>>(data);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("MDMP/{Id}")]
        //menampilkan semua data pada database MDF0 
        public async Task<IActionResult> GetListMmpById([FromQuery] Params prm, string Id)
        {
            var data = await _repo.GetMdmpById(Id);

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("MDMP")]
        //menambahkan data ke dalam database MDF0 dengan bentuk objek dengan menggunakan mapper
        
        public async Task<IActionResult> AddListMDF0Map(MdmpDto data)
        {
            
            // var temp = await _repo.GetMDF1ById(data.DefectCode);

            var id = await _repo.GetMDF1ByDefectType(data.DefectType);

            var add = _mapper.Map<MDMP>(data);
            

            add.DMDFNO = id.DEDFNO;
            add.DMCRDT = CommonMethod.DateToNumeric(DateTime.Now);
            add.DMCRTM = CommonMethod.TimeToNumeric(DateTime.Now);
            // // mdf1.DEDFNO = '0' + RandomString(1);
            // mdf1.DECRDT = CommonMethod.DateToNumeric(DateTime.Now);
            // mdf1.DECRTM = CommonMethod.TimeToNumeric(DateTime.Now);
            // mdf1.DECHDT = CommonMethod.DateToNumeric(DateTime.Now);
            // mdf1.DECHTM = CommonMethod.TimeToNumeric(DateTime.Now);
            _repo.Add<MDMP>(add);
            if (await _repo.SaveAll())
                return Ok("Berhasil Menyimpan Data");
            // _repo.Add<MDF0>(data);
            //  if (await _repo.SaveAll())
            //     return Ok("Data Berhasil Disimpan");

            throw new Exception("Gagal Menyimpan Data");
        }

        [AllowAnonymous]
        [HttpDelete("MDMP/{Id}")]
        //menghapus data pada database MDF0 dengan parameter id menggunakan params
        public async Task<IActionResult> DeleteMdmpById(String Id)
        {
            var delete = await _repo.GetMdmpById(Id);
            if (delete == null)
                return BadRequest("Data tidak ditemukan");

            _repo.Delete<MDMP>(delete);
            
            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Gagal Menghapus Data");
        }


    }
}