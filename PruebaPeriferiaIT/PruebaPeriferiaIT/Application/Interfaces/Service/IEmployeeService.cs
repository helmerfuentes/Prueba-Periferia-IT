using PruebaPeriferiaIT.Application.Commands;
using PruebaPeriferiaIT.Application.Common.Models;
using PruebaPeriferiaIT.Application.Queries;

namespace PruebaPeriferiaIT.Application.Interfaces.Service
{
    public interface IEmployeeService
    {
        Task<Result<string>> CreateEmployeeAsync(CreateEmployeeRequest employee);
        Task<Result<string>> DeleteEmployeeAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<Result<IEnumerable<EmployeeDto>>> GetEmployeesByDepartmentAsync(int departmentId);
        Task<Result<string>> UpdateEmployeeAsync(int id, UpdateEmployeeRequest employeeRequest);
    }
}
