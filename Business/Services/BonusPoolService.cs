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
            //load the details of the selected employee using the Id
            var employee = await employeeService.GetEmployeeByIdAsync(calculateBonus.SelectedEmployeeId);
            var employeeMapped = mapper.Map<EmployeeDto>(employee);

            if (employeeMapped != null)
            {
                int bonusAllocation = await CalculateBonusAllocation(employeeMapped.Salary, calculateBonus.TotalBonusPoolAmount);

                return new BonusPoolCalculatorResultDto
                {
                    Employee = employeeMapped,
                    Amount = bonusAllocation
                };
            }
            return null;
        }

        public async Task<int> CalculateBonusAllocation(int salary, int totalBonusPool)
        {
            //get the total salary budget for the company
            int totalSalary = await employeeService.GetEmployeeSalarySumAsync();

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)salary / (decimal)totalSalary;
            return (int)(bonusPercentage * totalBonusPool);
        }
    }
}
