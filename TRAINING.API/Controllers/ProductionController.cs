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
    public class ProductionController : ControllerBase
    {
        private readonly IProductionRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;
        public ProductionController(IProductionRepository repo, IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpGet("DatMDF0")]
        //menampilkan semua data pada database MDF0 
        public async Task<IActionResult> GetListMDF0([FromQuery] InventoryParams prm)
        {
            var data = await _repo.GetListMDF0();

            //var result = _mapper.Map<IEnumerable<LebaranDto>>(data);
            //Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            return Ok(data);
        }


        public async Task<IActionResult> GetMDF0([FromQuery] InventoryParams prm)
        {
            var data = await _repo.Get1MDF0(1);

            //var result = _mapper.Map<IEnumerable<LebaranDto>>(data);
            //Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("FindDatMDF0")]
        public async Task<IActionResult> FindListMDF0([FromQuery] InventoryParams prm, MDF0 obj)
        {
            // var data = await _repo.FindListMDF0();
            // return Ok(data);
            var data = await _repo.GetListMDF0();
            var result = _mapper.Map<IEnumerable<MDF0>>(data);

            return Ok(result);
        }


        [AllowAnonymous]
        [HttpPost("DatMDF0")]
        //menambahkan data ke dalam database MDF0 dengan bentuk objek
        public async Task<IActionResult> AddListMDF0(MDF0 data)
        {
            var temp = await _repo.GetDfGMDF0(data.DDDFGR);
            if (temp != null)
                return BadRequest("Data Sudah Ada");

            if (temp == null)
            {
                _repo.Add<MDF0>(data);

                if (await _repo.SaveAll())
                    return Ok();
            }

            throw new Exception("Berhasil Menyimpan Data");
        }
        // [AllowAnonymous]
        // [HttpGet("DatMDF0")]
        // public async Task<IActionResult> FindListMDF0(MDF0 data)
        // {
        //     int ddtrid = 1;
        //     var dat = await _repo.GetListMDF0(ddtrid);
        //     return Ok(dat);
        // }

        // [AllowAnonymous]
        // [HttpGet("DatMDF0")]
        // public async Task<IActionResult> GetMDF0Name([FromQuery] InventoryParams prm, IProductionRepository repo)
        // {

        //     var data = await _repo.GetListMDF0(prm);

        //     return Ok(data);
        // }

        [AllowAnonymous]
        [HttpDelete("DatMDF0")]
        public async Task<IActionResult> DeleteListMDF0(MDF0 data)
        {

            _repo.Delete<MDF0>(data);
            if (await _repo.SaveAll())
                return Ok();
            return Ok();
            // throw new Exception("Gagal Menghapus Data");
        }

        [AllowAnonymous]
        [HttpPut("EditDatMDF0")]
        //mengedit data dengan menggunakan objek
        public async Task<IActionResult> EditMDf0(MDF0 obj)
        {
            var data = await _repo.Get1MDF0(obj.DDTRID);
            if (data == null)
                return BadRequest("Data karyawan tidak ditemukan");

            // if (data.DDTRID.HasValue)
            //     return BadRequest("Data Sudah Ada ");

            // data.DDTRID = obj.DDTRID;
            data.DDDFGR = obj.DDDFGR;
            data.DDREMA = obj.DDREMA;
            data.DDRCST = obj.DDRCST;
            data.DDCRTM = obj.DDCRTM;
            data.DDCRUS = obj.DDCRUS;
            data.DDCHTM = obj.DDCHTM;
            data.DDCHUS = obj.DDCHUS;

            // var sum1 = obj.Question01 + obj.Question02 + obj.Question03 + obj.Question04 + obj.Question05 +
            //            obj.Question06 + obj.Question07 + obj.Question08 + obj.Question09 + obj.Question10 +
            //            obj.Question11 + obj.Question12 + obj.Question13 + obj.Question14 + obj.Question15 +
            //            obj.Question16 + obj.Question17 + obj.Question18 + obj.Question19 + obj.Question20 +
            //            obj.Question21 + obj.Question22 + obj.Question23 + obj.Question24 + obj.Question25;
            //    obj.Question26 + obj.Question27 + obj.Question28 + obj.Question29 + obj.Question30;
            // var sum2 = obj.Question05 + obj.Question06 + obj.Question07 + obj.Question08;

            // if (sum1 == 0 || sum1 >= 11)
            // {
            //     emp.ELRCST = 1;
            // }
            if (await _repo.SaveAll())
                return Ok(obj);

            throw new Exception("Gagal menyimpan data");
        }
        [AllowAnonymous]
        [HttpGet("1DatMDF0")]
        public async Task<IActionResult> Get1MDF0([FromQuery] InventoryParams prm)
        {
            var data = await _repo.Get1MDF0(prm.id);

            //var result = _mapper.Map<IEnumerable<LebaranDto>>(data);
            //Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetMDF0-id/{ddtrid}")]
        public async Task<IActionResult> GetMDF0(int ddtrid)
        {
            //GetMDF0ByID GetMDF0ByNAME
            // string strNIK = string.Empty;
            // string strRFID = string.Empty;

            // if (id.Length == 10)
            //     strRFID = id;
            // else
            //     strNIK = id;
            int temp = ddtrid; // -->mengisikan variabel temp dengan parameter ddtrid
            var emp = await _repo.Get1MDF0(temp);

            return Ok(emp);
        }

        [AllowAnonymous]
        [HttpGet("GetMDF0-nama/{ddchus}")]
        //mencari data pada database MDF0 dengan menggunakan parameter ddchus 
        public async Task<IActionResult> GetNameMDF0(string ddchus)
        {
            // string strNIK = string.Empty;
            // string strRFID = string.Empty;

            // if (id.Length == 10)
            //     strRFID = id;
            // else
            //     strNIK = id;
            string temp = ddchus; // -->mengisikan variabel temp dengan parameter ddtrid
            var emp = await _repo.GetNameMDF0(temp);

            return Ok(emp);
        }

        [AllowAnonymous]
        [HttpGet("GetMDF0ById")]
        //mencari data pada database mdf0 dengan parameter id menggunakan params
        public async Task<IActionResult> GetMDF0ById([FromQuery] InventoryParams prm)
        {
            var data = await _repo.GetMDF0ByParams(prm.id);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("GetMDF0ByName")]
        //mencari data pada database MDF0 dengan parameter nama menggunakan params
        public async Task<IActionResult> GetMDF0ByName([FromQuery] InventoryParams prm)
        {
            var data = await _repo.GetMDF0ByParamsName(prm.name);
            return Ok(data);
        }

        // [AllowAnonymous]
        // [HttpGet("GetMDF0ByName")]
        // //mencari data pada database MDF0 dengan parameter nama menggunakan params
        // public async Task<IActionResult> GetMDF0ByNam([FromQuery] InventoryParams prm)
        // {
        //     var data = await _repo.GetMDF0ByParamsName(prm.name);
        //     return Ok(data);
        // }
        [AllowAnonymous]
        [HttpGet("GetMDF0ByDddfgr/{dddfgr}")]
        public async Task<IActionResult> GetDfGMDF0(string dddfgr)
        {
            string temp = dddfgr; // -->mengisikan variabel temp dengan parameter dddfgr
            var emp = await _repo.GetMDF0ByDddfgr(temp);

            return Ok(emp);
        }

        [AllowAnonymous]
        [HttpGet("GetMDF0By/{transactionId},{defectGroup}")]
        public async Task<IActionResult> GetMDF0By(int transactionId, string defectGroup)
        {
            var emp = await _repo.GetMdf0By(defectGroup, transactionId);

            return Ok(emp);
        }





        // DIBUAT TANGGAL 09/01/2023


        [AllowAnonymous]
        [HttpGet("Mdf0/{ddtrid}")]
        public async Task<IActionResult> GetMDF0ByDdtrid(int ddtrid)
        {
            var data = await _repo.GetMDF0ByDdtrid(ddtrid);

            //var result = _mapper.Map<IEnumerable<LebaranDto>>(data);
            //Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpDelete("DatMDF0ByDdtrid/{ddtrid}")]
        //menghapus data pada database MDF0 dengan parameter id menggunakan params
        public async Task<IActionResult> DeleteListMDF0ByParams(int ddtrid)
        {
            var delete = await _repo.GetMDF0ByDdtrid(ddtrid);
            _repo.Delete<MDF0>(delete);
            if (await _repo.SaveAll())
                return Ok("Berhasil Menghapus Data");
            // return Ok();
            throw new Exception("Gagal Menghapus Data");
        }

        [AllowAnonymous]
        [HttpPut("Mdf0/{ddtrid}")]
        public async Task<IActionResult> EditEmployee(int ddtrid, MDF0 data)
        {
            var mdf0 = await _repo.GetMDF0ByDdtrid(ddtrid);
            if (mdf0 == null)
                return BadRequest("Data tidak ditemukan");

            mdf0.DDDFGR = data.DDDFGR;
            mdf0.DDREMA = data.DDREMA;
            mdf0.DDRCST = data.DDRCST;
            mdf0.DDCRTM = data.DDCRTM;
            mdf0.DDCRUS = data.DDCRUS;
            mdf0.DDCHTM = data.DDCHTM;
            mdf0.DDCHUS = data.DDCHUS;

            if (await _repo.SaveAll())
                return Ok("Data Berhasil Di Update");

            throw new Exception("Gagal mengupdate data");
        }


        //get data by params id


        // delete MDF0 by params
        [AllowAnonymous]
        [HttpDelete("DatMdf0/{ddtrid}")]
        public async Task<IActionResult> DeleteMdf0(int ddtrid)
        {
            var mdf0 = await _repo.GetMDF0ByDdtrid(ddtrid);
            if (mdf0 == null)
                return BadRequest("Data tidak ditemukan");

            _repo.Delete<MDF0>(mdf0);
            // employee.EMRCST = 0;
            // employee.EMCHDT = CommonMethod.DateToNumeric(DateTime.Now);
            // employee.EMCHTM = CommonMethod.TimeToNumeric(DateTime.Now);
            // employee.EMCHUS = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (await _repo.SaveAll())
                return Ok("Data Berhasil Dihapus");

            throw new Exception("Gagal mengupdate data");
        }


        //PEMBUATAN API DENGAN MENGGUNAKAN MAPPER 

        [AllowAnonymous]
        [HttpGet("DatMDF0Map")]
        //menampilkan semua data pada database MDF0 dengan menggunakan mapper
        public async Task<IActionResult> GetListMDF0Map([FromQuery] InventoryParams prm)
        {
            // var data = await _repo.GetListMDF0();
            var data = await _repo.GetListDefectPaging(prm);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);

            var result = _mapper.Map<IEnumerable<Mdf0Dto>>(data);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("DatMDF0Map/{ddtrid}")]
        //menampilkan data pada MDF0 berdasarkan ddtrid menggunakan mapper
        public async Task<IActionResult> GetMDF0ByDdtridMap(int ddtrid)
        {

            var data = await _repo.GetMDF0ByDdtrid(ddtrid);

            if (data == null)
                return BadRequest("Data tidak ditemukan");

            var result = _mapper.Map<Mdf0Dto>(data);

            return Ok(result);


        }


        [AllowAnonymous]
        [HttpPost("DatMDF0Map")]
        //menambahkan data ke dalam database MDF0 dengan bentuk objek dengan menggunakan mapper
        public async Task<IActionResult> AddListMDF0Map(Mdf0Dto data)
        {

            //Variabel penampung untuk menampung data yang akan di masukan
            var temp = await _repo.GetDfGMDF0(data.DefectGroup);
            if (temp != null)
                return BadRequest("Data Sudah Ada");

            var mdf0 = _mapper.Map<MDF0>(data);

            if (temp == null)
            {
                _repo.Add<MDF0>(mdf0);
                if (await _repo.SaveAll())
                    return Ok();
            }

            throw new Exception("Gagal Menyimpan Data");
        }



        [AllowAnonymous]
        [HttpPut("DatMdf0Map/{transactionId}")]
        public async Task<IActionResult> EditMDF0Map(int transactionId, Mdf0Dto data)
        {
            // var mdf0 = _mapper.Map<MDF0>(data);
            // var mdf0 = await _repo.GetMDF0ByDdtrid(ddtrid);
            var mdf0 = await _repo.GetMDF0ByDdtrid(transactionId);

            if (mdf0 == null)
                return BadRequest("Data tidak ditemukan");

            var temp = await _repo.GetDfGMDF0(data.DefectGroup);

            // var temp = await _repo.GetMdf0By(data.DefectGroup, data.TransactionId );
            if (temp != null && mdf0.DDTRID != temp.DDTRID)
                return BadRequest("Data Sudah Ada");
            // if (temp.DDTRID == mdf0.DDTRID)
            //     return BadRequest("Data Sudah Ada");

            // if (mdf0.DDTRID != data.TransactionId){

            // mdf0.DDTRID = data.TransactionId;
            mdf0.DDDFGR = data.DefectGroup;
            mdf0.DDREMA = data.Remark;
            mdf0.DDRCST = data.RecordStatus;
            mdf0.DDCRTM = (DateTime)data.CreateTime;
            mdf0.DDCRUS = data.CreateUser;
            mdf0.DDCHTM = DateTime.Now;
            mdf0.DDCHUS = data.ChangeUser;

            if (await _repo.SaveAll())
                return Ok();
            // }

            throw new Exception("Gagal mengupdate data");
        }


        [AllowAnonymous]
        [HttpDelete("DatMDF0ByDdtridMap/{transactionId}")]
        //menghapus data pada database MDF0 dengan parameter id menggunakan params
        public async Task<IActionResult> DeleteListMDF0ByDdtridMap(int transactionId)
        {
            var emp = await _repo.GetMDF0ByDdtrid(transactionId);

            if (emp == null)
                return BadRequest("Data karyawan tidak ditemukan");

            var delete = await _repo.GetMDF0ByDdtrid(transactionId);
            _repo.Delete<MDF0>(delete);

            if (await _repo.SaveAll())
                return Ok("Berhasil Menghapus Data");
            // return Ok();
            throw new Exception("Gagal Menghapus Data");
        }



        //TRUCK
        [AllowAnonymous]
        [HttpGet("truck")]
        public async Task<IActionResult> GetTruck([FromQuery] TruckParams prm)
        {
            var truck = await _repo.GetListTruck(prm);
            var result = _mapper.Map<IEnumerable<TruckDto>>(truck);
            Response.AddPagination(truck.CurrentPage, truck.PageSize, truck.TotalCount, truck.TotalPages);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("truck")]
        public async Task<IActionResult> AddTruck(TruckDto data)
        {
            var trck = _mapper.Map<TRCK>(data);
            trck.TBCRUS = "Ricky";
            trck.TBCHUS = "Ricky";


            _repo.Add<TRCK>(trck);


            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Gagal menyimpan data");


        }


        [AllowAnonymous]
        [HttpGet("truck/{truckId}")]
        public async Task<IActionResult> GetTruckByTruckId(int truckId)
        {
            var truck = await _repo.GetTruck(truckId);
            if (truck == null)
                return NoContent();

            var dataToReturn = _mapper.Map<TruckDto>(truck);
            return Ok(dataToReturn);
        }



        [AllowAnonymous]
        [HttpPut("truck/{truckId}")]
        public async Task<IActionResult> EditTruck(int truckId, TruckDto data)
        {
            var truck = await _repo.GetTruck(truckId);
            if (truck == null)
            {
                return BadRequest("Truck tidak ditemukan");
            }

            //Convert waktu angular ke timezone c#
            TimeZoneInfo timeZoneInfo;
            DateTime joinDateTime;
            DateTime endDateTime;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            joinDateTime = TimeZoneInfo.ConvertTime(data.JoinDate.Value, timeZoneInfo);
            endDateTime = TimeZoneInfo.ConvertTime(data.EndDate.Value, timeZoneInfo);

            truck.TBTRNA = data.TruckName;
            truck.TBMRKA = data.Merk;
            truck.TBDRNA = data.Driver;
            truck.TBJNDT = joinDateTime;
            truck.TBENDT = endDateTime;
            truck.TBCHDT = DateTime.Now;

            truck.TBCHUS = "Ricky";

            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Gagal mengupdate data");
        }


        //restore
        [AllowAnonymous]
        [HttpPut("truck-restore/{truckId}")]
        public async Task<IActionResult> RestoreTruck(int truckId, int truckId2)
        {
            var truck = await _repo.GetTruck(truckId);
            if (truck == null)
                return BadRequest("Truck tidak ditemukan");

            truck.TBRCST = 1;
            truck.TBCHDT = DateTime.Now;
            truck.TBCHUS = "Ricky";


            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Gagal merestore data");
        }


        [AllowAnonymous]
        [HttpDelete("truck/{truckId}")]
        public async Task<IActionResult> DeleteTruck(int truckId)
        {
            var truck = await _repo.GetTruck(truckId);
            if (truck == null)
                return BadRequest("Truck tidak ditemukan");

            // _repo.Delete<MEMP>(employee);
            truck.TBRCST = 0;
            truck.TBCHDT = DateTime.Now;
            truck.TBCHUS = "Ricky";

            if (await _repo.SaveAll())
                return Ok();

            throw new Exception("Gagal menghapus data");
        }
        [AllowAnonymous]
        [HttpGet("export")]
        public async Task<IActionResult> Export([FromQuery] TruckParams truckParams)
        {
            // => Ok(_mapper.Map<IList<TruckDto>>(await _repo.Export(truckParams)));
            var truck = await _repo.Export(truckParams);
            var result = _mapper.Map<IEnumerable<TruckDto>>(truck);
            // Response.AddPagination(truck.CurrentPage, truck.PageSize, truck.TotalCount, truck.TotalPages);

            return Ok(result);
        }


    }
}