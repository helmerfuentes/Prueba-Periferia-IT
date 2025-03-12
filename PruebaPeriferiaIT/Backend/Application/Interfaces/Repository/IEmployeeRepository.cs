using PruebaPeriferiaIT.Core.Entitites;

namespace PruebaPeriferiaIT.Application.Interfaces.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee product);
        Task UpdateAsync(Employee product);
        Task DeleteAsync(int id);
        Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId);
    }
}
