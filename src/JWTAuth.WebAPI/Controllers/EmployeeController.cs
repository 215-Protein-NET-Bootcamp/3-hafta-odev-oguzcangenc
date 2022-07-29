using JWTAuth.Business;
using JWTAuth.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
       
       

      
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeAddDto employeeAddDto)
        {
           var userId = int.Parse(User.Claims.First(x => x.Type == "AccountId").Value);
            var response = await _employeeService.AddAsync(employeeAddDto, userId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest();
        }

       
    }
}
