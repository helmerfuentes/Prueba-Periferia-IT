using PruebaPeriferiaIT.Core.Entitites;

namespace PruebaPeriferiaIT.Application.Interfaces.Repository
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(int id);
    }
}
