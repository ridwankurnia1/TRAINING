using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly String ExceptionMessage = "Kesalahan Server/Permintaan tidak dapat diproses";

        public PalletTypeController(IPalletTypeRepository repository, IMapper mapper, ILogger<PalletTypeController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> All([FromQuery] PalletTypeParams param)
        {
            var list = await _repository.All(param);
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

        [HttpGet("export")]
        public async Task<IActionResult> Exportable([FromQuery] PalletTypeParams param)
        {
            var list = await _repository.Exportable(param);
            if (list.Count == 0)
            {
                return NotFound(new ErrorResponse
                {
                    ErrorCode = 404,
                    Message = "Tidak ada data",
                });
            }

            return Ok(_mapper.Map<IList<PalletTypeDto>>(list));
        }

        [HttpGet("find/{user}")]
        public async Task<IActionResult> GetByUser(string user)
        {
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

            data.Company = "AMG";
            data.Branch = "CKP";
            data.CreatedDate = CommonMethod.DateToNumeric(System.DateTime.Now);
            data.CreatedTime = CommonMethod.TimeToNumeric(System.DateTime.Now);
            data.ChangedDate = CommonMethod.DateToNumeric(System.DateTime.Now);
            data.ChangedTime = CommonMethod.TimeToNumeric(System.DateTime.Now);

            if (data.CreatedUser == null)
            {
                data.CreatedUser = "TEST";
            }

            if (data.ChangedUser == null)
            {
                data.ChangedUser = "TEST";
            }

            _repository.Add(_mapper.Map<IPTY>(data));

            if (await _repository.SaveAll())
            {
                return NoContent();
            }

            throw new UnknownException(ExceptionMessage);
        }

        [HttpPut("{type}")]
        public async Task<IActionResult> Update(string type, PalletTypeDto requestData)
        {
            var data = await _repository.Query()
            .Select(c => new IPTY { HSCONO = c.HSCONO, HSBRNO = c.HSBRNO, HSPETY = c.HSPETY })
            .FirstOrDefaultAsync(c => c.HSPETY == type);

            if (data == null)
            {
                return NotFound(new ErrorResponse
                {
                    Message = "Data tidak ditemukan"
                });
            }

            requestData.Company = data.HSCONO;
            requestData.Branch = data.HSBRNO;
            requestData.CreatedDate = CommonMethod.DateToNumeric(System.DateTime.Now);
            requestData.CreatedTime = CommonMethod.TimeToNumeric(System.DateTime.Now);
            requestData.ChangedDate = CommonMethod.DateToNumeric(System.DateTime.Now);
            requestData.ChangedTime = CommonMethod.TimeToNumeric(System.DateTime.Now);

            if (requestData.ChangedUser == null)
            {
                requestData.ChangedUser = "TEST";
            }


            data = _mapper.Map<IPTY>(requestData);

            if (await _repository.Update(type, data))
            {
                return NoContent();
            }

            throw new UnknownException(ExceptionMessage);
        }

        [HttpDelete("{type}")]
        public async Task<IActionResult> Delete(string type)
        {
            var data = await _repository.FindByType(type);

            if (data == null)
            {
                return NotFound(new ErrorResponse
                {
                    StatusCode = 400,
                    Message = "Tidak ada data"
                });
            }

            _repository.Delete(data);

            if (await _repository.SaveAll())
            {
                return NoContent();
            }

            throw new UnknownException(ExceptionMessage);
        }

        [HttpGet("plap")]
        public async Task<IActionResult> GetPalletApp()
        {
            var res = await _repository.GetPalletAppDefinition();

            if (res == null || res.Count == 0)
            {
                return NotFound(new ErrorResponse
                {
                    StatusCode = 404,
                    Message = "Tidak ada data"
                });
            }

            return Ok(_mapper.Map<IList<DefinitionVarDto>>(res));
        }

        [HttpGet("gct2")]
        public async Task<IActionResult> GetCommonText2([FromQuery] string type)
        {
            var res = await _repository.GetCommonnText2(type);

            if (res == null || res.Count == 0)
            {
                return NotFound(new ErrorResponse
                {
                    StatusCode = 404,
                    Message = "Tidak ada data"
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
                    StatusCode = 404,
                    Message = "Tidak ada data"
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
                    StatusCode = 404,
                    Message = "Tidak ada data"
                });
            }

            return Ok(_mapper.Map<IList<MeasurementDefinitionDto>>(res));
        }

        private class ErrorResponse
        {
            public int StatusCode { get; set; } = 400;
            public int ErrorCode { get; set; }
            public string Message { get; set; }
        }

        private class SuccessResponse
        {
            public int StatusCode { get; set; } = 200;
            public string Message { get; set; } = "Success";
            public Object Data { get; set; }
        }

        [System.Serializable]
        public class UnknownException : System.Exception
        {
            public UnknownException() { }
            public UnknownException(string message) : base(message) { }
            public UnknownException(string message, System.Exception inner) : base(message, inner) { }
            protected UnknownException(
                System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}