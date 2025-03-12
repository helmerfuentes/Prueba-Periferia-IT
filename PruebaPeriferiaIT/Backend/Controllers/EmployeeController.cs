using Microsoft.AspNetCore.Mvc;
using PruebaPeriferiaIT.Application.Commands;
using PruebaPeriferiaIT.Application.Interfaces.Service;

namespace PruebaPeriferiaIT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _employeeService.GetAllEmployeesAsync());

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeRequest employee)
        {
            var result = await _employeeService.CreateEmployeeAsync(employee);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmployeeRequest employeeRequest)
        {
            var result = await _employeeService.UpdateEmployeeAsync(id, employeeRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpGet("department/{departmentId}/employees")]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            var result = await _employeeService.GetEmployeesByDepartmentAsync(departmentId);
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
