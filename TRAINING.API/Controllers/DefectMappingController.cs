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
    public class DefectMappingController : ControllerBase
    {
        private readonly IDefectMappingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public DefectMappingController(IDefectMappingRepository repo, IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _repo = repo;
        }        

        [AllowAnonymous]
        [HttpGet("MDMP")]
        //menampilkan semua data pada database MDF0 
        public async Task<IActionResult> GetListMDMP([FromQuery] DefectMappingParams prm)
        {
            var data = await _repo.GetMdmpPaging(prm);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);

            var result = _mapper.Map<IEnumerable<Mdf1Dto>>(data);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("MDMP/{Id}")]
        //menampilkan semua data pada database MDF0 
        public async Task<IActionResult> GetListMmpById([FromQuery] DefectMappingParams prm, string Id)
        {
            var data = await _repo.GetMdmpById(Id);

            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost("MDMP")]
        //menambahkan data ke dalam database MDF0 dengan bentuk objek dengan menggunakan mapper
        
        public async Task<IActionResult> AddListMdmp(DefectMappingDto data)
        {
            
            // var temp = await _repo.GetMDF1ById(data.DefectCode);

            // var id = await _repo.GetMDF1ByDefectType(data.DefectType);
            var id = await _repo.GetMDF1ByDefectCode(data.DefectCode);

            var add = _mapper.Map<MDMP>(data);
            
            add.DMDFNO = id.DEDFNO;
            add.DMDPGR = id.DEDFNA;
            add.DMCRDT = CommonMethod.DateToNumeric(DateTime.Now);
            add.DMCRTM = CommonMethod.TimeToNumeric(DateTime.Now);
            add.DMCHDT = CommonMethod.DateToNumeric(DateTime.Now);
            add.DMCHTM = CommonMethod.TimeToNumeric(DateTime.Now);

            _repo.Add<MDMP>(add);
            if (await _repo.SaveAll())
                return Ok();


            throw new Exception("Gagal Menyimpan Data");
        }

        [AllowAnonymous]
        [HttpPut("MDMP/{Id}")]
        public async Task<IActionResult> EditMdmp(String Id, DefectMappingDto data)
        {
            // var mdf0 = _mapper.Map<MDF0>(data);
            // var mdf0 = await _repo.GetMDF0ByDdtrid(ddtrid);
            // check = await _repo.GetMDF1ByName;
            var edit = await _repo.GetMdmpById(Id);

            var id = await _repo.GetMDF1ByDefectType(data.DefectType); //untuk meng get Transaction Id berdasarkan defectname 

            if (edit == null)
                return BadRequest("Data tidak ditemukan");
            

            edit.DMPOSD = data.DefectPosition;

            edit.DMPOSX = data.DefectPostX;
            edit.DMPOSY = data.DefectPostY;
            edit.DMREMA = data.Remark;
            edit.DMRCST = data.RecordStatus;
            edit.DMCHDT = CommonMethod.DateToNumeric(DateTime.Now);
            edit.DMCHTM = CommonMethod.TimeToNumeric(DateTime.Now);
            edit.DMCHUS = "GUSTI";
            
            if (await _repo.SaveAll())
                return Ok();
            // }
            
            throw new Exception("Gagal mengupdate data");
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

        [AllowAnonymous]
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdown([FromQuery]ProductionParams prm)
        {
            var lineP = await _repo.GetLineProcessGroup();

            var defT = await _repo.GetZvarDefectType();

            var defC = await _repo.GetDefectCode();

            var ddl_org = _mapper.Map<IEnumerable<DropdownDto>>(lineP);  
            var def_Ty = _mapper.Map<IEnumerable<DropdownDto>>(defT); 
            var def_Co = _mapper.Map<IEnumerable<DropdownDto>>(defC);         

            return Ok(new {
                lineProcess = ddl_org,
                zvarDefTy = def_Ty,
                defectCode = def_Co,
            });
        }


    }
}