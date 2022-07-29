using JWTAuth.Business;
using JWTAuth.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeAddDto employeeAddDto)
        {
            var response = await _employeeService.AddAsync(employeeAddDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost("put")]
        public async Task<IActionResult> Put([FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            var response = await _employeeService.UpdateAsync(employeeUpdateDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest();
        }

    }
}
