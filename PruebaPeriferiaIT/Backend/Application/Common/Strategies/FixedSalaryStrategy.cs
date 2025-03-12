namespace PruebaPeriferiaIT.Application.Common.Strategies
{
    public class FixedSalaryStrategy : ISalaryStrategy
    {
        public decimal CalculateSalary(decimal baseSalary) => baseSalary;
    }
}
