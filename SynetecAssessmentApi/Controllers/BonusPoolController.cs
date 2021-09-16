using AutoMapper;
using Business.Dtos;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            if (request.SelectedEmployeeId == 0)
            {
                return BadRequest("You have not specified a SelectedEmployeeId, please define the SelectedEmployeeId.");
            }

            var entities = await service.CalculateAsync(request);

            return entities == null ? BadRequest(
                $"The employee with SelectedEmployeeId {request.SelectedEmployeeId} has not been found, please defina an existing SelectedEmployeeId.")
                : Ok(entities);
        }
    }
}
