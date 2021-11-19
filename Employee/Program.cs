using Employee.Controllers.Registration.Extensions;
using Employee.Database.Registration.Extensions;
using Employee.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Employee
{
    class Program
    {
        private static readonly string ConnectionString = Environment.GetEnvironmentVariable("connectionString") 
            ?? throw new InvalidOperationException("You must set the 'connectionString' environment variable");

        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            try
            {
                ServiceProvider serviceProvider = new ServiceCollection()
                    .AddEmployeeRepository(ConnectionString)
                    .AddEmployeeController()
                    .BuildServiceProvider();

                IEmployeeController employeeController = serviceProvider.GetService<IEmployeeController>();
                await employeeController.HandleRequest(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
        }
    }
}
