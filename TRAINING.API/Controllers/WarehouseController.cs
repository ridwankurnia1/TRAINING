using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TRAINING.API.Data;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Controllers
{
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _repository;

        private readonly IMapper _mapper;

        public WarehouseController(IWarehouseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> All([FromQuery] WarehouseParams warehouseParams)
        {
            var list = await _repository.All(warehouseParams);

            var result = _mapper.Map<IList<WarehouseDto>>(list);

            Response.AddPagination(list.CurrentPage, list.PageSize, list.TotalCount, list.TotalPages);

            return Ok(result);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Single(string code)
        {
            var data = await _repository.Single(code);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WarehouseDto>(data));
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] WarehouseDto requestData)
        {
            if (requestData == null)
            {
                return BadRequest(requestData);
            }
            requestData.Branch = "CKP";
            requestData.CreatedTime = System.DateTime.Now;
            requestData.CreatedUser = "TEST";
            requestData.ChangedTime = System.DateTime.Now;
            requestData.ChangedUser = "TEST";

            var create = await _repository.Create(_mapper.Map<IWHSX>(requestData));

            if (!create)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, [FromBody] WarehouseDto requestData)
        {
            requestData.Branch = "CKP";
            requestData.CreatedTime = System.DateTime.Now;
            requestData.CreatedUser = "TEST";
            requestData.ChangedTime = System.DateTime.Now;
            requestData.ChangedUser = "TEST";

            var update = await _repository.Update(_mapper.Map<IWHSX>(requestData));

            if (!update)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            var delete = await _repository.Delete(code);

            if (!delete)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("type")]
        public async Task<IActionResult> AllType() => Ok(_mapper.Map<IList<GlobalCommonText2>>(await _repository.AllType()));

        [HttpGet("group")]
        public async Task<IActionResult> AllGroup(WarehouseParams warehouseParams)
        {
            var list = await _repository.AllGroup(warehouseParams);

            return Ok(_mapper.Map<IList<WarehouseGroupDto>>(list));
        }

        [HttpPost("group")]
        public async Task<IActionResult> CreateGroup([FromBody] WarehouseGroupDto requestData)
        {
            var data = requestData;

            data.CreatedUser = "TEST";
            data.ChangedUser = "TEST";
            data.CreatedTime = System.DateTime.Now;
            data.ChangedTime = System.DateTime.Now;
            data.Branch = "CKP";

            var create = await _repository.CreateGroup(_mapper.Map<IWGRX>(data));

            if (!create)
            {
                throw new System.Exception("Tidak dapat memproses permintaan");
            }

            return NoContent();
        }

        [HttpGet("group/{code}")]
        public async Task<IActionResult> SingleGroup(string code)
        {
            var data = await _repository.SingleGroup(code);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WarehouseGroupDto>(data));
        }

        [HttpPut("group/{code}")]
        public async Task<IActionResult> UpdateGroup(string code, [FromBody] WarehouseGroupDto requestData)
        {
            var data = await _repository.SingleGroup(code);

            if (data == null)
            {
                return NotFound();
            }

            requestData.Branch = "CKP";
            requestData.CreatedUser = data.HVCRUS;
            requestData.ChangedUser = data.HVCHUS;

            requestData.CreatedTime = System.DateTime.Now;
            requestData.ChangedTime = System.DateTime.Now;

            data = _mapper.Map<IWGRX>(requestData);

            var update = await _repository.UpdateGroup(data);

            if (!update)
            {
                throw new System.Exception("Tidak dapat memproses permintaan");
            }

            return NoContent();
        }

        [HttpDelete("group/{code}")]
        public async Task<IActionResult> DeleteGroup(string code)
        {
            var data = await _repository.SingleGroup(code);

            if (data == null)
            {
                return NotFound();
            }

            var delete = await _repository.DeleteGroup(data);

            if (!delete)
            {
                throw new System.Exception("Tidak dapat memproses permintaan");
            }

            return NoContent();
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export(WarehouseParams warehouseParams) => Ok(_mapper.Map<IList<WarehouseDto>>(await _repository.Export(warehouseParams)));

        [HttpGet("seeds")]
        public async Task<IActionResult> Seeds([FromQuery] bool clear)
        {
            if (clear)
            {
                _repository.GetContext().Database.ExecuteSqlRaw("TRUNCATE TABLE IWGRX");
                _repository.GetContext().Database.ExecuteSqlRaw("TRUNCATE TABLE IWHSX");
            }

            if (_repository.Query().Any() && _repository.QueryGroup().Any())
            {
                return BadRequest();
            }

            string[] whg = new string[10];

            for (int i = 0; i < 10; i++)
            {
                var entity = new IWGRX
                {
                    HVWHGR = Guid.NewGuid().ToString().Substring(0, 8),
                    HVGRNA = Faker.Company.Name(),
                    HVBRNO = "CKP",
                    HVREMA = Faker.Lorem.Sentence(),
                    HVRCST = Faker.RandomNumber.Next(1),
                    HVCRTM = DateTime.Now,
                    HVCRUS = "TEST",
                    HVCHTM = DateTime.Now,
                    HVCHUS = "TEST"
                };

                _repository.GetContext().IWGRX.Add(entity);
                whg[i] = entity.HVWHGR;
            }

            foreach (var name in whg)
            {
                for (int i = 0; i < 10; i++)
                {
                    var hwnick = Faker.Lorem.GetFirstWord();
                    var hwdfwh = Faker.Lorem.GetFirstWord();

                    _repository.GetContext().IWHSX.Add(new IWHSX
                    {
                        HWWHNO = Guid.NewGuid().ToString().Substring(0, 8),
                        HWBRNO = "CKP",
                        HWWHNA = Faker.Company.Name(),
                        HWNICK = hwnick.Substring(0, hwnick.Length > 9 ? 9 : hwnick.Length),
                        HWWHGR = name,
                        HWDFWH = hwdfwh.Substring(0, hwdfwh.Length > 9 ? 9 : hwdfwh.Length),
                        HWFDAY = Faker.RandomNumber.Next(0, 100),
                        HWFIFO = Faker.RandomNumber.Next(0, 1),
                        HWRCST = Faker.RandomNumber.Next(0, 1),
                        HWCRTM = DateTime.Now,
                        HWCRUS = "TEST",
                        HWCHTM = DateTime.Now,
                        HWCHUS = "TEST"
                    });
                }
            }

            await _repository.GetContext().SaveChangesAsync();

            return Ok(await _repository.Query().Select(c => new IWHSX { HWWHNO = c.HWWHNO }).CountAsync());
        }

    }
}