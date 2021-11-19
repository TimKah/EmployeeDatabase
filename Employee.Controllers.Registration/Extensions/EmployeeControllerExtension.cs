using Employee.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.Controllers.Registration.Extensions
{
    public static class EmployeeControllerExtension
    {
        public static IServiceCollection AddEmployeeController(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IEmployeeController, EmployeeController>();
        }
    }
}
