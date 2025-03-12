using AutoMapper;
using PruebaPeriferiaIT.Application.Commands;
using PruebaPeriferiaIT.Application.Common.Factory;
using PruebaPeriferiaIT.Application.Common.Models;
using PruebaPeriferiaIT.Application.Interfaces.Repository;
using PruebaPeriferiaIT.Application.Interfaces.Service;
using PruebaPeriferiaIT.Application.Queries;
using PruebaPeriferiaIT.Core.Entitites;
using PruebaPeriferiaIT.Core.Enums;

namespace PruebaPeriferiaIT.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeService
        (
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<Result<string>> CreateEmployeeAsync(CreateEmployeeRequest employeeRequest)
        {
            var errors = ValidateEmployee(employeeRequest);

            if (errors.Any())
                return Result<string>.Failure(errors.ToArray());

            var department = await _departmentRepository.GetByIdAsync(employeeRequest.DepartmentId);
            if (department == null)
                return Result<string>.Failure("El departamento no existe.");

            var employee = new Employee
            {
                Name = employeeRequest.Name,
                Email = employeeRequest.Email,
                Salary = CalculateSalary(employeeRequest.Salary, employeeRequest.Position),
                DepartmentId = employeeRequest.DepartmentId,
                Position = (JobPositionEnum)employeeRequest.Position
            };
            await _employeeRepository.AddAsync(employee);
            return Result<string>.Success("Empleado creado con éxito.");
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employess = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employess);
        }

        public async Task<Result<string>> UpdateEmployeeAsync(int id, UpdateEmployeeRequest employeeRequest)
        {
            var errors = ValidateEmployee(employeeRequest);

            if (errors.Any())
                return Result<string>.Failure(errors.ToArray());

            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return Result<string>.Failure("El empleado no existe.");

            employee.Name = employeeRequest.Name;
            employee.Email = employeeRequest.Email;
            employee.Salary = CalculateSalary(employeeRequest.Salary, employeeRequest.Position);
            employee.Position = (JobPositionEnum)employeeRequest.Position;

            await _employeeRepository.UpdateAsync(employee);
            return Result<string>.Success("Empleado actualizado con éxito.");
        }

        public async Task<Result<string>> DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return Result<string>.Failure("El empleado no existe.");

            await _employeeRepository.DeleteAsync(id);
            return Result<string>.Success("Empleado eliminado con éxito.");
        }

        public async Task<Result<IEnumerable<EmployeeDto>>> GetEmployeesByDepartmentAsync(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
                return Result<IEnumerable<EmployeeDto>>.Failure("El departamento no existe.");

            var employees = await _employeeRepository.GetByDepartmentIdAsync(departmentId);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return Result<IEnumerable<EmployeeDto>>.Success(employeeDtos);
        }


        private decimal CalculateSalary(decimal baseSalary, int position)
        {
            var salaryStrategy = SalaryStrategyFactory.GetStrategy((JobPositionEnum)position);
            return salaryStrategy.CalculateSalary(baseSalary);
        }

        private List<string> ValidateEmployee(EmployeeBaseRequest employeeRequest)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(employeeRequest.Name))
                errors.Add("El nombre del empleado es obligatorio.");

            if (string.IsNullOrWhiteSpace(employeeRequest.Email) || !employeeRequest.Email.Contains("@"))
                errors.Add("El email no es válido.");

            if (employeeRequest.Salary <= 0)
                errors.Add("El salario debe ser mayor a 0.");

            if (!Enum.IsDefined(typeof(JobPositionEnum), employeeRequest.Position))
                errors.Add("La posición ingresada no es válida.");

            return errors;
        }


    }
}
