using Business.Services;
using Business.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SynetecAssessmentApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IBonusPoolService, BonusPoolService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
