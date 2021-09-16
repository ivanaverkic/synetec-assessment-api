using AutoMapper;
using Business.Dtos;
using Business.Services.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;
        private readonly AppDbContext _dbContext;

        public BonusPoolService(IMapper mapper, IEmployeeService employeeService)
        {
            this.mapper = mapper;
            this.employeeService = employeeService;

            var dbContextOptionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionBuilder.UseInMemoryDatabase(databaseName: "HrDb");

            _dbContext = new AppDbContext(dbContextOptionBuilder.Options);
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(CalculateBonusDto calculateBonus)
        {
            //load the details of the selected employee using the Id
            var employee = await employeeService.GetEmployeeById(calculateBonus.SelectedEmployeeId);
            var employeeMapped = mapper.Map<EmployeeDto>(employee);

            if (employeeMapped != null)
            {
                int bonusAllocation = CalculateBonusAllocation(employeeMapped.Salary, calculateBonus.TotalBonusPoolAmount);

                return new BonusPoolCalculatorResultDto
                {
                    Employee = employeeMapped,
                    Amount = bonusAllocation
                };
            }
            return null;
        }

        public int CalculateBonusAllocation(int salary, int totalBonusPool)
        {
            //get the total salary budget for the company
            int totalSalary = (int)_dbContext.Employees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)salary / (decimal)totalSalary;
            return (int)(bonusPercentage * totalBonusPool);
        }

        //public async Task<BonusPoolCalculatorResultDto> CalculateAsync(CalculateBonusDto calculateBonus)
        //{
        //    //load the details of the selected employee using the Id
        //    Employee employee = await _dbContext.Employees
        //        .Include(e => e.Department)
        //        .FirstOrDefaultAsync(item => item.Id == calculateBonus.SelectedEmployeeId);

        //    if (employee != null)
        //    {
        //        //get the total salary budget for the company
        //        int totalSalary = (int)_dbContext.Employees.Sum(item => item.Salary);

        //        //calculate the bonus allocation for the employee
        //        decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
        //        int bonusAllocation = (int)(bonusPercentage * calculateBonus.TotalBonusPoolAmount);

        //        return new BonusPoolCalculatorResultDto
        //        {
        //            Employee = new EmployeeDto
        //            {
        //                Fullname = employee.Fullname,
        //                JobTitle = employee.JobTitle,
        //                Salary = employee.Salary,
        //                Department = new DepartmentDto
        //                {
        //                    Title = employee.Department.Title,
        //                    Description = employee.Department.Description
        //                }
        //            },

        //            Amount = bonusAllocation
        //        };
        //    }
        //    return null;
        //}

    }
}
