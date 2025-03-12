using AutoMapper;
using Moq;
using PruebaPeriferiaIT.Application.Commands;
using PruebaPeriferiaIT.Application.Interfaces.Repository;
using PruebaPeriferiaIT.Application.Interfaces.Service;
using PruebaPeriferiaIT.Application.Queries;
using PruebaPeriferiaIT.Application.Services;
using PruebaPeriferiaIT.Core.Entitites;
using PruebaPeriferiaIT.Core.Enums;

namespace TestBackend
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<IDepartmentRepository> _departmentRepositoryMock;
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IMapper> _mapperMock;

        public EmployeeServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _departmentRepositoryMock = new Mock<IDepartmentRepository>();
            _employeeService = new EmployeeService(_employeeRepositoryMock.Object, _departmentRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetEmployeesByDepartmentAsync_ShouldReturnEmployees_WhenDepartmentExists()
        {
            // Arrange
            const int departmentId = 1;
            var department = new Department { Id = departmentId, Name = "IT" };
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Juan Pérez", Email = "juan@email.com", Salary = 3000, Position = JobPositionEnum.Developer, DepartmentId = departmentId },
                new Employee { Id = 2, Name = "Ana Gómez", Email = "ana@email.com", Salary = 4000, Position = JobPositionEnum.Manager, DepartmentId = departmentId }
            };

            var employeeDtos = employees.Select(e => new EmployeeDto { Key = e.Id, Name = e.Name, Email = e.Email }).ToList();

            _departmentRepositoryMock.Setup(repo => repo.GetByIdAsync(departmentId)).ReturnsAsync(department);
            _employeeRepositoryMock.Setup(repo => repo.GetByDepartmentIdAsync(departmentId)).ReturnsAsync(employees);
            _mapperMock.Setup(m => m.Map<IEnumerable<EmployeeDto>>(employees)).Returns(employeeDtos);

            // Act
            var result = await _employeeService.GetEmployeesByDepartmentAsync(departmentId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count());
            Assert.Contains(result.Data, e => e.Name == "Juan Pérez");
        }

        [Fact]
        public async Task GetEmployeesByDepartmentAsync_ShouldReturnFailure_WhenDepartmentDoesNotExist()
        {
            // Arrange
            int departmentId = 99;
            _departmentRepositoryMock.Setup(repo => repo.GetByIdAsync(departmentId)).ReturnsAsync((Department)null);

            // Act
            var result = await _employeeService.GetEmployeesByDepartmentAsync(departmentId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("El departamento no existe.", result.Errors);
        }

        [Fact]
        public async Task CreateEmployeeAsync_ShouldReturnSuccess_WhenEmployeeIsValid()
        {
            // Arrange
            var request = new CreateEmployeeRequest
            {
                Name = "Carlos López",
                Email = "carlos@email.com",
                Salary = 3500,
                Position = (int)JobPositionEnum.Developer,
                DepartmentId = 1
            };

            var department = new Department { Id = 1, Name = "IT" };
            var employee = new Employee
            {
                Id = 1,
                Name = request.Name,
                Email = request.Email,
                Salary = request.Salary,
                Position = (JobPositionEnum)request.Position,
                DepartmentId = request.DepartmentId
            };
            _departmentRepositoryMock.Setup(repo => repo.GetByIdAsync(request.DepartmentId)).ReturnsAsync(department);
            _mapperMock.Setup(m => m.Map<Employee>(request)).Returns(employee);
            _employeeRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);

            // Act
            var result = await _employeeService.CreateEmployeeAsync(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Empleado creado con éxito.", result.Data);
        }

        [Fact]
        public async Task CreateEmployeeAsync_ShouldReturnFailure_WhenPositionIsInvalid()
        {
            // Arrange
            var request = new CreateEmployeeRequest
            {
                Name = "Carlos López",
                Email = "carlos@email.com",
                Salary = 3500,
                Position = 99,
                DepartmentId = 1
            };

            // Act
            var result = await _employeeService.CreateEmployeeAsync(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("La posición ingresada no es válida.", result.Errors);
        }
    }
}