using PruebaPeriferiaIT.Application.Common.Strategies;
using PruebaPeriferiaIT.Core.Enums;

namespace PruebaPeriferiaIT.Application.Common.Factory
{
    public static class SalaryStrategyFactory
    {
        public static ISalaryStrategy GetStrategy(JobPositionEnum position)
        {
            return position switch
            {
                JobPositionEnum.Developer => new DeveloperSalaryStrategy(),
                JobPositionEnum.Manager => new ManagerSalaryStrategy(),
                JobPositionEnum.HR => new FixedSalaryStrategy(),
                JobPositionEnum.Sales => new FixedSalaryStrategy(),
                _ => throw new ArgumentException("Posición inválida")
            };
        }
    }
}
