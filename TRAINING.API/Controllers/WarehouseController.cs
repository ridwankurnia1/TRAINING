using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AllGroup()
        {
            var list = await _repository.AllGroup();

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

    }
}