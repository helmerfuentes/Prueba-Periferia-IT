namespace PruebaPeriferiaIT.Application.Commands
{
    public class CreateEmployeeRequest : EmployeeBaseRequest
    {
        public int DepartmentId { get; set; }
    }
}
