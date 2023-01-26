using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TRAINING.API.Data;
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

        [HttpGet("group")]
        public async Task<IActionResult> AllGroup()
        {
            var list = await _repository.AllGroup();

            if (list.Count == 0)
            {
                return NotFound("Tidak ada data");
            }

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