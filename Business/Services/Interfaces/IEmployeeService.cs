using Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int selectedEmployeeId);
        Task<int> GetEmployeeSalarySumAsync();
    }
}
