using Employee.Database.Contexts;
using Employee.Database.Profiles;
using Employee.Database.Repositories;
using Employee.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.Database.Registration.Extensions
{
    public static class EmployeeRepositoryExtension
    {
        public static IServiceCollection AddEmployeeRepository(this IServiceCollection serviceCollection, string connectionString)
        {
            return serviceCollection.AddDbContext<EmployeesRepositoryContext>(options => options.UseNpgsql(connectionString))
                                    .AddSingleton<IEmployeesRepository, EmployeesRepository>()
                                    .AddAutoMapper(typeof(EmployeeDatabaseProfile));
        }
    }
}
