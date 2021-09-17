using AutoMapper;
using Business.Dtos;
using Business.Services.Interfaces;
using System.Threading.Tasks;

namespace Business.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;

        public BonusPoolService(IMapper mapper, IEmployeeService employeeService)
        {
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(CalculateBonusDto calculateBonus)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(calculateBonus.SelectedEmployeeId);
            var employeeMapped = mapper.Map<EmployeeDto>(employee);

            if (employeeMapped != null)
            {
                int bonusAllocation = await GetBonusAllocation(employeeMapped.Salary, calculateBonus.TotalBonusPoolAmount);

                return new BonusPoolCalculatorResultDto
                {
                    Employee = employeeMapped,
                    Amount = bonusAllocation
                };
            }
            return null;
        }

        private async Task<int> GetBonusAllocation(int salary, int totalBonusPool)
        {
            int totalSalary = await employeeService.GetEmployeeSalarySumAsync();

            decimal bonusPercentage = (decimal)salary / (decimal)totalSalary;
            return (int)(bonusPercentage * totalBonusPool);
        }
    }
}
