using Domain.DTO.TypeDTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationTypesController : ControllerBase
    {
        IOperationTypeService _operationTypeService;

        public OperationTypesController(IOperationTypeService service)
        {
            _operationTypeService = service;
        }

        // GET api/operationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationTypeDTO>>> GetAllAsync()
        {
            var operationTypes = await _operationTypeService.GetAllEntitysAsync();
            return Ok(operationTypes.ToList());
        }

        // GET api/operationTypes/
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> GetByIdAsync(Guid id)
        {
            var operationType = await _operationTypeService.GetByIdAsync(id);
            return Ok(operationType);
        }

        // POST api/operationTypes
        [HttpPost]
        public async Task<ActionResult<List<OperationTypeDTO>>> CreateAsync([FromBody] List<OperationTypeCreateUpdateDTO> newOperationTypes)
        {

            List<OperationTypeDTO> operationTypes = new List<OperationTypeDTO>();
            foreach (var operationTypeDTO in newOperationTypes)
            {
                var operationType = await _operationTypeService.CreateOperationTypeAsync(operationTypeDTO);
                operationTypes.Add(operationType);
            }

            return Created(nameof(GetByIdAsync), operationTypes);
        }

        // PUT api/operationTypes
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> UpdateAsync(Guid id, [FromBody] OperationTypeCreateUpdateDTO updatedOperationType)
        {
            await _operationTypeService.UpdateOperationTypeAsync(id, updatedOperationType);
            return NoContent();
        }

        // DELETE api/operationTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationTypeDTO>> DeleteAsync(Guid id)
        {
            await _operationTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
