using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TRAINING.API.Data;
using TRAINING.API.Helper;
using TRAINING.API.Model;
using TRAINING.API.ViewModel;

namespace TRAINING.API.Controllers
{
    [Route("api/ipty")]
    [ApiController]
    public class PalletTypeController : ControllerBase
    {
        private readonly IPalletTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PalletTypeController> _logger;

        public PalletTypeController(IPalletTypeRepository repository, IMapper mapper, ILogger<PalletTypeController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> All([FromQuery] PalletTypeParams param)
        {
            var list = await _repository.GetAllPalletTypes(param);
            if (list.Count == 0)
            {
                return NotFound(new
                {
                    Code = 404,
                    Message = "Tidak ada data"
                });
            }
            var result = _mapper.Map<IEnumerable<PalletTypeDto>>(list);

            Response.AddPagination(list.CurrentPage, list.PageSize, list.TotalCount, list.TotalPages);

            return Ok(result);
        }

        [HttpGet("find/{user}")]
        public async Task<IActionResult> GetByUser(string user)
        {
            if (user == null)
            {
                return NotFound();
            }

            var data = await _repository.FindByUser(user);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PalletTypeDto>(data));
        }

        [HttpGet("find-type/{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            if (type == null)
            {
                return NotFound();
            }

            var data = await _repository.FindByType(type);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PalletTypeDto>(data));
        }

        [HttpPost()]
        public async Task<IActionResult> Create(PalletTypeDto requestData)
        {
            var data = requestData;

            data.CreatedDate = CommonMethod.DateToNumeric(System.DateTime.Now);
            data.CreatedTime = CommonMethod.TimeToNumeric(System.DateTime.Now);
            data.ChangedDate = CommonMethod.DateToNumeric(System.DateTime.Now);
            data.ChangedTime = CommonMethod.TimeToNumeric(System.DateTime.Now);

            if (data.CreatedUser == null)
            {
                data.CreatedUser = "TEST";
            }

            _repository.Add(_mapper.Map<IPTY>(data));

            if (await _repository.SaveAll())
            {
                return NoContent();
            }

            throw new System.Exception("Gagal menyimpan data");
        }

        [HttpPut("{type}")]
        public async Task<IActionResult> Update(string type, PalletTypeDto requestData)
        {
            if (type == null)
            {
                return NotFound();
            }

            var data = requestData;

            if (data.ChangedUser == null)
            {
                data.ChangedUser = "TESTING";
            }

            if (await _repository.Update(type, data))
            {
                return NoContent();
            }

            throw new System.Exception("Gagal mengubah data");
        }

        [HttpDelete("{type}")]
        public async Task<IActionResult> Delete(string type)
        {
            if (type == null)
            {
                return NotFound();
            }

            var data = await _repository.FindByType(type);

            if (data == null)
            {
                return NotFound();
            }

            _repository.Delete(data);

            if (await _repository.SaveAll())
            {
                return NoContent();
            }

            throw new System.Exception("Gagal menghapus data");
        }

        [HttpGet("plap")]
        public async Task<IActionResult> GetPalletApp()
        {
            var res = await _repository.GetPalletAppDefinition();

            if (res == null || res.Count == 0)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Tidak ada data"
                });
            }

            return Ok(_mapper.Map<IList<DefinitionVarDto>>(res));
        }

        [HttpGet("gct2")]
        public async Task<IActionResult> GetCommonText2([FromQuery]string type)
        {
            var res = await _repository.GetCommonnText2(type);

            if (res == null || res.Count == 0)
            {
                return NotFound(new ErrorResponse
                {
                    statusCode = 404,
                    message = "Tidak ada data"
                });
            }

            return Ok(_mapper.Map<IList<GlobalCommonText2>>(res));
        }

        [HttpGet("currencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            var res = await _repository.GetCurrencyDefinition();
            if (res == null || res.Count == 0)
            {
                return NotFound(new ErrorResponse
                {
                    statusCode = 404,
                    message = "Tidak ada data"
                });
            }

            return Ok(_mapper.Map<IList<CurrencyDefinitionDto>>(res));
        }

        [HttpGet("measurements")]
        public async Task<IActionResult> GetMeasurements()
        {
            var res = await _repository.GetMeasurements();
            if (res == null || res.Count == 0)
            {
                return NotFound(new ErrorResponse
                {
                    statusCode = 404,
                    message = "Tidak ada data"
                });
            }

            return Ok(_mapper.Map<IList<MeasurementDefinitionDto>>(res));
        }

        private class ErrorResponse
        {
            public int statusCode { get; set; }
            public int errorCode { get; set; }
            public string message { get; set; }
        }
    }
}