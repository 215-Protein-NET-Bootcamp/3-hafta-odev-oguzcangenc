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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employees = await _employeeService.GetByIdAsync(id);
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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            var updateEmployee = await _employeeService.GetByIdAsync(employeeUpdateDto.Id);
            if (updateEmployee.Success)
            {
                var response = await _employeeService.UpdateAsync(employeeUpdateDto);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest();
            }
            return NotFound();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteEmployee = await _employeeService.GetByIdAsync(id);
            if (deleteEmployee.Success)
            {
                var response = await _employeeService.DeleteAsync(deleteEmployee.Data);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest();
            }
            return BadRequest();

        }
    }
}
