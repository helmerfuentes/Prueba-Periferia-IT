using PruebaPeriferiaIT.Core.Enums;

namespace PruebaPeriferiaIT.Core.Entitites
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public JobPositionEnum Position { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
