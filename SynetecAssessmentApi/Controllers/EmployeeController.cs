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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService service;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet("Employees")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await service.GetEmployeesAsync();
            return Ok(mapper.Map<List<EmployeeDto>>(entities));
        }
    }
}
