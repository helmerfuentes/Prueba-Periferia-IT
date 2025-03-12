using Microsoft.EntityFrameworkCore;
using PruebaPeriferiaIT.Application.Interfaces.Repository;
using PruebaPeriferiaIT.Core.Entitites;
using PruebaPeriferiaIT.Infrastructure.Persistence;

namespace PruebaPeriferiaIT.Infrastructure.Repositories
{
    public class DepartmentRepository(AppDbContext _context) : IDepartmentRepository
    {
        public Task<Department?> GetByIdAsync(int id)
        => _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
    }
}
