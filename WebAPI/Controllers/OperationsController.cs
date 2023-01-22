using Domain.DTO.OperationDTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationService operationService;

        public OperationsController(IOperationService service)
        {
            operationService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationDTO>>> GetAllAsync()
        {
            var operations = await operationService.GetAllEntitysAsync();
            return Ok(operations.ToList());
        }

        // GET api/operations/operationById/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDTO>> GetByIdAsync(Guid id)
        {
            var operation = await operationService.GetByIdAsync(id);
            return Ok(operation);
        }

        // POST api/operations
        [HttpPost]
        public async Task<ActionResult<IEnumerable<OperationDTO>>> CreateAsync([FromBody] IEnumerable<OperationCreateUpdateDTO> newOperations)
        {
            var operations = new List<OperationDTO>();
            foreach (var newOperation in newOperations)
            {
                var operation = await operationService.CreateOperationAsync(newOperation);
                operations.Add(operation);
            }
            return Created(nameof(GetAllAsync), operations);
        }

        // PUT api/operations/
        [HttpPut("{id}")]
        public async Task<ActionResult<OperationDTO>> UpdateAsync(Guid id, [FromBody] OperationCreateUpdateDTO updatedOperation)
        {
            var update = await operationService.UpdateOperationAsync(id, updatedOperation);
            return Ok(update);
        }

        // DELETE api/operations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationDTO>> DeleteAsync(Guid id)
        {
            await operationService.DeleteAsync(id);
            return Ok();
        }
    }
}
