namespace PruebaPeriferiaIT.Application.Common.Strategies
{
    public class DeveloperSalaryStrategy : ISalaryStrategy
    {
        public decimal CalculateSalary(decimal baseSalary) => baseSalary * 1.10m;
    }
}
