// Controllers/VariableController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TRAINING.API.Helper;
using TRAINING.API.Helpers;
using TRAINING.API.Model;
using TRAINING.API.Repositories;
using TRAINING.API.ViewModel;


namespace TRAINING.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariableController : ControllerBase
    {
        private readonly IVariableRepository _repository;
        private readonly IMapper _mapper;

        public VariableController(IVariableRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all variables with optional search
        /// </summary>
        /// <param name="variableParams">Search parameters</param>
        /// <returns>List of variables</returns>
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<VariableDto>>> GetAllVariables(
        // [FromQuery] string? search,
        // [FromQuery] int page = 1,
        // [FromQuery] int pageSize = 10)
        // {
        //     try
        //     {
        //         var variableParams = new VariableParams
        //         {
        //             Search = search,
        //             Page = page,
        //             PageSize = pageSize
        //         };

        //         var variables = await _repository.GetAllAsync(variableParams);
        //         var result = _mapper.Map<List<VariableDto>>(variables);
        //         return Ok(result);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError,
        //             $"Error retrieving variables: {ex.Message}");
        //     }
        // }

        [HttpGet]
        public async Task<IActionResult> GetVariable([FromQuery] VariableParams prm)
        {
            // prm.Page = prm.Page == 0 ? 10 : prm.Page;
            // prm.PageSize = prm.PageSize == 0 ? 20 : prm.PageSize;

            var variable = await _repository.GetListVariable(prm);
            var result = _mapper.Map<IEnumerable<VariableDto>>(variable);
            Response.AddPagination(variable.CurrentPage, variable.PageSize, variable.TotalCount, variable.TotalPages);

            return Ok(result);
        }

        /// <summary>
        /// Get variable by ID
        /// </summary>
        /// <param name="id">Variable ID</param>
        /// <returns>Variable details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<VariableDto>> GetVariable(int id)
        {
            try
            {
                var variable = await _repository.GetByIdAsync(id);
                if (variable == null)
                {
                    return NotFound($"Variable with ID {id} not found");
                }
                return Ok(variable.ToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error retrieving variable: {ex.Message}");
            }
        }

        /// <param name="createDto">Variable creation data</param>
        /// <returns>Created variable</returns>
        [HttpPost]
        public async Task<ActionResult<VariableDto>> CreateVariable(VariableDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                createDto.User = "AMG";
                createDto.Name = "CKP";
                // createDto.CreatedTime = System.DateTime.Now;
                // createDto.Changedtime = System.DateTime.Now;

                // var variable = createDto.ToModel();
                var createdVariable = await _repository.CreateAsync(_mapper.Map<ZVAR>(createDto));

                return CreatedAtAction(nameof(GetVariable),
                    new { id = createdVariable.ZRRCID },
                    createdVariable.ToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error creating variable: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing variable
        /// </summary>
        /// <param name="id">Variable ID</param>
        /// <param name="updateDto">Variable update data</param>
        /// <returns>Updated variable</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<VariableDto>> UpdateVariable(int id, VariableDto updateDto)
        {
            try
            {
                if (id != updateDto.VariableId)
                {
                    return BadRequest("Variable ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingVariable = await _repository.GetByIdAsync(id);
                if (existingVariable == null)
                {
                    return NotFound($"Variable with ID {id} not found");
                }

                existingVariable.ZRCONO = updateDto.User;
                existingVariable.ZRBRNO = updateDto.Name;
                existingVariable.ZRVANO = updateDto.Code;
                existingVariable.ZRVANA = updateDto.Value;

                var updatedVariable = await _repository.UpdateAsync(id, existingVariable);
                return Ok(updatedVariable.ToDto());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating variable: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a variable
        /// </summary>
        /// <param name="id">Variable ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariable(int id)
        {
            try
            {
                var exists = await _repository.ExistsAsync(id);
                if (!exists)
                {
                    return NotFound($"Variable with ID {id} not found");
                }

                var deleted = await _repository.DeleteAsync(id);
                if (deleted)
                {
                    return NoContent();
                }

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting variable");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error deleting variable: {ex.Message}");
            }
        }
    }
}