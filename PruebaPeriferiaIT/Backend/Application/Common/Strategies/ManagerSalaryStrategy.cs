namespace PruebaPeriferiaIT.Application.Common.Strategies
{
    public class ManagerSalaryStrategy : ISalaryStrategy
    {
        public decimal CalculateSalary(decimal baseSalary) => baseSalary * 1.20m;
    }
}
