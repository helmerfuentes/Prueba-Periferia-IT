using Microsoft.EntityFrameworkCore;
using PruebaPeriferiaIT.Application.Interfaces.Repository;
using PruebaPeriferiaIT.Core.Entitites;
using PruebaPeriferiaIT.Infrastructure.Persistence;

namespace PruebaPeriferiaIT.Infrastructure.Repositories
{
    public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
    {
        public async Task<IEnumerable<Employee>> GetAllAsync() => await context.Employees.Include(e => e.Department).ToListAsync();
        public async Task<Employee?> GetByIdAsync(int id) => await context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);
        public async Task AddAsync(Employee employee) { context.Employees.Add(employee); await context.SaveChangesAsync(); }
        public async Task UpdateAsync(Employee employee) { context.Employees.Update(employee); await context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var employee = await GetByIdAsync(id); if (employee != null) { context.Employees.Remove(employee); await context.SaveChangesAsync(); } }
        public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            return await context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
        }
    }
}
