using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodReportsController : ControllerBase
    {
        private IOperationService _operationService;

        public PeriodReportsController(IOperationService service)
        {
            _operationService = service;
        }

        // GET api/outcome/Date
        [HttpGet, Route("Date")]
        public async Task<ActionResult<TotalForPeriodDTO>> GetAllAsync(DateTime date)
        {
            var total = await _operationService.GetTotalForDateAsync(date);
            return Ok(total);
        }

        // GET api/outcome/Period
        [HttpGet, Route("Period")]
        public async Task<ActionResult<TotalForPeriodDTO>> GetAsync(DateTime startDate, DateTime endDate)
        {
            var total = await _operationService.GetTotalForPeriodAsync(startDate, endDate);
            return Ok(total);
        }
    }
}
