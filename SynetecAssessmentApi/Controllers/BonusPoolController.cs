using Business.Dtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusPoolController : ControllerBase
    {
        private readonly IBonusPoolService service;

        public BonusPoolController(IBonusPoolService service)
        {
            this.service = service;
        }

        [HttpPost("CalculateBonus")]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            var entities = await service.CalculateAsync(request);

            return entities == null ? BadRequest(
                $"The employee with SelectedEmployeeId {request.SelectedEmployeeId} has not been found, please define an existing SelectedEmployeeId.")
                : Ok(entities);
        }
    }
}
